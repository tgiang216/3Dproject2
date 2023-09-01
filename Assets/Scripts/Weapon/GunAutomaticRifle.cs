using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GunAutomaticRifle : MonoBehaviour
{
    [SerializeField] private InputActionReference _shootAction;
    [SerializeField] private float _cooldownDuration;

    public UnityEvent BulletShot;

    private float _lastShotTime;

    private void Update()
    {
        if (IsShootButtonPressed() && Time.time - _lastShotTime >= _cooldownDuration)
        {
            Shoot();
            _lastShotTime = Time.time;
        }
    }

    private bool IsShootButtonPressed() => _shootAction.action.ReadValue<float>() != 0;

    private void Shoot() => BulletShot.Invoke();
}

