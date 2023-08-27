using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunListMoveCtrl : MonoBehaviour
{
    [SerializeField] private Transform[] guns;

    [SerializeField] private float _radius;
    [SerializeField] private float _angle;

    private void Start()
    {
        //guns[1].transform.position = transform.position + new Vector3(Mathf.Cos(30f), Mathf.Sin(30f), 0) * 0.5f;
        guns[1].transform.position = GetPositionOnCircle(_radius, _angle);
    }
    private Vector3 GetPositionOnCircle(float radius, float angle)
    {
        return transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
       
    }
}
