using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation 
{
    public Candidate Candidate { get; private set; }

    public Choice CurrentChoice { get; private set; }

    public Conversation(Candidate candidate, Choice opening)
    {
        Candidate = candidate;
        CurrentChoice = opening;
    }

    public void SelectOption(Option option)
    {
        CurrentChoice = GetNextChoice(option);
    }

    private Choice GetNextChoice(Option option)
    {

    }
}
