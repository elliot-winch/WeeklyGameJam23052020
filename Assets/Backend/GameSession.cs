using UnityEngine;

//TEMP Testing
public class TestScoreGenerator : IScoreGenerator
{
    public int GetScore(Personality personality, Option option)
    {
        return 0;
    }
}
//end Temp

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private CandidateFactory m_CandidateFactory;
    [SerializeField]
    private ChoiceLoader m_ChoiceFactory;

    public SubscriptionValue<Conversation> Conversation { get; private set; } = new SubscriptionValue<Conversation>();

    private void Awake()
    {
        m_CandidateFactory.Load();
        m_ChoiceFactory.Load();

        Conversation.Value = new Conversation(m_CandidateFactory.Generate(), m_ChoiceFactory.ChoicePool.Choices[0], m_ChoiceFactory.ChoicePool, new TestScoreGenerator());
    }
}
