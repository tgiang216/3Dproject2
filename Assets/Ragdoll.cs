using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigids;
    [SerializeField] private float hitForce;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        CollectBones();
        DisableRagdoll();
    }

    [ContextMenu("Collect bones")]
    private void CollectBones() => _rigids = GetComponentsInChildren<Rigidbody>();

    [ContextMenu("Enable ragdoll")]
    public void EnableRagdoll() => SetRagdoll(true);

    [ContextMenu("Disable ragdoll")]
    public void DisableRagdoll() => SetRagdoll(false);

    private void SetRagdoll(bool isEnable)
    {
        foreach (var rigid in _rigids)
        {
            rigid.isKinematic = !isEnable;
        }
        _animator.enabled = !isEnable;
    }
    public void StopRagdoll()
    {
        StartCoroutine(StopRagdollInTime(1f));
    }

    public IEnumerator StopRagdollInTime(float time)
    {
        SetRagdoll(true);
        yield return new WaitForSeconds(time);
        SetRagdoll(false);
    }
    public void ApplyForce(Vector3 hitDirection)
    {
        var rigidBody = _animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(hitDirection * hitForce,ForceMode.VelocityChange);
        
        //Debug.Log("vang vang " + hitDirection);
    }
}
