using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] Transform startSpawnPoint;
    [SerializeField] Transform endSpawnPoint;
    public float spawnFrequency;

    public Color meteorColor;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            float randomX = Random.Range(startSpawnPoint.position.x, endSpawnPoint.position.x);
            GameObject meteor = Instantiate(meteorPrefab, new Vector2(randomX, startSpawnPoint.position.y), transform.rotation);
            meteor.GetComponent<SpriteRenderer>().color = meteorColor;
            meteor.GetComponentInChildren<TrailRenderer>().startColor = meteorColor;
            meteor.GetComponentInChildren<TrailRenderer>().endColor = meteorColor;
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
