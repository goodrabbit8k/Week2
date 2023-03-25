using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keterangan : Saya menggunakan fungsi Range untuk mengatur spawn change pada bomb. Dengan ini saya bisa menentukan kemungkinan spawn
                yang pas pada gameplay
*/

public class SpawnManager : MonoBehaviour
{
    public GameObject bombPrefabs;

    [Range(0f, 1f)]  public float bombSpawnChance = 0.05f;

    [SerializeField] GameObject[] fruitPrefabs;

    BoxCollider spawnPoint;

    [SerializeField] float maximumSpawnDelay = 1f;
    [SerializeField] float minimumSpawnDelay = 1f;
    [SerializeField] float maximumAngle = 10f;
    [SerializeField] float minimumAngle = -10f;
    [SerializeField] float maximumForce = 20f;
    [SerializeField] float minimumForce = 18f;
    [SerializeField] float maximumLifeTime = 3f;

    void Start()
    {
        spawnPoint = GetComponent<BoxCollider>();     
    }

    void OnEnable()
    {
        StartCoroutine(Spawning());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(2);

        while (enabled)
        {
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            Vector3 position = GenerateRandomSpawnPosition();
            Quaternion rotation = GenerateRandomSpawnRotation();
            float force = GenerateRandomForce();

            if (Random.value < bombSpawnChance)
            {
                prefab = bombPrefabs;
            }

            GameObject fruit = Instantiate(prefab, position, rotation);

            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            Destroy(fruit, maximumLifeTime);

            yield return new WaitForSeconds(Random.Range(minimumSpawnDelay, maximumSpawnDelay));
        }
    }

    Vector3 GenerateRandomSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = Random.Range(spawnPoint.bounds.min.x, spawnPoint.bounds.max.x);
        spawnPosition.y = Random.Range(spawnPoint.bounds.min.y, spawnPoint.bounds.max.y);
        spawnPosition.z = Random.Range(spawnPoint.bounds.min.z, spawnPoint.bounds.max.z);

        return spawnPosition;
    }

    Quaternion GenerateRandomSpawnRotation()
    {
        Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(minimumAngle, maximumAngle));

        return spawnRotation;
    }

    float GenerateRandomForce()
    {
        float spawnForce = Random.Range(minimumForce, maximumForce);

        return spawnForce;
    }
}
