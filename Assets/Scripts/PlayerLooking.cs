using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLooking : MonoBehaviour
{

    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private float _anglePerSecond;
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private float _smoothFactor;

    private float _pitchAngle;
    private Quaternion _desiredRotation = Quaternion.identity;
    private Quaternion _cameraDesiredRotation = Quaternion.identity;

    private float AdjustedSpeed => _anglePerSecond * _sensitivity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 rotateDirection = _lookAction.action.ReadValue<Vector2>();
        UpdateTurning(rotateDirection.x * AdjustedSpeed * Time.deltaTime);
        UpdatePitching(-rotateDirection.y * AdjustedSpeed * Time.deltaTime);
    }

    private void UpdateTurning(float deltaAngle)
    {
        _desiredRotation *= Quaternion.Euler(0, deltaAngle, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation,
            _smoothFactor * Time.deltaTime);
    }

    private void UpdatePitching(float deltaAngle)
    {
        _pitchAngle += deltaAngle;
        _pitchAngle = Mathf.Clamp(_pitchAngle, _minPitch, _maxPitch);
        _cameraDesiredRotation = Quaternion.Euler(_pitchAngle, 0, 0);

        _cameraHolder.localRotation = Quaternion.Lerp(
            _cameraHolder.localRotation, _cameraDesiredRotation, _smoothFactor * Time.deltaTime);
    }
}
