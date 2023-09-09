using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHitForceCtrl : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Ragdoll _ragdoll;
    [SerializeField] private float _hitForce;
    //[SerializeField] private float _upForce;

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
        _ragdoll = this.GetComponent<Ragdoll>();
    }
    public void ApplyForce(Vector3 hitDirection, Vector3 hitPosition)
    {
        //var rigidBody = _animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        Rigidbody rigidbody = _ragdoll.GetRigitbody(hitDirection);
        //Vector3 upDirection = Vector3.up * _upForce;
        hitDirection.y = 1;
        hitDirection.Normalize();
        Debug.Log("hit direction" + hitDirection);
        rigidbody.AddForceAtPosition(hitDirection * _hitForce, hitPosition, ForceMode.Impulse);
        //rigidbody.AddForce(forceFirection * _hitForce, ForceMode.VelocityChange);

        //Debug.Log("vang vang " + hitDirection);
    }

}
