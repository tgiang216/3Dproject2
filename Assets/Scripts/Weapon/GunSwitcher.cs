using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;

    void Update()
    {
        if (Keyboard.current.digit1Key.isPressed)
        {
            ActivateGun(0);
        }

        if (Keyboard.current.digit2Key.isPressed)
        {
            ActivateGun(1);
        }
    }

    private void ActivateGun(int index)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(i == index);
        }
    }
}
