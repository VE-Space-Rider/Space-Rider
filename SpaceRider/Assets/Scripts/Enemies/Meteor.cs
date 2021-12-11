using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] float xFallVelocity = -12f;
    Rigidbody2D rb;

    private bool hasFallen = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyIfFalls());
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasFallen)
        {
            rb.velocity = new Vector2(xFallVelocity, rb.velocity.y);
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        hasFallen = true;
        ParticleSystem effect = Instantiate(hitEffect, transform.position, transform.rotation);
        effect.startColor = GetComponent<SpriteRenderer>().color;
        Destroy(effect, 4f);
        if (col.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Gameover>().ShowGameoverPanel();
            col.gameObject.SetActive(false);
        }
        if(col.gameObject.CompareTag("Line"))
        {
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
        hitEffect.Play();
    }

    IEnumerator DestroyIfFalls()
    {
        while(true)
        {
            if(transform.position.y < -20f)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(2f);
        }
    }

}
