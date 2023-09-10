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
    public float defaultRate = 20f;

    
    public float maxRangeToPlayer;

    private float getHitAnimRate;
    private Transform hipsBoneTranform;
    private Transform playerTransform;

    private bool IsDead = false;
    [SerializeField] private StateId currentState;

    public bool IsPlayerInRange => Vector3.Distance(playerTransform.position, transform.position) < maxRangeToPlayer;
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
        stateMachine.RegisterState(new RagdollState());
        stateMachine.RegisterState(new StandUpState());
        stateMachine.RegisterState(new WalkState());
        stateMachine.ChangeState(initialState);

        getHitAnimRate = defaultRate;
        hipsBoneTranform = animator.GetBoneTransform(HumanBodyBones.Hips);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public void Update()
    {   
        stateMachine.Update();
        currentState = stateMachine.currentState;
        //animator.SetFloat("Speed", navMeshAgent.speed);
        CountDamageInTime();
        DetectPlayer();


    }

    public void OnDieEvent()
    {
        stateMachine.ChangeState(StateId.Death);
        IsDead = true;
    }
    private float totalDamageInSecond = 0;
    private float damageCountTimer = 0;
    private bool damageIsCounted = false;
    public void OnTakeDamageEvent(float damage)
    {
        totalDamageInSecond += damage;
        damageIsCounted = true;
        //Debug.Log("CountDamageInTime() " + CountDamageInTime());
        if (IsDead) return;
        if(CountDamageInTime() >= 10f) this.stateMachine.ChangeState(StateId.Ragdoll);

        float rate = Random.Range(0f, 100f);
        if(rate < getHitAnimRate)
        {
            //animator.SetTrigger("FallFont");
            getHitAnimRate = defaultRate;
        }
        else
        {
            getHitAnimRate++;
        }
    }
     private void DetectPlayer()
    {
        if ((int)stateMachine.currentState == (int)StateId.Ragdoll) return;
        if ((int)stateMachine.currentState == (int)StateId.StandUp) return;
        if ((int)stateMachine.currentState == (int)StateId.Death) return;
        if ((int)stateMachine.currentState == (int)StateId.ChasePlayer) return;
        
        
        if (IsPlayerInRange)
        {
            stateMachine.ChangeState(StateId.ChasePlayer);
        }
    }
     public float CountDamageInTime()
    {
        if(damageIsCounted)
        {
            damageCountTimer += Time.deltaTime;
            if(damageCountTimer >= 0.5f)
            {
                damageIsCounted = false;
                totalDamageInSecond = 0f;
                damageCountTimer = 0f;
            }
        }
        return totalDamageInSecond;
    }

    [SerializeField] private LayerMask mask;
    public void UpdatePositionToHipsBone()
    {
        Vector3 originalPos = transform.position;
        transform.position = hipsBoneTranform.position;
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo,100f, mask))
        {
            Vector3 newPos = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
            transform.position = newPos;
            //Debug.Log("Position " + newPos + "Hit point " );
        }
        hipsBoneTranform.position = originalPos;
    }
    
}
