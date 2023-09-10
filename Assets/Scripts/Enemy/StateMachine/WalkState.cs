using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    private Vector3 _walkDirection;
    private float WalkSpeed = 0f;
    public void Enter(AiAgent agent)
    {
        WalkSpeed = Random.Range(0.2f, 1.5f);
        agent.navMeshAgent.speed = WalkSpeed;
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public StateId GetId()
    {
        return StateId.Walk;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }
        
        if (!agent.navMeshAgent.hasPath)
        {
            
            Vector3 destination = new Vector3(agent.transform.position.x, agent.transform.position.y, Vector3.forward.z + 100F);
            //Debug.Log("Set destination : "+ destination);
            agent.navMeshAgent.destination = agent.transform.position - Vector3.forward * 300f;
            
            
        }
        agent.animator.SetFloat("Speed", agent.navMeshAgent.speed);
    }
}
