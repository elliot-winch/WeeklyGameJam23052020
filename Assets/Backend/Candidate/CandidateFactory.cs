using UnityEngine;
using System.Linq;
using Random = System.Random;
using System.Collections.Generic;

public class CandidateFactory : MonoBehaviour
{
    [SerializeField]
    private string m_FilePath;

    private CandidatePool m_CandidatePool;

    private List<Candidate> m_AvailableCandidates;
    private Random m_Random;

    public void Load(int? seed = null)
    {
        m_Random = seed.HasValue ? new Random(seed.Value) : new Random();       
        m_CandidatePool = JSONLoader.LoadFromFile<CandidatePool>(m_FilePath);
        m_AvailableCandidates = m_CandidatePool.Candidates.ToList();
    }

    public Candidate Generate(int? seed = null)
    {
        if (m_AvailableCandidates.Count <= 0)
        {
            return GenerateRandom(seed);
        }

        Random random = seed.HasValue ? new Random(seed.Value) : m_Random;

        Candidate candidate = m_AvailableCandidates[random.Next(0, m_CandidatePool.Candidates.Length)];

        m_AvailableCandidates.Remove(candidate);

        return Populate(candidate, random);
    }

    public Candidate GenerateRandom(int? seed = null)
    {
        Random random = seed.HasValue ? new Random(seed.Value) : m_Random;

        return Populate(new Candidate(), random);
    }

    private Candidate Populate(Candidate candidate, Random random)
    {
        if (string.IsNullOrWhiteSpace(candidate.FirstName))
        {
            candidate.FirstName = m_CandidatePool.FirstNames[random.Next(0, m_CandidatePool.FirstNames.Length)];
        }

        if (string.IsNullOrWhiteSpace(candidate.LastName))
        {
            candidate.LastName = m_CandidatePool.LastNames[random.Next(0, m_CandidatePool.LastNames.Length)];
        }

        if (candidate.RandomWealth)
        {
            candidate.Wealth = random.Next(m_CandidatePool.WealthBounds.Min, m_CandidatePool.WealthBounds.Max);
        }

        if (candidate.RandomLoyality)
        {
            candidate.Loyality = random.Next(m_CandidatePool.LoyalityBounds.Min, m_CandidatePool.LoyalityBounds.Max);
        }

        if (candidate.RandomReputation)
        {
            candidate.Reputation = random.Next(m_CandidatePool.ReputationBounds.Min, m_CandidatePool.ReputationBounds.Max);
        }

        if (candidate.RandomPersuasionRequirement)
        {
            candidate.PersuasionRequirement = random.Next(m_CandidatePool.PersuasionRequirementBounds.Min, m_CandidatePool.PersuasionRequirementBounds.Max);
        }

        return candidate;
    }
}
