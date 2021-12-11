using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyRaisor : MonoBehaviour
{
    [Header("Meteor")]
    [SerializeField] float initMeteorFrequency;
    [SerializeField] float meteorFreqUpgrade;
    [SerializeField] float maxMeteorFrequency;
    [Header("Enemy UFO")]
    [SerializeField] int initUfoSpawnMeters;
    [SerializeField] int ufoFreqUpgrade;
    [SerializeField] int maxUfoSpawnMeters;
    [SerializeField] int initUfoHealth;
    [SerializeField] int ufoHealthUpgrade;
    [SerializeField] int maxUfoHealth;
    [SerializeField] float initUfoSpeed;
    [SerializeField] float ufoSpeedUpgrade;
    [SerializeField] float maxUfoSpeed;

    [SerializeField] int hardenEveryMeters;

    [SerializeField] GameObject vechicle;

    MeteorSpawner meteorSpawner;
    UFO_Spawner ufoSpawner;

    private int score;
    private int nextHarden;

    // Start is called before the first frame update
    void Start()
    {
        nextHarden = hardenEveryMeters;

        meteorSpawner = FindObjectOfType<MeteorSpawner>();
        ufoSpawner = FindObjectOfType<UFO_Spawner>();

        ufoSpawner.health = initUfoHealth;
        ufoSpawner.spawnEveryMeters = initUfoSpawnMeters;
        ufoSpawner.speed = initUfoSpeed;
        meteorSpawner.spawnFrequency = initMeteorFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        score = vechicle.GetComponent<ScoreCounter>().GetScore();
        if(score >= nextHarden)
        {
            if(ufoSpawner.health < maxUfoHealth)
                ufoSpawner.health += ufoHealthUpgrade;
            if(ufoSpawner.spawnEveryMeters > maxUfoSpawnMeters)
                ufoSpawner.spawnEveryMeters += ufoFreqUpgrade;
            if (ufoSpawner.speed < maxUfoSpeed)
                ufoSpawner.speed += ufoSpeedUpgrade;
            if(meteorSpawner.spawnFrequency > maxMeteorFrequency)
                meteorSpawner.spawnFrequency += meteorFreqUpgrade;
            nextHarden += hardenEveryMeters;
        }
    }
}
