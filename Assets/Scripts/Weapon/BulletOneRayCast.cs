using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOneRayCast : BulletRaycaster
{
    public override void BulletShot()
    {
        var aimingRay = new Ray(_aimingCamera.position, _aimingCamera.forward);
        if (Physics.Raycast(aimingRay, out RaycastHit hitInfo))
        {
            CreateHitEffect(hitInfo);
            DeliverDamage(hitInfo);
        }
    }
}
