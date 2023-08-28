using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimCtrl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField] private float _normalSensitivity;
    [SerializeField] private float _aimSensitivity;
    [SerializeField] private LayerMask _aimlayerMask = new LayerMask();
    //[SerializeField] private Transform debugTransform;
    private StarterAssetsInputs starterAssetsInput;
    private ThirdPersonController thirdPersonController;
    private void Awake()
    {
        starterAssetsInput= GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (starterAssetsInput.aim)
        {
            StartAim();
        } else
        {
            StopAim();
        }             
    }

    public Vector3 GetMouseWorldPosistion()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimlayerMask))
        {
            //debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        return mouseWorldPosition;
    }    
    
    private void StartAim()
    {
        _aimVirtualCamera.gameObject.SetActive(true);
        thirdPersonController.SetSensivity(_aimSensitivity);
        thirdPersonController.SetRotateOnMove(false);

        Vector3 worldAimTarget = GetMouseWorldPosistion();
        worldAimTarget.y = transform.position.y;
        Vector3 aimDiection = (worldAimTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDiection, Time.deltaTime * 20f);
    }
    private void StopAim()
    {
        _aimVirtualCamera.gameObject.SetActive(false);
        thirdPersonController.SetSensivity(_normalSensitivity);
        thirdPersonController.SetRotateOnMove(true);
    }
}
