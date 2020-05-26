using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Conversation 
{
    private ChoicePool m_ChoicePool;
    private IScoreGenerator m_Scorer;
    private Personality m_Personality;

    public SubscriptionValue<int> PersuasionLevel { get; private set; } = new SubscriptionValue<int>();
    public SubscriptionValue<Choice> Choice { get; private set; }

    public Conversation(Candidate candidate, ChoicePool pool, IScoreGenerator scorer)
    {
        m_ChoicePool = pool;
        m_Scorer = scorer;
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
        Choice.Value = GetNextChoice(option);
    }

    private Choice GetNextChoice(Option option)
    {
        int score = m_Scorer.GetScore(m_Personality, option);

        //The value of each choice is summed to make the current persuasion level
        PersuasionLevel.Value += score;

        //ordering the list with the highest score first means that as we compare, we check the better results first
        IEnumerable<AttainableChoice> outcomes = option.AttainableChoices.OrderByDescending(outcome => outcome.ScoreRequired);
        
        foreach(AttainableChoice outcome in outcomes)
        {
            if(score >= outcome.ScoreRequired)
            {
                return GetChoiceForID(outcome.ChoiceID);
            }
        }

        return GetChoiceForID(option.DefaultChoiceID);
    }

    private Choice GetChoiceForID(string id)
    {
        Choice choice = m_ChoicePool.Choices.FirstOrDefault(c => c.ID == id);

        if(choice == null)
        {
            Debug.LogError("Cannot find choice for ID: " + id);
        }

        return choice;
    }
}
