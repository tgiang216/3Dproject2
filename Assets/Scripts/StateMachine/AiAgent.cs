using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public StateMachine stateMachine;
    public StateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Animator animator;

    public Ragdoll ragdoll;
    public SkinnedMeshRenderer mesh;

    private float getHitAnimRate;
    public float defaultRate = 20f;

    void Start()
    {
        ragdoll= GetComponent<Ragdoll>();
        mesh= GetComponent<SkinnedMeshRenderer>();
        animator= GetComponent<Animator>();
        navMeshAgent= GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new ChasePlayerState());
        stateMachine.RegisterState(new DeathState());
        stateMachine.RegisterState(new IdleState());
        stateMachine.ChangeState(initialState);

        getHitAnimRate = defaultRate;
    }

    // Update is called once per frame
    public void Update()
    {
        stateMachine.Update();
        animator.SetFloat("Speed", navMeshAgent.speed);
    }

    public void OnDieEvent()
    {
        stateMachine.ChangeState(StateId.Death);
    }
    public void OnTakeDamageEvent(float damage)
    {
        Debug.Log("Take damage " + damage);
        
        float rate = Random.Range(0f, 100f);
        if(rate < getHitAnimRate)
        {
            animator.SetTrigger("GetHit");
            getHitAnimRate = defaultRate;
        }
        else
        {
            getHitAnimRate++;
        }
    }
}
