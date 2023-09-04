using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMultiRayCast : BulletRaycaster
{
    [SerializeField] private float _shotNumber = 7;
    [SerializeField] private float _shotArea = 0.05f;
    public override void BulletShot()
    {
        for (int i = 0; i < _shotNumber; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-_shotArea, _shotArea), Random.Range(-_shotArea, _shotArea), Random.Range(-_shotArea, _shotArea));
            var aimingRay = new Ray(_aimingCamera.position, _aimingCamera.forward + offset);
            if (Physics.Raycast(aimingRay, out RaycastHit hitInfo, 1000f, _hitLayerMask))
            {
                CreateHitEffect(hitInfo);
                DeliverDamage(hitInfo);
            }
        }
        
    }
}
