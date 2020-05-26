using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cult 
{
    public int StartingWealth;
    public int StartingLoyality;
    public int StartingReputation;

    [SerializeField]
    public List<Candidate> Recruits;

    public SubscriptionValue<int> Wealth { get; private set; }
    public SubscriptionValue<int> Loyality { get; private set; }
    public SubscriptionValue<int> Reputation { get; private set; }

    public Cult()
    {
        Wealth = new SubscriptionValue<int>(StartingWealth);
        Loyality = new SubscriptionValue<int>(StartingLoyality);
        Reputation = new SubscriptionValue<int>(StartingReputation);
    }

    public void AddCandidate(Candidate candidate)
    {
        Recruits.Add(candidate);

        Wealth.Value += candidate.Wealth;
        Loyality.Value += candidate.Loyality;
        Reputation.Value += candidate.Reputation;
    }
}
