using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1f;
    public float maxDistance = 1f;
    public float dieForce = 10f;
}
