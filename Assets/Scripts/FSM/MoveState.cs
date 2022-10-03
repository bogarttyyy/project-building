public class MoveState : IAIState
{
    public IAIState DoState(AIBrain aiObject)
    {
        return aiObject.moveState;
    }

    
}