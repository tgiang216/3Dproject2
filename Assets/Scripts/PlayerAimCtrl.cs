using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimCtrl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField] private float _normalSensitivity;
    [SerializeField] private float _aimSensitivity;
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
            _aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensivity(_aimSensitivity);
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensivity(_normalSensitivity);
        }
    }

    public void Test(Input value)
    {
        
    }
}
