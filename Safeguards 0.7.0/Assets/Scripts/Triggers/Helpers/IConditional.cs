using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConditional 
{
    //an exercise in named parameters and optional parameters
    bool CheckCondition(StatsManager user = null, StatsManager opponent = null, int value = -1);
    //add more optional parameters as needed
}
