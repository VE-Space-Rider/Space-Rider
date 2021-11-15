using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Emmit(Vector2 emmiter, Vector2 target, float force)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce((target - emmiter).normalized * force, ForceMode2D.Impulse);
    }
}
