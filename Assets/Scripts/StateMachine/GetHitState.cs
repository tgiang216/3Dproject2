using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : State
{
    public void Enter(AiAgent agent)
    {
        
    }

    public void Exit(AiAgent agent)
    {
        agent.stateMachine.ChangeState(StateId.Idle);
    }

    public StateId GetId()
    {
        return StateId.GetHit;
    }

    public void Update(AiAgent agent)
    {
        
    }
}
