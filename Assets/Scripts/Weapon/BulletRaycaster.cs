using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycaster : MonoBehaviour
{
    [SerializeField] private Transform _aimingCamera;
    [SerializeField] private GameObject _hitMarkerPrefab;
    [SerializeField] private int _damage;

    public void AutomaticRifle_BulletShot()
    {
        var aimingRay = new Ray(_aimingCamera.position, _aimingCamera.forward);
        if (Physics.Raycast(aimingRay, out RaycastHit hitInfo))
        {
            CreateHitEffect(hitInfo);
            DeliverDamage(hitInfo.collider);
        }
    }

    private void CreateHitEffect(RaycastHit hitInfo)
    {
        Quaternion rotation = Quaternion.LookRotation(hitInfo.normal);
        GameObject effect = Instantiate(_hitMarkerPrefab, hitInfo.point, rotation);
        effect.transform.SetParent(hitInfo.collider.transform);
    }

    private void DeliverDamage(Collider collider)
    {
        //Health victim = collider.GetComponentInParent<Health>();
        //if (victim != null)
        //{
        //    victim.TakeDamage(_damage);
        //}
    }
}
