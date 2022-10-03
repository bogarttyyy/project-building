using System;
using UnityEngine;

public class ThinkingState : IAIState
{
    public IAIState DoState(AIBrain aiObject)
    {
        Debug.Log("State Changed: Thinking");
        GoThink(aiObject);
        Debug.Log("State Changed: Collect");
        return aiObject.collectState;
    }

    private void GoThink(AIBrain aiObject)
    {
        aiObject.ThinkForAWhile();
    }
}