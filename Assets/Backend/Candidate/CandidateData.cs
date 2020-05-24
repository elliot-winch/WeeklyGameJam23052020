﻿using System;

[Serializable]
public class Candidate
{
    public string FirstName;
    public string LastName;
    public int Wealth;
    public int Loyality;
    public int Reputation;
    public int PersuasionRequirement;
    public Personality Personality;

    //Names are randomly generated if they're left blank
    public bool RandomWealth = true;
    public bool RandomLoyality = true;
    public bool RandomReputation = true;
    public bool RandomPersuasionRequirement = true;
}

[Serializable]
public class CandidatePool
{
    public Candidate[] Candidates;

    //Random generated fields
    public string[] FirstNames;
    public string[] LastNames;

    public MinMax WealthBounds;
    public MinMax LoyalityBounds;
    public MinMax ReputationBounds;
    public MinMax PersuasionRequirementBounds;
}

[Serializable]
public class MinMax
{
    public int Min;
    public int Max;
}