using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolderFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _smooth;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private Vector3 _aimDirection;

    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency = 1f;
    private float DistanceToPlayer => Vector3.Distance(transform.position, _player.position);
    void Start()
    {
        
        this.transform.parent = null;
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.LookAt(_player.position);
        transform.forward = _player.forward;
       
        if(DistanceToPlayer > _minDistance)
        {
            FollowPlayer();
        }
        Hovering();
    }

    private void Hovering()
    {
        float y = Mathf.Cos(Time.time * _frequency) * _amplitude + transform.position.y;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    private void FollowPlayer()
    {
        Vector3 target = new Vector3(_player.position.x, transform.position.y, _player.position.z) + _offset;
        //this.transform.position = transform.Tr(transform.position, target, _speed * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, _smooth * Time.deltaTime);
    }
}
