using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Movement : MonoBehaviour
{
    [SerializeField] float bountry;
    //[SerializeField] float distance;

    public float speed;

    private float upper;
    private float lower;

    Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize move borders and position
        (lower, upper) = CalculateRandomRange();
        transform.position = new Vector2(transform.position.x, upper - lower);
        int randomTargetPos = Random.Range(0, 1);
        //Set dir = down
        if(randomTargetPos == 0)
        {
            targetPos = new Vector2(transform.position.x, lower);
        }
        //Set dir = up
        else
        {
            targetPos = new Vector2(transform.position.x, upper);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //Calculates random properties for the ufo movemnt
    private (float,float) CalculateRandomRange()
    {
        //Calculate distance between bountries
        float distance = Random.Range(bountry/2f, bountry * 2f);
        //Calculate lowerLimit
        float lowerLimit = Random.Range(-bountry, bountry - distance);
        //Calculate upperLimit
        float upperLimit = Random.Range(lowerLimit + distance, bountry);

        return (lowerLimit, upperLimit);
    }

    void Move()
    {
        //If its close to upper bountry start moving down
        if (Mathf.Abs(upper - transform.position.y) <= 0.1f)
        {
            targetPos = new Vector2(transform.position.x, lower);
        }
        //If its close to lower bountry start moving up
        if (Mathf.Abs(lower - transform.position.y) <= 0.1f)
        {
            targetPos = new Vector2(transform.position.x, upper);
        }
        //Perform movement
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
