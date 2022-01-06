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
        FindObjectOfType<SoundEffects>().PlayMeteorFallEffect();
    }

    // Update is called once per frame
    void Update()
    {
        //If it has not yet collided
        if(!hasFallen)
        {
            rb.velocity = new Vector2(xFallVelocity, rb.velocity.y);
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        //Trigger hasFallen (Update)
        hasFallen = true;
        //Create and play destroy effect
        ParticleSystem effect = Instantiate(hitEffect, transform.position, transform.rotation);
        effect.startColor = GetComponent<SpriteRenderer>().color;
        FindObjectOfType<SoundEffects>().PlayMeteorContactEffect();
        Destroy(effect, 4f);
        //If it contacts player
        if (col.gameObject.CompareTag("Player"))
        {
            //Show gameover
            FindObjectOfType<SoundEffects>().PlayExplosionEffect();

            FindObjectOfType<Gameover>().ShowGameoverPanel();
            col.gameObject.SetActive(false);
        }
        //If it contacts platform
        if(col.gameObject.CompareTag("Line"))
        {
            //Destroy platform
            Destroy(col.gameObject);
        }
        //Else destroy the gameObject
        Destroy(gameObject);
        hitEffect.Play();
    }

    //Checks if commet is out of screen to destroy it
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
