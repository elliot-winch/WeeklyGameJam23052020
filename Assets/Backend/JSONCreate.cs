using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONCreate : MonoBehaviour
{
    private void Start()
    {
        ChoicePool pool = new ChoicePool
        {
            Choices = new Choice[]
            {
                new Choice
                {
                    ID = "1",
                    Phrase = "Hello there",
                    Options = new Option[]
                    {
                        new Option
                        {
                            Phrase = "Hi, how are you?",
                            DefaultChoiceID = "2",
                            AttainableChoices = new AttainableChoice[]
                            {
                                new AttainableChoice()
                                {
                                    ScoreRequired = 10,
                                    ChoiceID = "3"
                                }
                            }
                        },
                        new Option
                        {
                            Phrase = "You better join this cult, bitch",
                            DefaultChoiceID = "4",
                            AttainableChoices = new AttainableChoice[]
                            {
                                new AttainableChoice()
                                {
                                    ScoreRequired = 10,
                                    ChoiceID = "5"
                                }
                            }
                        },
                    } 
                },
                new Choice
                {
                    ID = "2",
                    Phrase = "I'm okay. I am concerned you guys are a cult.",
                    Options = new Option[]
                    {
                        new Option
                        {
                            Phrase = "We're absolutely a cult",
                            DefaultChoiceID = "6",
                            AttainableChoices = new AttainableChoice[]
                            {
                                new AttainableChoice()
                                {
                                    ScoreRequired = 10,
                                    ChoiceID = "3"
                                }
                            }
                        },
                        new Option
                        {
                            Phrase = "Why do you say that?",
                            DefaultChoiceID = "7",
                            AttainableChoices = new AttainableChoice[]
                            {
                                new AttainableChoice()
                                {
                                    ScoreRequired = 10,
                                    ChoiceID = "3"
                                }
                            }
                        },
                    }
                }
            }
        };

        Debug.Log(JsonConvert.SerializeObject(pool));
    }
}
