using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolderFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    

    private float DistanceToPlayer => Vector3.Distance(transform.position, _player.position);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.parent = null;
        //transform.LookAt(_player.position);
        transform.forward = _player.forward;
        if(DistanceToPlayer > _minDistance)
        {
            Vector3 target = new Vector3(_player.position.x, transform.position.y,_player.position.z) + _offset;
            //this.transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, target, _speed*Time.deltaTime);
        }
    }
}
