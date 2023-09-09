using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigids;
    [SerializeField] private float _hitForce;
 

    private void Start()
    {
        _animator = GetComponent<Animator>();
        CollectBones();
        DisableRagdoll();
    }

    [ContextMenu("Collect bones")]
    private void CollectBones() => _rigids = GetComponentsInChildren<Rigidbody>();

    [ContextMenu("Enable ragdoll")]
    public void EnableRagdoll()
    {
        
        SetRagdoll(true);
    }

    [ContextMenu("Disable ragdoll")]
    public void DisableRagdoll()
    {
        //_agent.UpdatePositionToHipsBone();
        SetRagdoll(false);
    }

    private void SetRagdoll(bool isEnable)
    {

        foreach (var rigid in _rigids)
        {
            rigid.isKinematic = !isEnable;
        }
        //Debug.Log("Set ragdoll " + isEnable);
        _animator.enabled = !isEnable;
    }
      
    public Rigidbody GetRigitbody(Vector3 position)
    {
        Rigidbody rigidbody = _rigids.OrderBy(rigidbody=> Vector3.Distance(rigidbody.position, position)).First();
        return rigidbody;
    }
}
