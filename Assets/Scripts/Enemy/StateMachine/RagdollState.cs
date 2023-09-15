using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollState : State
{
    private float standUpTime ;
    private float eslapeTimeResetBone = 0f;
    private bool IsResetting;
    BoneTransform[] standUpBoneTransform;
    public void Enter(AiAgent agent)
    {
        //Debug.Log("Enter Standup State");  
        agent.ragdoll.EnableRagdoll();
        agent.navMeshAgent.speed = 0;
        standUpTime = Random.Range(2.0f, 5.0f);
        eslapeTimeResetBone = 0f;
        IsResetting= false;

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
           if(!IsResetting)
            {            
                agent.UpdateRotationToHipsBone();
                agent.UpdatePositionToHipsBone();
                agent.PopulateBoneTransforms(agent.ragdollBoneTransforms);
                IsResetting = true;
                Debug.Log("Update bone before");
                standUpBoneTransform = agent.GetStandUpBoneTransforms();
            }
            
            //agent.ragdoll.DisableRagdoll() ;
            //agent.animator.Play("Stand up");
            
            ResettingBones(agent);                     
        }
    }

    private void ResettingBones(AiAgent agent)
    {
        Debug.Log("Update bone after");
        //Debug.Log("ragdoll lenght" + agent.ragdollBoneTransforms.Length);
        //Debug.Log("standup lenght" + agent.standUpBoneTransforms.Length);
        eslapeTimeResetBone += Time.deltaTime;
        float elapsedPercentage = eslapeTimeResetBone / agent.timeToResetBones;

        

        for (int i = 0; i < agent.bones.Length; i++)
        {
            agent.bones[i].localPosition = Vector3.Lerp(
                agent.ragdollBoneTransforms[i].Position,
                standUpBoneTransform[i].Position
                , elapsedPercentage);

            agent.bones[i].localRotation = Quaternion.Lerp(
                agent.ragdollBoneTransforms[i].Rotation,
                standUpBoneTransform[i].Rotation,
                elapsedPercentage);
        }
        if(elapsedPercentage >= 1)
        {
            eslapeTimeResetBone = 0f;
            agent.stateMachine.ChangeState(StateId.StandUp);
        }
    }
}
