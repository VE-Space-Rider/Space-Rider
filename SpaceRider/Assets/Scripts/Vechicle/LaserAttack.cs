using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class LaserAttack : MonoBehaviour
{
    [SerializeField] GameObject rayPrefab;
    [SerializeField] Transform rayTip;
    [SerializeField] VechicleAttackEnergy attackEnergy;

    [SerializeField] float damage;
    [SerializeField] float energyConsumtion;
    [SerializeField] float activateEverySeconds;

    private float attackTimer = 0f;
    public bool canAttack = true;

    private Enemy target;

    // Update is called once per frame
    void Update()
    {
        //Get required components from Laser
        rayPrefab.GetComponent<LightningBoltScript>().StartPosition = rayTip.position;
        rayPrefab.GetComponent<LightningBoltScript>().EndPosition = rayTip.position;
        //If Mouse R Click is pressed when L Click is not pressed.
        if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
        {
            //If its *authorized to attack and it has enough energy (*Universe Destruction)`
            if (canAttack && attackEnergy.CanAttack())
            {
                //Draw the ray effect and damage the target
                DrawRay();
                DamageTarget();
            }
        }
        else
        {
            if(!Input.GetKey(KeyCode.Mouse0))
            {
                FindObjectOfType<SoundEffects>().StopLaserSoundEffect();
            } 
        }
    }

    //Damages closest target(if it exists)
    private void DamageTarget()
    {
        //If it has a target
        if(target)
        {
            //Play sound effect
            FindObjectOfType<SoundEffects>().PlayLaserSoundEffect();
            //Update attack Timer and reduce energy while attacking
            attackTimer += Time.deltaTime;
            attackEnergy.ReduceEnergy();
            //Deal damage every second to targeted enemy
            if(attackTimer >= activateEverySeconds)
            {
                //Deal damage and reset the timer
                target.TakeDamage(damage);
                attackTimer = 0f;
            }
        }
    }

    private void DrawRay()
    {
        //Find nearest target
        target = GetNearestEnemyToMouse();
        //If it exists
        if (target)
        {
            //Calculate laser effect start and end points
            rayPrefab.GetComponent<LightningBoltScript>().StartPosition = rayTip.position;
            rayPrefab.GetComponent<LightningBoltScript>().EndPosition = target.transform.position;
            Debug.Log(target.transform.position);
        }
    }

    //Gets mouse position  to world points
    private Vector3 GetMousePos()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    //Finds neareset enemy to mouse
    private Enemy GetNearestEnemyToMouse()
    {
        //Get all active enemies
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        //Minimum actuation range
        float min = 20f;
        Enemy nearestEnemy = null;
        //Get closest enemy
        for(int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(GetMousePos(), enemies[i].transform.position);
            if(distance < min)
            {
                nearestEnemy = enemies[i];
                min = distance;
            }
        }
        return nearestEnemy;
    }

    //Returns current target
    public Enemy GetTarget()
    {
        return target;
    }
}
