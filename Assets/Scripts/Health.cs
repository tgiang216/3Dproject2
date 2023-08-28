using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxHealthPoint;
    public UnityEvent<float> DamageTaken;
    public UnityEvent Dead;
    public UnityEvent<float, float> HpChanged;
    private float _healthPoint;

    [SerializeField] InputActionReference _takeDamageAction;
    public bool IsDead => HealthPoint <= 0;

    public float HealthPoint
    {
        get => _healthPoint;
        private set
        {
            _healthPoint = value;
            HpChanged.Invoke(_healthPoint, MaxHealthPoint);
        }
    }

    private void Start()
    {
        HealthPoint = MaxHealthPoint;
    }
    private void Update()
    {
        if (_takeDamageAction.action.IsPressed())
        {
            OnTakeDamage(10);
        }
    }
    public void OnTakeDamage(float damage)
    {
        if (IsDead) return;
        HealthPoint -= damage;
        if(IsDead)
        {
            Dead.Invoke();
        }
    }
}
