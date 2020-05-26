using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum ConversationResult
{
    Success,
    Failure,
    Rejected
}

[Serializable]
public class CandidateResultResponse
{
    public string SuccessString;
    public string FailedString;
    public string RejectedString;
}

public class ConversationConclusion
{
    public string String { get; private set; }
    public ConversationResult Result { get; private set; }

    public ConversationConclusion(ConversationConclusionStrings pool, Candidate candidate, ConversationResult result)
    {
        Result = result;
        String = GetCandidateConclusionString(pool, candidate, result);
    }

    private string GetCandidateConclusionString(ConversationConclusionStrings pool, Candidate candidate, ConversationResult result)
    {
        switch (result)
        {
            case ConversationResult.Success:
                return candidate.SuccessResponse ?? pool.SuccessResponses.GetRandom();
            case ConversationResult.Failure:
                return candidate.FailureResponse ?? pool.FailureResponses.GetRandom();
            case ConversationResult.Rejected:
                return candidate.RejectedResponse ?? pool.RejectedResponses.GetRandom();
            default: 
                Debug.LogError("Conversation Result not recognised");
                return null;
        }
    }
}
