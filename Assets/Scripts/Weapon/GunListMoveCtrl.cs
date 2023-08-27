using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunListMoveCtrl : MonoBehaviour
{
    [SerializeField] private Transform[] guns;

    [SerializeField] private float _radius;
    [SerializeField] private float _angle;

    [SerializeField] PathType pathType = PathType.CatmullRom;
    [SerializeField] Vector3[] waypoints = new[] {
		new Vector3(4, 2, 0),
		new Vector3(8, 6, 14),
		new Vector3(4, 6, 14),
		new Vector3(0, 6, 6),
		new Vector3(-3, 0, 0)

    }; 
    private void Start()
    {
        MoveAround(guns[1].transform, 1f);
    }
    private Vector3 GetPositionOnCircle(float radius, float angle)
    {
        return transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
       
    }

    private void MoveAround(Transform target, float radius)
    {
        Tween t = target.DOPath(waypoints, 4, pathType)
            .SetOptions(true)
            .SetLookAt(0.001f);
        // Then set the ease to Linear and use infinite loops
        t.SetEase(Ease.Linear).SetLoops(-1);
    }
}
