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

    IEnumerator Spawn()
    {
        //Initial waitng period
        yield return new WaitForSeconds(5f);
        while (true)
        {
            //Calculate random position between a start and an end point
            float randomX = Random.Range(startSpawnPoint.position.x, endSpawnPoint.position.x);
            //Create and initialize comet (based on universe)
            GameObject meteor = Instantiate(meteorPrefab, new Vector2(randomX, startSpawnPoint.position.y), transform.rotation);
            meteor.GetComponent<SpriteRenderer>().color = meteorColor;
            meteor.GetComponentInChildren<TrailRenderer>().startColor = meteorColor;
            meteor.GetComponentInChildren<TrailRenderer>().endColor = meteorColor;
            //Wait for next comet
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
