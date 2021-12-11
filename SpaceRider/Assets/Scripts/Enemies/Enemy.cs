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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(damageAnimation.isPlaying)
        {
            damageLight.intensity += Time.deltaTime * 60f;
        }
        else
        {
            damageLight.intensity = 1f;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageAnimation();
        if(health <= 0f)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
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
        if(collision.gameObject.CompareTag("Player"))
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            FindObjectOfType<Gameover>().ShowGameoverPanel();
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
            
            
        }
    }
}
