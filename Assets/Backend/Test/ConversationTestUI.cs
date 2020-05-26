using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TODO: replace with formal UI. This is a test to ensure enough data is exposed to build the UI
/// </summary>
public class ConversationTestUI : MonoBehaviour
{
    [SerializeField]
    private Button m_ButtonPrefab;
    [SerializeField]
    private Transform m_ButtonParent;
    [SerializeField]
    private TextMeshProUGUI m_CandidateNameText;
    [SerializeField]
    private TextMeshProUGUI m_ConversationConclusionText;
    [SerializeField]
    private TextMeshProUGUI m_CandidateSpeechText;
    [SerializeField]
    private CandidateStatMeter _wealthMeter;
    [SerializeField]
    private CandidateStatMeter _reputationMeter;
    [SerializeField]
    private CandidateStatMeter _loyaltyMeter;
    [SerializeField]
    private GameSession m_GameSession;

    private void Awake()
    {
        m_GameSession.Candidate.Subscribe(SetupCandidate);
        m_GameSession.Conversation.Subscribe(SetupConversation);
        m_GameSession.Conclusion.Subscribe(SetupConversationConclusion);
    }

    private void SetupCandidate(Candidate candidate)
    {
        if(candidate != null)
        {
            m_CandidateNameText.text = candidate.FirstName;

            _wealthMeter?.SetCandidateStats(candidate.Wealth, m_GameSession.CandidatePool.WealthBounds.Max);
            _reputationMeter?.SetCandidateStats(candidate.Loyality, m_GameSession.CandidatePool.ReputationBounds.Max);
            _loyaltyMeter?.SetCandidateStats(candidate.Loyality, m_GameSession.CandidatePool.LoyalityBounds.Max);
        }
    }

    private void SetupConversation(Conversation conversation)
    {
        if(conversation != null)
        {
            conversation.Choice.Subscribe(SetupChoice);
        }
        else
        {
            ClearChoice();
        }
    }

    private void SetupConversationConclusion(ConversationConclusion conclusion)
    {
        m_ConversationConclusionText.text = conclusion?.String ?? "";
    }

    private void SetupChoice(Choice choice)
    {
        ClearChoice();

        m_CandidateSpeechText.text = choice.Phrase;

        foreach (Option option in choice.Options)
        {
            Button button = Instantiate(m_ButtonPrefab, m_ButtonParent);
            button.GetComponentInChildren<Text>().text = option.Phrase;
            button.onClick.AddListener(() => { m_GameSession.Conversation.Value.SelectOption(option); });
        }
    }

    private void ClearChoice()
    {
        m_CandidateSpeechText.text = "";

        foreach (Button button in m_ButtonParent.GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }
    }
}
