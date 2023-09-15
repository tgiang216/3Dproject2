using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState : State
{
    private float _originSpeed;
    public void Enter(AiAgent agent)
    {
        //_originSpeed = agent.navMeshAgent.speed;
        
        agent.ragdoll.DisableRagdoll();
        //agent.animator.Play("Face Down Stand Up");
        if(agent.isFacingUp)
        {
            agent.animator.SetTrigger("FaceUpStandUp");
        } else
        {
            agent.animator.SetTrigger("FaceDownStandUp");
        }
        
        agent.animator.SetFloat("Speed", 0f);
        agent.navMeshAgent.enabled= false;
        //Debug.Log("Disable navMesh in Stand up");
    }

    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.enabled = true;
        //Debug.LogError("Stand up exit ");
    }

    public StateId GetId()
    {
        return StateId.StandUp;
    }

    public void Update(AiAgent agent)
    {
        if(agent.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !agent.animator.IsInTransition(0))
        {
            //Debug.Log("Standing up " + agent.animator.GetCurrentAnimatorStateInfo(0));
            agent.stateMachine.ChangeState(StateId.Walk);
            //agent.navMeshAgent.speed = _originSpeed;
        }
    }
}
