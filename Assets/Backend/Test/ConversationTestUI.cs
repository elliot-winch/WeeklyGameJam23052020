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
    private TextMeshProUGUI m_CandidateSpeechText;
    [SerializeField]
    private GameSession m_GameSession;

    private void Awake()
    {
        m_GameSession.Conversation.Subscribe(SetupConversation);   
    }

    private void SetupConversation(Conversation conversation)
    {
        if(conversation != null)
        {
            m_CandidateNameText.text = conversation.Candidate.FirstName;
            conversation.Choice.Subscribe(SetupChoice);
        }
    }

    private void SetupChoice(Choice choice)
    {
        ClearChoiceButtons();

        m_CandidateSpeechText.text = choice.Phrase;

        foreach(Option option in choice.Options)
        {
            Button button = Instantiate(m_ButtonPrefab, m_ButtonParent);
            button.GetComponentInChildren<Text>().text = option.Phrase;
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
