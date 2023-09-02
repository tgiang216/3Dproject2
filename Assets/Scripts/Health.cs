using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxHealthPoint;
    
    [SerializeField] private float _healthPoint;

    [SerializeField] InputActionReference _takeDamageAction;
    


    float _blinkTimer;
    public UnityEvent<float> DamageTaken;
    public UnityEvent Dead;
    public UnityEvent<float, float> HpChanged;
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
        //_skinnedMesh= GetComponent<SkinnedMeshRenderer>();
    }
    private void Update()
    {
       
    }
    public void TakeDamage(float damage)
    {
        
        HealthPoint -= damage;
        if(IsDead)
        {
            Dead.Invoke();
        }
        
    }

    
}
