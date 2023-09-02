using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public StateId GetId()
    {
        return StateId.Death;
    }
    public void Enter(AiAgent agent)
    {
        agent.ragdoll.EnableRagdoll();
        
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public void Update(AiAgent agent)
    {
        
    }
}
