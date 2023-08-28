using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void Health_OnHPChange(float hp, float maxHP)
    {
        _slider.value =(float)hp / maxHP;
    }
}
