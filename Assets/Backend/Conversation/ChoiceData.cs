﻿using System;

[Serializable]
public class ChoicePool
{
    public Choice[] Choices;
}

[Serializable]
public class Choice
{
    public string ID;
    //What the candidate will say to the player
    public string Phrase;
    public Option[] Options;
}

[Serializable]
public class Option
{
    //What the player will say to the candidate
    public string Phrase;

    //The worst case scenario
    public string DefaultChoiceID;
    //Choices reached when a minimum score is suppased
    public AttainableChoice[] AttainableChoices;
}

[Serializable]
public class AttainableChoice
{
    public int ScoreRequired;
    public string ChoiceID;
}