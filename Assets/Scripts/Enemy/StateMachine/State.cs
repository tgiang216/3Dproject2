using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateId
{
    ChasePlayer,
    Death,
    Idle,
    Ragdoll,
    StandUp,
    Walk,
    Attack
}
public interface State
{
    public StateId GetId();
    public void Enter(AiAgent agent);
    public void Update(AiAgent agent);
    public void Exit(AiAgent agent);
}
