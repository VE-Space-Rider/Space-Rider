using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnEverySeconds;
    [SerializeField] GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        //Create an enemy every [spawnEverySeconds] seconds
        while(true)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnEverySeconds);
        }
    }
}
