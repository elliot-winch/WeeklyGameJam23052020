using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TODO: replace with formal UI. This is a test to ensure enough data is exposed to build the UI
/// </summary>
public class ConversationTestUI : MonoBehaviour
{
    [SerializeField]
    private GameSession m_GameSession;

    [Header("Conversation")]
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

    [Header("Candidate Stats")]
    [SerializeField]
    private CandidateStatMeter _wealthMeter;

    [SerializeField]
    private CandidateStatMeter _reputationMeter;

    [SerializeField]
    private CandidateStatMeter _loyaltyMeter;

    [Header("Cult Stats")]
    [SerializeField]
    private Image _cultWealthImage;

    [SerializeField]
    private Image _cultLoyaltyImage;

    [SerializeField]
    private Image _cultReputationImage;

    private void Awake()
    {
        m_GameSession.Candidate.Subscribe(SetupCandidate);
        m_GameSession.Conversation.Subscribe(SetupConversation);
        m_GameSession.Conclusion.Subscribe(SetupConversationConclusion);

        m_GameSession.Cult.Wealth.Subscribe(UpdateWealthCultMeter);
        m_GameSession.Cult.Loyality.Subscribe(UpdateLoyaltyCultMeter);
        m_GameSession.Cult.Reputation.Subscribe(UpdateReputationCultMeter);
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

            //Create a new button in the normal button area to serve as the "next" button
            //works when using the recruit/dismiss buttons to end a conversation
            //but not if you reach the end of the text options and it gives the null reference error
            Button next = Instantiate(m_ButtonPrefab, m_ButtonParent);
            next.GetComponentInChildren<TextMeshProUGUI>().text = "Move to next Candidate";
            next.onClick.AddListener(m_GameSession.NextCandidate);
 
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
            button.GetComponentInChildren<TextMeshProUGUI>().text = option.Phrase;
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

    private void UpdateWealthCultMeter(int value)
    {
        Debug.Log(value);
        _cultWealthImage.fillAmount = (float)value / (float)m_GameSession.Cult.Max;
    }

    private void UpdateLoyaltyCultMeter(int value)
    {
        Debug.Log(value);
        _cultLoyaltyImage.fillAmount = (float)value / (float)m_GameSession.Cult.Max;
    }

    private void UpdateReputationCultMeter(int value)
    {
        Debug.Log(value);
        _cultReputationImage.fillAmount = (float)value / (float)m_GameSession.Cult.Max;
    }
}
