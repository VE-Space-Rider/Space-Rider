using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DigitalRuby.LightningBolt;
using UnityEngine.Experimental.Rendering.Universal;

public class UniverseDestruction : MonoBehaviour
{
    [SerializeField] Color textColor;

    [SerializeField] Universe[] universes;
    [SerializeField] ParticleSystem nebula;
    [SerializeField] ParticleSystem stars;

    [SerializeField] int destructionSeconds = 150;
    [SerializeField] int enablePortalSecond = 5;
    [SerializeField] int portalCreationTime = 3;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Slider timerSlider;

    [SerializeField] Transform rayTip;
    [SerializeField] GameObject rayPrefab;
    [SerializeField] Transform vechicle;
    [SerializeField] GameObject transportEffect;
    [SerializeField] GameObject explosionEffect;
    private float portalTimer = 0f;
    private float initChaos = 0f;

    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetColors();
        //Initialize Laser Effect Chaos
        initChaos = rayPrefab.GetComponent<LightningBoltScript>().ChaosFactor;
        //Reset Timer
        timer = destructionSeconds;
        timerSlider.maxValue = destructionSeconds;
        timerSlider.value = destructionSeconds;
        //Start the countdown
        StartCoroutine(UniverseCountdown());      
    }

    // Update is called once per frame
    void Update()
    {   //If Player presses Attack Button and its near time to destroy univrse
        if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
        {
            if(timer <= enablePortalSecond)
            {
                FindObjectOfType<SoundEffects>().PlayLaserSoundEffect();
                OpenPortal();
            }
        }
    }

    //Countdown for universe Destruction
    IEnumerator UniverseCountdown()
    {
        while (timer > 0)
        {
            timer--;
            timerText.text = timer.ToString();
            timerSlider.value = timer;
            //Start playing teleport sound indicator
            if(timer <= enablePortalSecond)
            {
                FindObjectOfType<SoundEffects>().PlayCountdownEffect();
            }
            //If its time to start creating portal
            if (timer == enablePortalSecond)
            {
                //Change countdown text color to red
                timerText.color = textColor;
                //Disable attacking
                FindObjectOfType<LaserAttack>().canAttack = false;
            }
            //If its time to destroy universe
            if (timer == 0)
            {
                //Play explosion effect
                explosionEffect.GetComponent<ParticleSystem>().Play();
                FindObjectOfType<SoundEffects>().PlayExplosionEffect();
                //Destroy enemies
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                foreach(Enemy enemy in enemies)
                {
                    Destroy(enemy.gameObject);
                }
                //If player did not teleport
                if(vechicle.gameObject.active)
                {
                    GetComponent<Gameover>().ShowGameoverPanel();
                    vechicle.GetComponent<Rigidbody2D>().isKinematic = true;
                    FindObjectOfType<LineDraw>().enabled = false;
                }

            }
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(2f);
        ResetEverything();
        yield return new WaitForSeconds(1f);
        //Reset vechicle and attack
        vechicle.gameObject.SetActive(true);
        FindObjectOfType<SoundEffects>().PlayTeleportEffect();
        vechicle.GetComponent<Animator>().Play("Unsrink");
        FindObjectOfType<LaserAttack>().canAttack = true;
    }

    //Teleports player back to start and resets values
    private void ResetEverything()
    {
        //IF Vechicle is deactivated
        if (!vechicle.gameObject.active)
        {
            //Reset countdown text color
            timerText.color = Color.white;
            //Reset vevhicle positon and rotation
            vechicle.transform.position = new Vector3(0f, 0f, 0f);
            vechicle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //Set its velocity to 0
            vechicle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //Change Universe theme
            SetColors();
            //Reset Laser Effect
            rayPrefab.GetComponent<LightningBoltScript>().ChaosFactor = initChaos;
            transportEffect.GetComponent<Light2D>().intensity = 0f;
            transportEffect.GetComponent<Light2D>().pointLightOuterRadius = 1f;
            //Reset Timers
            timer = destructionSeconds;
            portalTimer = 0f;
            //Start countdown
            StartCoroutine(UniverseCountdown());
        }   
    }

    //Set Colors for planet theme
    private void SetColors()
    {
        Universe universe = universes[Random.Range(0, universes.Length)];
        //Scenery Colors
        Camera.main.backgroundColor = universe.universeColor;
        nebula.startColor= universe.nebulaColor;
        stars.startColor = universe.starColor;
        //Meteor Colors
        FindObjectOfType<MeteorSpawner>().meteorColor = universe.meteorColor;
        //UFO Colors
        FindObjectOfType<UFO_Spawner>().ufoColor1 = universe.ufoColor1;
        FindObjectOfType<UFO_Spawner>().ufoColor2 = universe.ufoColor2;
        FindObjectOfType<UFO_Spawner>().ufoColor3 = universe.ufoColor3;
        FindObjectOfType<UFO_Spawner>().ufoColor4 = universe.ufoColor4;

    }

    //Teleport player
    private void OpenPortal()
    {
        //0.065
        //Teleport effect
        portalTimer += Time.deltaTime;
        rayPrefab.GetComponent<LightningBoltScript>().ChaosFactor = 1f;
        rayPrefab.GetComponent<LightningBoltScript>().StartPosition = rayTip.transform.position;
        rayPrefab.GetComponent<LightningBoltScript>().EndPosition = vechicle.position;
        transportEffect.GetComponent<Light2D>().intensity = portalTimer * 16f;
        transportEffect.GetComponent<Light2D>().pointLightOuterRadius = portalTimer * 16f;
        //If player holded mouse for more than 1.5 seconds
        if (portalTimer >= 1.5f)
        {
            //If transport effect is not playing, play it
            if (!transportEffect.GetComponent<ParticleSystem>().isPlaying)
            {
                transportEffect.GetComponent<ParticleSystem>().Play();
                FindObjectOfType<SoundEffects>().PlayTeleportEffect();
                vechicle.GetComponent<Animator>().Play("Srink");
            }
            //If player holded mouse click for more than 2.3 secs teleport player
            if(portalTimer >= 2.3f)
            {
                vechicle.GetComponent<ScoreCounter>().ChangeUniverse();
                vechicle.gameObject.SetActive(false);
            }
        }
    }

}
