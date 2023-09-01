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
    [SerializeField] private float _healthPoint;

    [SerializeField] InputActionReference _takeDamageAction;
    [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
    [SerializeField] private float _blinkIntensity;
    [SerializeField] private float _blinkDuration;
    float _blinkTimer;
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
       BlinkEffect();
    }
    public void TakeDamage(float damage)
    {
        _blinkTimer = _blinkDuration;
        if (IsDead) return;
        HealthPoint -= damage;
        if(IsDead)
        {
            Dead.Invoke();
        }
        
    }

    private void BlinkEffect()
    {
        if(_blinkTimer <= 0) return;
        _blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(_blinkTimer / _blinkDuration);
        float intesity = lerp* _blinkIntensity + 1f;
        _skinnedMesh.material.color = Color.white * intesity;
    }
}
