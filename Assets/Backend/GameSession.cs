using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private CandidateFactory m_CandidateFactory;
    [SerializeField]
    private ChoiceLoader m_ChoiceFactory;
    [SerializeField]
    private string m_ConversationConclusionStringsFilePath;
    [SerializeField]
    private string m_CultStartingStatsFilePath;

    private ConversationConclusionStrings m_ConversationConlusionStrings;

    public Cult Cult { get; private set; }
    public CandidatePool CandidatePool => m_CandidateFactory.CandidatePool;

    public SubscriptionValue<Candidate> Candidate { get; private set; } = new SubscriptionValue<Candidate>();
    public SubscriptionValue<Conversation> Conversation { get; private set; } = new SubscriptionValue<Conversation>();
    public SubscriptionValue<ConversationConclusion> Conclusion { get; private set; } = new SubscriptionValue<ConversationConclusion>();


    private void Awake()
    {
        m_CandidateFactory.Load();
        m_ChoiceFactory.Load();

        m_ConversationConlusionStrings = JSONLoader.LoadFromFile<ConversationConclusionStrings>(m_ConversationConclusionStringsFilePath);

        Cult = JSONLoader.LoadFromFile<Cult>(m_CultStartingStatsFilePath);
    }

    public void NextCandidate()
    {
        Conclusion.Value = null;

        Candidate.Value = m_CandidateFactory.Generate();

        Conversation.Value = new Conversation(Candidate.Value, m_ChoiceFactory.ChoicePool);
    }

    public void AttemptRecruitCandidate()
    {
        if(Conversation.Value == null)
        {
            return;
        }

        if(Conversation.Value.PersuasionLevel.Value >= Candidate.Value.PersuasionRequirement)
        {
            Cult.AddCandidate(Candidate.Value);
            Debug.Log("candidate Added");

            Conclusion.Value = new ConversationConclusion(m_ConversationConlusionStrings, Candidate.Value, ConversationResult.Success);
        }
        else
        {
            Conclusion.Value = new ConversationConclusion(m_ConversationConlusionStrings, Candidate.Value, ConversationResult.Failure);
        }

        EndConversation();
    }

    public void RejectCandidate()
    {
        if (Conversation.Value == null)
        {
            return;
        }

        EndConversation();

        Conclusion.Value = new ConversationConclusion(m_ConversationConlusionStrings, Candidate.Value, ConversationResult.Rejected);
    }

    private void EndConversation()
    {
        Conversation.Value = null;
    }
}
