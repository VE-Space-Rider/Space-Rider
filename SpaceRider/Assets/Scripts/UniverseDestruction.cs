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
        initChaos = rayPrefab.GetComponent<LightningBoltScript>().ChaosFactor;
        timer = destructionSeconds;
        timerSlider.maxValue = destructionSeconds;
        timerSlider.value = destructionSeconds;
        StartCoroutine(UniverseCountdown());      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
        {
            if(timer <= enablePortalSecond)
            {
                OpenPortal();
            }
        }
    }

    IEnumerator UniverseCountdown()
    {
        while (timer > 0)
        {
            timer--;
            timerText.text = timer.ToString();
            timerSlider.value = timer;
            if (timer == enablePortalSecond)
            {
                timerText.color = textColor;
                FindObjectOfType<LaserAttack>().canAttack = false;
            }
            if (timer == 0)
            {
                explosionEffect.GetComponent<ParticleSystem>().Play();
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                foreach(Enemy enemy in enemies)
                {
                    Destroy(enemy.gameObject);
                }
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
        vechicle.gameObject.SetActive(true);
        vechicle.GetComponent<Animator>().Play("Unsrink");
        FindObjectOfType<LaserAttack>().canAttack = true;
    }

    private void ResetEverything()
    {
        if (!vechicle.gameObject.active)
        {
            timerText.color = Color.white;
            
            vechicle.transform.position = new Vector3(0f, 0f, 0f);
            vechicle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            vechicle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            SetColors();
            rayPrefab.GetComponent<LightningBoltScript>().ChaosFactor = initChaos;
            transportEffect.GetComponent<Light2D>().intensity = 0f;
            transportEffect.GetComponent<Light2D>().pointLightOuterRadius = 1f;
            timer = destructionSeconds;
            portalTimer = 0f;

            StartCoroutine(UniverseCountdown());
        }   
    }

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

    private void OpenPortal()
    {
        //0.065
        portalTimer += Time.deltaTime;
        rayPrefab.GetComponent<LightningBoltScript>().ChaosFactor = 1f;
        rayPrefab.GetComponent<LightningBoltScript>().StartPosition = rayTip.transform.position;
        rayPrefab.GetComponent<LightningBoltScript>().EndPosition = vechicle.position;
        transportEffect.GetComponent<Light2D>().intensity = portalTimer * 16f;
        transportEffect.GetComponent<Light2D>().pointLightOuterRadius = portalTimer * 16f;

        if (portalTimer >= 1.5f)
        {
            //transportEffect.transform.parent = null;
            if(!transportEffect.GetComponent<ParticleSystem>().isPlaying)
            {
                transportEffect.GetComponent<ParticleSystem>().Play();
                vechicle.GetComponent<Animator>().Play("Srink");
            }
            if(portalTimer >= 2.3f)
            {
                vechicle.GetComponent<ScoreCounter>().ChangeUniverse();
                vechicle.gameObject.SetActive(false);
            }
        }
    }

}
