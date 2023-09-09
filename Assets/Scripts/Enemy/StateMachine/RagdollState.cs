using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollState : State
{
    private float standUpTime ;
    public void Enter(AiAgent agent)
    {
        Debug.Log("Enter Standup State");
        agent.ragdoll.EnableRagdoll();
        standUpTime = Random.Range(2.0f, 5.0f);
    }

    public void Exit(AiAgent agent)
    {
        //agent.UpdatePositionToHipsBone();
    }

    public StateId GetId()
    {
        return StateId.Ragdoll;
    }

    public void Update(AiAgent agent)
    {
        standUpTime -= Time.deltaTime;
        if(standUpTime < 0 )
        {
            //agent.UpdatePositionToHipsBone();
            //agent.ragdoll.DisableRagdoll() ;
            //agent.animator.Play("Stand up");
            
            agent.stateMachine.ChangeState(StateId.StandUp);                     
        }
    }
}
