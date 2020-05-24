using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreGenerator
{
    int GetScore(Personality personality, Option option);
}
