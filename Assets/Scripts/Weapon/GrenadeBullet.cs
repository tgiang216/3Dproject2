using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private LayerMask _hitLayers;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _hitMask;

    private Vector3 _lastPosition;
    private bool _isExploded;

    private void Start() => _lastPosition = transform.position;

    private void Update()
    {
        if (Physics.Linecast(_lastPosition, transform.position, out RaycastHit hitInfo,
            _hitLayers))
        {
            Explode(hitInfo.point);
        }
    }

    private void OnTriggerEnter(Collider collider) => Explode(transform.position);

    private void Explode(Vector3 position)
    {
        if (_isExploded) return;

        _isExploded = true;
        CreateExplosionEffect(position);
        Collider[] victims = Physics.OverlapSphere(position, _explosionRadius, _hitMask);
        DeliveryDamage(victims, position);
        BlowUp(victims, position);
        Destroy(gameObject);
    }

    private void CreateExplosionEffect(Vector3 position)
    {
        Quaternion rotation = Quaternion.LookRotation(-transform.forward);
        position -= transform.forward * 1f;
        Instantiate(_explosionPrefab, position, rotation);
    }

    private void DeliveryDamage(Collider[] victims, Vector3 position)
    {        
        //List<Health> hitList = new();
        //for (int i = 0; i < victims.Length; i++)
        //{
        //    var enemyHealth = victims[i].GetComponentInParent<Health>();
        //    if (enemyHealth != null && !hitList.Contains(enemyHealth))
        //    {
        //        enemyHealth.TakeDamage(_damage);
        //        hitList.Add(enemyHealth);
        //    }
        //}
    }

    private void BlowUp(Collider[] victims, Vector3 position)
    {
        for (int i = 0; i < victims.Length; i++)
        {
            if (victims[i].TryGetComponent(out Rigidbody rigid))
            {
                rigid.AddExplosionForce(_explosionForce, position, _explosionRadius, 
                    2f, ForceMode.Impulse);
            }
        }
    }

}
