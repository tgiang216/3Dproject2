using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunHolderFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _targetFollow;
    [ SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _smooth;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private PlayerAimCtrl _aimPosition;
   // [SerializeField] private bool _isTargetInAim;

    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency = 1f;


    [SerializeField] private InputActionReference _shootAction;
    private float DistanceToPlayer => Vector3.Distance(transform.position, _targetFollow.position);
    private bool IsShootButtonPressed() => _shootAction.action.ReadValue<float>() != 0;
    void Start()
    {
        
        this.transform.parent = null;
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.LookAt(_aimPosition.GetMouseWorldPosistion());
        //transform.forward = _targetFollow.forward;
       
        if(DistanceToPlayer > _minDistance)
        {
            FollowPlayer();
        }
        Hovering(); // lo lung
        //UpdateGunRotate(); // xoay dung theo ngam
    }
    private void UpdateGunRotate()
    {
        if (IsShootButtonPressed())
        {
            //transform.LookAt(_aimPosition.GetMouseWorldPosistion());
            Vector3 dir = _aimPosition.GetMouseWorldPosistion() - transform.position;
            Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
            transform.rotation = nextRotation;            
        }
        else
        {
            Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_targetFollow.forward), Time.deltaTime * 5f);
            transform.rotation = nextRotation;
            
        }
    }
    private void Hovering()
    {
        float y = Mathf.Cos(Time.time * _frequency) * _amplitude + transform.position.y;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    private void FollowPlayer()
    {
        Vector3 target = _targetFollow.position + _offset;
        //this.transform.position = transform.Tr(transform.position, target, _speed * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, _smooth * Time.deltaTime);
    }
}
