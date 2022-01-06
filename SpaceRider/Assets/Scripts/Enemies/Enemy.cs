using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Enemy : MonoBehaviour
{
    public float health;
    [SerializeField] ParticleSystem damageAnimation;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] Light2D damageLight;

    // Update is called once per frame
    void Update()
    {
        //If damage effect is playing smoothly increase light intensity
        if(damageAnimation.isPlaying)
        {
            damageLight.intensity += Time.deltaTime * 60f;
        }
        else
        {
            //Else set intensity to 1
            damageLight.intensity = 1f;
        }
    }

    public void TakeDamage(float damage)
    {
        //Reduce health based on damage and apply animation
        health -= damage;
        DamageAnimation();
        if(health <= 0f)
        {
            //Play destroy effect and destroy gameobject
            Instantiate(destroyEffect, transform.position, transform.rotation);
            FindObjectOfType<SoundEffects>().PlayExplosionEffect();
            Destroy(gameObject);
        }
    }

    private void DamageAnimation()
    {
        if(damageAnimation)
        {
            damageAnimation.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If it comes in contanct with the player
        if(collision.gameObject.CompareTag("Player"))
        {
            //Play destroy effect and prepare gameover task
            FindObjectOfType<SoundEffects>().PlayExplosionEffect();
            Instantiate(destroyEffect, transform.position, transform.rotation);
            FindObjectOfType<Gameover>().ShowGameoverPanel();
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
            
            
        }
    }
}
