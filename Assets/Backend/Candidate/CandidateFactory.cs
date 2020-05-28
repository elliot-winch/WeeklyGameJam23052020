using UnityEngine;
using System.Linq;
using Random = System.Random;
using System.Collections.Generic;

public class CandidateFactory : MonoBehaviour
{
    [SerializeField]
    private string m_FilePath;

    public CandidatePool CandidatePool { get; private set; }

    private List<Candidate> m_AvailableCandidates;
    private Random m_Random;

    public void Load(int? seed = null)
    {
        m_Random = seed.HasValue ? new Random(seed.Value) : new Random();       
        CandidatePool = JSONLoader.LoadFromFile<CandidatePool>(m_FilePath);
        m_AvailableCandidates = CandidatePool.Candidates.ToList();
    }

    public Candidate Generate(int? seed = null)
    {
        if (m_AvailableCandidates.Count <= 0)
        {
            return GenerateRandom(seed);
        }

        Random random = seed.HasValue ? new Random(seed.Value) : m_Random;

        Candidate candidate = m_AvailableCandidates[random.Next(0, m_AvailableCandidates.Count)];

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
            candidate.FirstName = CandidatePool.FirstNames.GetRandom(random);
        }

        if (string.IsNullOrWhiteSpace(candidate.LastName))
        {
            candidate.LastName = CandidatePool.LastNames.GetRandom(random);
        }

        if (candidate.RandomWealth)
        {
            candidate.Wealth = random.Next(CandidatePool.WealthBounds.Min, CandidatePool.WealthBounds.Max);
        }

        if (candidate.RandomLoyality)
        {
            candidate.Loyality = random.Next(CandidatePool.LoyalityBounds.Min, CandidatePool.LoyalityBounds.Max);
        }

        if (candidate.RandomReputation)
        {
            candidate.Reputation = random.Next(CandidatePool.ReputationBounds.Min, CandidatePool.ReputationBounds.Max);
        }

        if (candidate.RandomPersuasionRequirement)
        {
            candidate.PersuasionRequirement = random.Next(CandidatePool.PersuasionRequirementBounds.Min, CandidatePool.PersuasionRequirementBounds.Max);
        }

        return candidate;
    }
}
