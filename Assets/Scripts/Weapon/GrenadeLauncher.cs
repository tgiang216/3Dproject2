using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firingPos;
    [SerializeField] private float _launchingForce;

    public void OnShoot(CallbackContext context)
    {
        if (context.performed && gameObject.activeInHierarchy)
        {
            GameObject bullet = 
                Instantiate(_bulletPrefab, _firingPos.position, _firingPos.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(
                _firingPos.forward * _launchingForce, ForceMode.Impulse);
        }
    }
}
