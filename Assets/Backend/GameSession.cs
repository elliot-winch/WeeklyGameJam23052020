using Newtonsoft.Json;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private CandidateFactory m_CandidateFactory;

    private Conversation m_Conversation;

    private void Awake()
    {
        m_CandidateFactory.Load();

    }

    private void Update()
    {
        //Testing
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(JsonConvert.SerializeObject(m_CandidateFactory.Generate()));
        }
    }
}
