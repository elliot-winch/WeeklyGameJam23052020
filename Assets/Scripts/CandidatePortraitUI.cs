using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CandidatePortraitUI : MonoBehaviour
{
    [SerializeField]
    private Image[] m_Portraits;
    [SerializeField]
    private GameSession m_GameSession;

    private Image m_CurrentImage;

    private void Awake()
    {
        m_GameSession.Candidate.Subscribe((candidate) => SetPortrait(candidate));
        m_GameSession.Conversation.Subscribe(SetupConversation);
    }

    private void SetupConversation(Conversation conversation)
    {
        if (conversation != null)
        {
            conversation.PersuasionLevelDelta.Subscribe((persuasionDelta) => SetPortrait(m_GameSession.Candidate.Value, persuasionDelta));
        }
    }

    private void SetPortrait(Candidate candidate, int choiceScore = 0)
    {
        m_CurrentImage?.gameObject.SetActive(false);

        if (candidate == null)
        {
            return;
        }

        string portraitID = GetPortraitID(candidate, choiceScore);

        Debug.Log(portraitID);

        m_CurrentImage = m_Portraits.FirstOrDefault(image => image.name == portraitID);

        m_CurrentImage?.gameObject.SetActive(true);
    }

    private string GetPortraitID(Candidate candidate, int choiceScore)
    {
        if(candidate.Portraits != null)
        {
            //Find the first scoreToActivate that exceeds the choiceScore
            string portraitID = candidate.Portraits
                .OrderByDescending(portrait => portrait.ScoreToActivate)
                .FirstOrDefault(portrait => portrait.ScoreToActivate >= choiceScore).ImageID;

            if(string.IsNullOrWhiteSpace(portraitID) == false)
            {
                return portraitID;
            }
        }

        return candidate.DefaultPortrait?.ImageID;
    }
}
