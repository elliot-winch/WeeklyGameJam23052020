using Newtonsoft.Json;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private CandidateFactory m_CandidateFactory;

    private void Awake()
    {
        //Testing:
        m_CandidateFactory.Load();

        /*
        Candidate c = new Candidate();

        Debug.Log(JsonConvert.SerializeObject(c));

        CandidatePool cp = new CandidatePool()
        {
            Candidates = new Candidate[]
            {
                c
            },
            FirstNames = new string[]
            {
                "Dave"
            },
            LastNames = new string[]
            {
                "Smith"
            },
            LoyalityBounds = new MinMax
            {
                Min = 0, Max = 100
            }
        };

        Debug.Log(JsonConvert.SerializeObject(cp));
        */
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
