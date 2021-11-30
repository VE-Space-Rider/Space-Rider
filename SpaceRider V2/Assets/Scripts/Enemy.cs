using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] ParticleSystem damageAnimation;
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
}
