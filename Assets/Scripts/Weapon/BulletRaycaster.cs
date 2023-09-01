using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycaster : MonoBehaviour
{
    [SerializeField] protected Transform _aimingCamera;
    [SerializeField] protected GameObject _hitMarkerPrefab;
    [SerializeField] protected int _damage;

    public virtual void BulletShot()
    {
        
    }

    

    protected void CreateHitEffect(RaycastHit hitInfo)
    {
        Quaternion rotation = Quaternion.LookRotation(hitInfo.normal);
        GameObject effect = Instantiate(_hitMarkerPrefab, hitInfo.point, rotation);
        effect.transform.SetParent(hitInfo.collider.transform);
    }

    protected void DeliverDamage(Collider collider)
    {
        //Health victim = collider.GetComponentInParent<Health>();
        //if (victim != null)
        //{
        //    victim.TakeDamage(_damage);

        //}
    }
}
