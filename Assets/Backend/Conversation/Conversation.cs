using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Conversation 
{
    private const string CONVERSATION_END_ID = "-2";
    private const string RECRUIT_ID = "-3";

    private GameSession m_GameSession;
    private ChoicePool m_ChoicePool;
    private Personality m_Personality;

    public SubscriptionValue<int> PersuasionLevel { get; private set; } = new SubscriptionValue<int>();
    public SubscriptionValue<int> PersuasionLevelDelta { get; private set; } = new SubscriptionValue<int>();
    public SubscriptionValue<Choice> Choice { get; private set; }

    public Conversation(GameSession gameSession, Candidate candidate, ChoicePool pool)
    {
        m_GameSession = gameSession;
        m_ChoicePool = pool;
        m_Personality = candidate.Personality;

        Choice opening;

        if (string.IsNullOrWhiteSpace(candidate.StartingConversationChoiceID))
        {
            opening = pool.Choices.Where(choice => choice.IsStartingNode).ToArray().GetRandom();
        }
        else
        {
            opening = GetChoiceForID(candidate.StartingConversationChoiceID);
        }

        Choice = new SubscriptionValue<Choice>(opening);
    }

    public void SelectOption(Option option)
    {
        string nextChoiceID = GetNextChoiceID(option);

        if(nextChoiceID == CONVERSATION_END_ID)
        {
            m_GameSession.NextCandidate();
        }
        else if(nextChoiceID == RECRUIT_ID){
            m_GameSession.AttemptRecruitCandidate();
        }
        else
        {
            Choice.Value = GetChoiceForID(nextChoiceID);
        }
    }

    private Choice GetChoiceForID(string id)
    {
        return m_ChoicePool.Choices.FirstOrDefault(c => c.ID == id);
    }

    private string GetNextChoiceID(Option option)
    {
        int score = m_Personality * option.AppealingFactors;

        Debug.Log(score);

        //The value of each choice is summed to make the current persuasion level
        PersuasionLevel.Value += score;
        PersuasionLevelDelta.Value = score;

        //Ordering the list with the highest score first means that as we compare, we check the better results first
        IEnumerable<AttainableChoice> outcomes = option.AttainableChoices.OrderByDescending(outcome => outcome.ScoreRequired);

        foreach (AttainableChoice outcome in outcomes)
        {
            if (score >= outcome.ScoreRequired)
            {
                return outcome.ChoiceID;
            }
        }

        return option.DefaultChoiceID;
    }
}
