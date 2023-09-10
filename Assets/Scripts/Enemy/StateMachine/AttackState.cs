using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    
    public void Enter(AiAgent agent)
    {
        agent.animator.SetBool("IsAttack", true);
        agent.navMeshAgent.enabled= false;
    }

    public void Exit(AiAgent agent)
    {
        agent.animator.SetBool("IsAttack", false);
        agent.navMeshAgent.enabled = true;
    }

    public StateId GetId()
    {
        return StateId.Attack;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.IsPlayerInAttackRange)
        {
            agent.stateMachine.ChangeState(StateId.ChasePlayer);
        }
        agent.FaceToPlayer();
    }
}
