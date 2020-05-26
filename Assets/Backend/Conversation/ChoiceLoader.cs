using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceLoader : MonoBehaviour
{
    [SerializeField]
    private string m_FilePath;

    public ChoicePool ChoicePool { get; private set; }

    public void Load()
    {
        ChoicePool = JSONLoader.LoadFromFile<ChoicePool>(m_FilePath);
    }
}
