using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState : State
{
    public void Enter(AiAgent agent)
    {
        agent.UpdatePositionToHipsBone();
        agent.ragdoll.DisableRagdoll();
        //agent.animator.Play("Face Down Stand Up");
        agent.animator.SetTrigger("StandUp");
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public StateId GetId()
    {
        return StateId.StandUp;
    }

    public void Update(AiAgent agent)
    {
        if(agent.animator.GetCurrentAnimatorStateInfo(0).IsName("Face Down Stand Up")){
            agent.stateMachine.ChangeState(StateId.Idle);
        }
    }
}
