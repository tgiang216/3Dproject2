using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private int _maxZombieCount;
    [SerializeField] private float _cooldown;
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private float _radius;

    private int _zombieCount;

    private void Start() => StartCoroutine(SpawnZombiesByTime());

    private IEnumerator SpawnZombiesByTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_cooldown);
            if (_zombieCount < _maxZombieCount)
            {
                SpawnZombie();
            }
        }
    }

    private void SpawnZombie()
    {
        GameObject zombie = Instantiate(_zombiePrefab, RandomPosition(), transform.rotation);
        //zombie.GetComponent<Health>().Dead.AddListener(() => _zombieCount--);
        _zombieCount++;
    }

    private Vector3 RandomPosition()
    {
        Vector2 offset = Random.insideUnitCircle * _radius;
        return transform.position + new Vector3(offset.x, 0, offset.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
