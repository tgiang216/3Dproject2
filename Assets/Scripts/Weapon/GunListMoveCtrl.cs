using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class GunListMoveCtrl : MonoBehaviour
{
    [SerializeField] private Transform[] guns;

    [SerializeField] private float _radius;
    [SerializeField] private float _angle;

    [SerializeField] PathType pathType = PathType.CatmullRom;
    [SerializeField] Vector3[] waypoints;
    private void Start()
    {
        //MoveAround(guns[0].transform, 1f);
        for (int i = 0; i < guns.Length; i++)
        {
            
        }
    }
    private Vector3 GetPositionOnCircle(float radius, float angle)
    {
        return transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
       
    }

    private void MoveAround(Transform target, float radius)
    {
        Tween t = target.DOPath(waypoints, 7, pathType)
            .SetOptions(true);
            
        // Then set the ease to Linear and use infinite loops
        t.SetEase(Ease.Linear).SetLoops(-1);
    }

    private Transform[] GetCirclePathList(float radius, int listLength, Vector3 center)
    {
        Transform[] transforms = new Transform[listLength];

        return transforms;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            //Handles.Label(waypoints[i], i.ToString());
            Gizmos.color = Color.green;
            //Gizmos.DrawSphere(waypoints[i], 0.2f);
        }
    }
}
