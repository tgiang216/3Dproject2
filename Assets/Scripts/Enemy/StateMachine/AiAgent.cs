using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    
    public GameObject cube;
    public StateMachine stateMachine;
    public StateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Animator animator;

    public Ragdoll ragdoll;
    public SkinnedMeshRenderer mesh;   
    public float defaultRate = 20f;

    
    public float maxChaseRange;
    public float maxAttackRange;

    private float getHitAnimRate;
    private Transform hipsBoneTranform;
    private Transform playerTransform;

    [Header("Reset bone before standup")]
    public BoneTransform[] faceDownStandUpBoneTransforms;
    public BoneTransform[] ragdollBoneTransforms;
    public BoneTransform[] faceUpStandUpBoneTransforms;
    public Transform[] bones;
    public string faceDownStandUpClipName;
    public string faceUpStandUpClipName;
    public float timeToResetBones;
    public bool isFacingUp => hipsBoneTranform.up.y > 0;


    private bool IsDead = false;
    [SerializeField] private StateId currentState;

    public bool IsPlayerInChaseRange => Vector3.Distance(playerTransform.position, transform.position) <= maxChaseRange;
    public bool IsPlayerInAttackRange => Vector3.Distance(playerTransform.position, transform.position) <= maxAttackRange;
    private void Awake()
    {
        ragdoll = GetComponent<Ragdoll>();
        mesh = GetComponent<SkinnedMeshRenderer>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        hipsBoneTranform = animator.GetBoneTransform(HumanBodyBones.Hips);
        bones = hipsBoneTranform.GetComponentsInChildren<Transform>();
        faceDownStandUpBoneTransforms = new BoneTransform[bones.Length];
        faceUpStandUpBoneTransforms = new BoneTransform[bones.Length];
        ragdollBoneTransforms = new BoneTransform[bones.Length];
        Debug.Log("Bones lengh " + bones.Length);
        for (int i = 0; i < bones.Length; i++)
        {
            faceDownStandUpBoneTransforms[i] = new BoneTransform();
            faceUpStandUpBoneTransforms[i] = new BoneTransform();
            ragdollBoneTransforms[i] = new BoneTransform();
        }
        PopulateAnimationStartBoneTransform(faceDownStandUpClipName, faceDownStandUpBoneTransforms);
        PopulateAnimationStartBoneTransform(faceUpStandUpClipName, faceUpStandUpBoneTransforms);
        DrawDebug();
    }

    void Start()
    {
        
        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new ChasePlayerState());
        stateMachine.RegisterState(new DeathState());
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new RagdollState());
        stateMachine.RegisterState(new StandUpState());
        stateMachine.RegisterState(new WalkState());
        stateMachine.RegisterState(new AttackState());
        stateMachine.ChangeState(initialState);

        getHitAnimRate = defaultRate;
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public void Update()
    {   
        stateMachine.Update();
        currentState = stateMachine.currentState;
        //animator.SetFloat("Speed", navMeshAgent.speed);
        CountDamageInTime();
        Debug.Log(IsPlayerInAttackRange);
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
        if ((int)stateMachine.currentState == (int)StateId.Attack) return;

        if (IsPlayerInAttackRange)
        {
            if(Vector3.Distance(transform.position, playerTransform.position)< maxAttackRange -1f)
            {
                stateMachine.ChangeState(StateId.Attack);
            }
            
        }
        else
        if (IsPlayerInChaseRange)
        {
            stateMachine.ChangeState(StateId.ChasePlayer);
        }
        else
        {
            stateMachine.ChangeState(StateId.Walk);
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
        Vector3 originalPos = hipsBoneTranform.position;
        transform.position = hipsBoneTranform.position;

        Vector3 positionOffest = GetStandUpBoneTransforms()[0].Position;
        positionOffest.y = 0;
        positionOffest = transform.rotation*positionOffest;
        transform.position -= positionOffest;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo,100f, mask))
        {
            Vector3 newPos = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
            transform.position = newPos;
            //Debug.Log("Position " + newPos + "Hit point " );
        }
        hipsBoneTranform.position = originalPos;
    }
    public void UpdateRotationToHipsBone()
    {
        Vector3 originalPosition = hipsBoneTranform.position;
        Quaternion rotation = hipsBoneTranform.rotation;

        Vector3 desiredDirection = hipsBoneTranform.right * -1;
        if(isFacingUp)
        {
            desiredDirection *= -1;
        }
        desiredDirection.y = 0;
        desiredDirection.Normalize();

        Quaternion fromToRotation = Quaternion.FromToRotation(transform.forward,desiredDirection);
        transform.rotation *= fromToRotation;

        hipsBoneTranform.position = originalPosition;
        hipsBoneTranform.rotation = rotation;
    }
    public void FaceToPlayer()
    {
        Vector3 dir = playerTransform.position - transform.position;
        Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
        transform.rotation = nextRotation;
    }

    public void PopulateBoneTransforms(BoneTransform[] boneTransforms)
    {
        for (int i = 0; i < bones.Length; i++)
        {
            boneTransforms[i].Position = bones[i].localPosition;
            boneTransforms[i].Rotation= bones[i].localRotation;
        }
    }
    public void PopulateAnimationStartBoneTransform(string clipName, BoneTransform[] boneTransform)
    {
        Vector3 orginalPosition = transform.position;
        Quaternion orginalRotation = transform.rotation;
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if(clip.name == clipName)
            {
                Debug.Log(" get animation tranform");
                clip.SampleAnimation(gameObject, 0);
                PopulateBoneTransforms(boneTransform);
                break;
            }
        }
        transform.position = orginalPosition;
        transform.rotation = orginalRotation;
    }

    private void OnDrawGizmos()
    {
        
    }
    void DrawDebug()
    {
        Debug.Log("Draw debug");
        cube.transform.position = faceDownStandUpBoneTransforms[0].Position;
        for (int i = 0; i < faceDownStandUpBoneTransforms.Length - 1; i++)
        {
            Vector3 position = faceDownStandUpBoneTransforms[i].Position;
            Debug.Log(position);
            Debug.DrawLine(faceDownStandUpBoneTransforms[i].Position, faceDownStandUpBoneTransforms[i+1].Position,Color.red);
        }
    }

    public string GetStandUpStateName()
    {
        return null;
    }
    public BoneTransform[] GetStandUpBoneTransforms()
    {
        return isFacingUp ? faceUpStandUpBoneTransforms : faceDownStandUpBoneTransforms;
    }
}


public class BoneTransform
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
}