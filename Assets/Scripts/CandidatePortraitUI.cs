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
        Debug.Log("Set Portrait: " + candidate?.FirstName + " score" + choiceScore);

        m_CurrentImage?.gameObject.SetActive(false);

        if (candidate == null)
        {
            return;
        }

        string portraitID = GetPortraitID(candidate, choiceScore);

        m_CurrentImage = m_Portraits.FirstOrDefault(image => image.name == portraitID);

        m_CurrentImage?.gameObject.SetActive(true);
    }

    private string GetPortraitID(Candidate candidate, int choiceScore)
    {
        if(candidate.Portraits != null)
        {
            //Find the first scoreToActivate that exceeds the choiceScore
            Portrait portrait = candidate.Portraits
                .OrderByDescending(p => p.ScoreToActivate)
                .FirstOrDefault(p => p.ScoreToActivate <= choiceScore);

            if(portrait != null)
            {
                return portrait.ImageID;
            }
        }

        return candidate.DefaultPortrait?.ImageID;
    }
}
