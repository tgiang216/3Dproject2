using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState : State
{
    private float _originSpeed;
    public void Enter(AiAgent agent)
    {
        //_originSpeed = agent.navMeshAgent.speed;
        agent.UpdatePositionToHipsBone();
        agent.ragdoll.DisableRagdoll();
        //agent.animator.Play("Face Down Stand Up");
        agent.animator.SetTrigger("StandUp");
        agent.navMeshAgent.enabled= false;
    }

    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.enabled = true;
    }

    public StateId GetId()
    {
        return StateId.StandUp;
    }

    public void Update(AiAgent agent)
    {
        if(agent.animator.GetCurrentAnimatorStateInfo(0).IsName("Face Down Stand Up")){
            agent.stateMachine.ChangeState(StateId.Walk);
            //agent.navMeshAgent.speed = _originSpeed;
        }
    }
}
