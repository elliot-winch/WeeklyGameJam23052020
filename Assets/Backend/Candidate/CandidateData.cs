using System;

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

    public string StartingConversationChoiceID;

    //Responses to conversation outcomes
    public string SuccessResponse;
    public string FailureResponse;
    public string RejectedResponse;
}

[Serializable]
public class CandidatePool
{
    public Candidate[] Candidates;

    public MinMax WealthBounds;
    public MinMax LoyalityBounds;
    public MinMax ReputationBounds;
    public MinMax PersuasionRequirementBounds;

    //Random generated fields
    public string[] FirstNames;
    public string[] LastNames;
}

[Serializable]
public class MinMax
{
    public int Min;
    public int Max;
}
