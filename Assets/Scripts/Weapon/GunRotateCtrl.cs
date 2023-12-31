using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunRotateCtrl : MonoBehaviour
{
    [SerializeField] private PlayerAimCtrl _aimPosition;
    //[SerializeField] private InputActionReference _shootAction;
    //[SerializeField] private InputActionReference _aimAction;
    [SerializeField] private StarterAssetsInputs _input;
    //private bool IsShootButtonPressed() => _shootAction.action.ReadValue<float>() != 0;
    //private bool IsAimButtonPressed() => _aimAction.action.ReadValue<float>() != 0;

    // Update is called once per frame
    private void Start()
    {
        _aimPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAimCtrl>();
        //_targetFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        UpdateGunRotate();
    }

    private void UpdateGunRotate()
    {
        if (_input.look == Vector2.zero) return;
        Vector3 dir = _aimPosition.GetMouseWorldPosistion() - transform.position;
        Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
        transform.rotation = nextRotation;

        //if (IsShootButtonPressed() || IsAimButtonPressed())
        //{
        //    //transform.LookAt(_aimPosition.GetMouseWorldPosistion());
        //    Vector3 dir = _aimPosition.GetMouseWorldPosistion() - transform.position;
        //    Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
        //    transform.rotation = nextRotation;
        //}
        //else
        //{
        //    Quaternion nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_targetFollow.forward), Time.deltaTime * 5f);
        //    transform.rotation = nextRotation;
          
        
    }
}
