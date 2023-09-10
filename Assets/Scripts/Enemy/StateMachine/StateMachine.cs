using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State[] states;
    public AiAgent agent;
    public StateId currentState;

    public StateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(StateId)).Length;
        states = new State[numStates];
    }
    public void RegisterState(State state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public State GetState(StateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }
    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }
    public void ChangeState(StateId newState)
    {
        Debug.LogWarning("Current State "+ currentState + " ------- " + "New State : "+ newState);
        if ((int)currentState!= (int)StateId.Ragdoll && (int)newState == (int)currentState) return;
        //Debug.LogWarning("");
        GetState(currentState)?.Exit(agent);
        Debug.LogWarning("Current State " + currentState + " Change to " + "New State : " + newState);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
        

    }
}
