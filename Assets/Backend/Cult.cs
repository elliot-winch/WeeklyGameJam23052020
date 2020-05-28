using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cult 
{
    public int StartingWealth;
    public int StartingLoyality;
    public int StartingReputation;

    [NonSerialized]
    public List<Candidate> Recruits;

    public SubscriptionValue<int> Wealth { get; private set; }
    public SubscriptionValue<int> Loyality { get; private set; }
    public SubscriptionValue<int> Reputation { get; private set; }

    public int Max = 100;

    public Cult()
    {
        Wealth = new SubscriptionValue<int>(StartingWealth);
        Loyality = new SubscriptionValue<int>(StartingLoyality);
        Reputation = new SubscriptionValue<int>(StartingReputation);

        Recruits = new List<Candidate>();
    }

    public void AddCandidate(Candidate candidate)
    {

        Recruits.Add(candidate);

        Wealth.Value += candidate.Wealth;
        Loyality.Value += candidate.Loyality;
        Reputation.Value += candidate.Reputation;

        if(Wealth.Value > Max)
            Wealth.Value = Max;

        if(Wealth.Value < 0)
            Wealth.Value = 0;

        if(Reputation.Value > Max)
            Reputation.Value = Max;

        if(Reputation.Value < 0)
            Reputation.Value = 0;

        if(Loyality.Value > Max)
            Loyality.Value = Max;

        if(Loyality.Value < 0)
            Loyality.Value = 0;
    }
}
