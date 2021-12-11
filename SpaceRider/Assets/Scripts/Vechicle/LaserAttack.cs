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
        rayPrefab.GetComponent<LightningBoltScript>().StartPosition = rayTip.position;
        rayPrefab.GetComponent<LightningBoltScript>().EndPosition = rayTip.position;
        if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
        {
            if (canAttack && attackEnergy.CanAttack())
            {
                DrawRay();
                DamageTarget();
            }
        }
    }

    private void DamageTarget()
    {
        if(target)
        {
            attackTimer += Time.deltaTime;
            attackEnergy.ReduceEnergy();
            if(attackTimer >= activateEverySeconds)
            {
                target.TakeDamage(damage);
                attackTimer = 0f;
            }
        }
    }

    private void DrawRay()
    {
        target = GetNearestEnemyToMouse();
        if (target)
        {
            rayPrefab.GetComponent<LightningBoltScript>().StartPosition = rayTip.position;
            rayPrefab.GetComponent<LightningBoltScript>().EndPosition = target.transform.position;
            Debug.Log(target.transform.position);
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private Enemy GetNearestEnemyToMouse()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float min = 20f;
        Enemy nearestEnemy = null;
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

    public Enemy GetTarget()
    {
        return target;
    }
}
