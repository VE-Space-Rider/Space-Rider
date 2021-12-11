using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Spawner : MonoBehaviour
{
    [Header("Spawner Configuration")]
    [SerializeField] GameObject ufoPrefab;
    public int spawnEveryMeters;
    public int health;
    public float speed;
    [Header("Player Object")]
    [SerializeField] GameObject vechicle;
    

    public Color ufoColor1;
    public Color ufoColor2;
    public Color ufoColor3;
    public Color ufoColor4;

    private int score;
    private int nextSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = spawnEveryMeters;
    }

    private void Update()
    {
        score = vechicle.GetComponent<ScoreCounter>().GetScore();
        if(score >= nextSpawn)
        {
            GameObject ufo = Instantiate(ufoPrefab, transform.position, transform.rotation);
            ufo.GetComponent<Enemy>().health = health;
            ufo.GetComponent<UFO_Movement>().speed = speed;
            ufo.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ufoColor1;
            ufo.transform.GetChild(1).GetComponent<SpriteRenderer>().color = ufoColor2;
            ufo.transform.GetChild(2).GetComponent<SpriteRenderer>().color = ufoColor3;
            ufo.transform.GetChild(3).GetComponent<SpriteRenderer>().color = ufoColor4;
            nextSpawn += spawnEveryMeters;
        }
    }
}
