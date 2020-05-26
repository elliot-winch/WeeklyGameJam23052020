using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        }
    }

    private void SetupConversation(Conversation conversation)
    {
        if(conversation != null)
        {
            conversation.Choice.Subscribe(SetupChoice);

            //TODO: Change hardcoded "20" to an actual max that the stats can be.

            _wealthMeter.SetCandidateStats(conversation.Candidate.Wealth, 20);
            _reputationMeter.SetCandidateStats(-10, -20);
            _loyaltyMeter.SetCandidateStats(conversation.Candidate.Loyality, 20);

        }
    }

    private void SetupConversationConclusion(ConversationConclusion conclusion)
    {
        m_ConversationConclusionText.text = conclusion?.String ?? "";
    }

    private void SetupChoice(Choice choice)
    {
        ClearChoiceButtons();

        m_CandidateSpeechText.text = choice.Phrase;

        foreach(Option option in choice.Options)
        {
            Button button = Instantiate(m_ButtonPrefab, m_ButtonParent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = option.Phrase;
            button.onClick.AddListener(() => { m_GameSession.Conversation.Value.SelectOption(option); });
        }
    }

    private void ClearChoiceButtons()
    {
        foreach(Button button in m_ButtonParent.GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }
    }
}
