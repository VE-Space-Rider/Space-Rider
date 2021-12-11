using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] Sprite[] planetSprites;
    [SerializeField] GameObject planetObject;
    [SerializeField] Transform planetStartPos;
    [SerializeField] Transform planetEndPos;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(planetObject.transform.position.x <= planetEndPos.position.x)
        {
            

            int randomIndex = Random.Range(0, planetSprites.Length-1);
            planetObject.GetComponent<SpriteRenderer>().sprite = planetSprites[randomIndex];

            float randomY = Random.Range(-20f, 20f);
            float randomSize = Random.Range(minSize, maxSize);
            planetStartPos.position = new Vector2(planetStartPos.position.x,randomY);
            planetObject.transform.localScale = new Vector2(randomSize, randomSize);
            planetObject.transform.position = planetStartPos.position;

        }
        else
        {
            planetObject.transform.position = Vector2.MoveTowards(planetObject.transform.position, planetEndPos.position, speed * Time.deltaTime);
        }  
    }
}
