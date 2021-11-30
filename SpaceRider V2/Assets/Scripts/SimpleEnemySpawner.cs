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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnEverySeconds);
        }
    }
}
