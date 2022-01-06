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
    // Update is called once per frame
    void Update()
    {
        //Checks if Planet positon is greater than End Position
        if(planetObject.transform.position.x <= planetEndPos.position.x)
        {
            //Render a random Planet
            int randomIndex = Random.Range(0, planetSprites.Length-1);
            planetObject.GetComponent<SpriteRenderer>().sprite = planetSprites[randomIndex];
            //Calculate random vertical position and size
            float randomY = Random.Range(-20f, 20f);
            float randomSize = Random.Range(minSize, maxSize);
            //Place planet at Start Position and apply changes
            planetStartPos.position = new Vector2(planetStartPos.position.x,randomY);
            planetObject.transform.localScale = new Vector2(randomSize, randomSize);
            planetObject.transform.position = planetStartPos.position;

        }
        else
        {
            //Move towards End Position
            planetObject.transform.position = Vector2.MoveTowards(planetObject.transform.position, planetEndPos.position, speed * Time.deltaTime);
        }  
    }
}
