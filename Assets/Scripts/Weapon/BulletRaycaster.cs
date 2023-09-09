using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycaster : MonoBehaviour
{
    [SerializeField] protected Transform _aimingCamera;
    [SerializeField] protected GameObject _hitMarkerPrefab;
    [SerializeField] protected float _damage;
    [SerializeField] protected LayerMask _hitLayerMask;


    public virtual void BulletShot()
    {
        
    }

    

    protected void CreateHitEffect(RaycastHit hitInfo)
    {
        Quaternion rotation = Quaternion.LookRotation(hitInfo.normal);
        GameObject effect = Instantiate(_hitMarkerPrefab, hitInfo.point, rotation);
        effect.transform.SetParent(hitInfo.collider.transform);
    }

    protected void DeliverDamage(RaycastHit hitInfo)
    {
        Health victim = hitInfo.collider.GetComponentInParent<Health>();
        if (victim != null)
        {
            Vector3 hitDirection = (hitInfo.point - transform.position).normalized;
            victim.TakeDamage(_damage, hitDirection, hitInfo.point);
            hitInfo.rigidbody.AddForce(hitDirection*50f);
            hitInfo.transform.SendMessage("HitByRay",null,SendMessageOptions.DontRequireReceiver);
            //Debug.Log("Take damage " + _damage);
        }
    }
}
