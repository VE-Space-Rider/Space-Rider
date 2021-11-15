using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform emmiter;
    [SerializeField] private float emmitForce;
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private float baseDamage;

    private float attackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = baseAttackSpeed;
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Attack()
    {
        while(true)
        {
            if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
            {
                GameObject projectile = Instantiate(projectilePrefab, emmiter.position, emmiter.rotation);
                projectile.GetComponent<Projectile>().Emmit(emmiter.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), emmitForce);
                yield return new WaitForSeconds(1 / attackSpeed);
            }
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }
}
