using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffectOnGetHit : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
    [SerializeField] private float _blinkIntensity;
    [SerializeField] private float _blinkDuration;
    float _blinkTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void BlinkEffect()
    {
        if (_blinkTimer <= 0) return;
        _blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(_blinkTimer / _blinkDuration);
        float intesity = lerp * _blinkIntensity + 1f;
        _skinnedMesh.material.color = Color.white * intesity;
    }
}
