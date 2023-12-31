using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadShot : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject _headModel;
    [SerializeField] private GameObject _effect;
    [SerializeField] private float _addDamage;
    //[SerializeField] private Collider collider;
    private bool headshot = false;

    public void HitByRay()
    {
        health.TakeDamage(_addDamage, Vector3.zero, Vector3.zero);
        if (health.IsDead && !headshot)
        {
            _headModel.SetActive(false);
            headshot = true;
            GameObject effect = Instantiate(_effect,transform.position, transform.rotation);
            //effect.transform.parent = transform;
            //this.gameObject.SetActive(false);
            //this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }


}
