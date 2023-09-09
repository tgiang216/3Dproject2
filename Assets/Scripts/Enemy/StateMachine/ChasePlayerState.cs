using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerState : State
{
    public Transform playerTransform;
    
    private float timer = 0f;
    public StateId GetId()
    {
        return StateId.ChasePlayer;
    }
    public void Enter(AiAgent agent)
    {
        if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }
    public void Update(AiAgent agent)
    {
        if (!agent.enabled) return;
        if (!agent.navMeshAgent.isActiveAndEnabled) return;
       
        timer -= Time.deltaTime;
        if(!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = playerTransform.position;
        }
        if(timer < 0f)
        {
            Vector3 direction = (playerTransform.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if(direction.sqrMagnitude > agent.config.maxDistance* agent.config.maxDistance)
            {
                if(agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = playerTransform.position;
                }
            }
            timer = agent.config.maxTime;
        }
    }
    public void Exit(AiAgent agent)
    {
        
    }

   

    
}
