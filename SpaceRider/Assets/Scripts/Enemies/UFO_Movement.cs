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
        (lower, upper) = CalculateRandomRange();
        transform.position = new Vector2(transform.position.x, upper - lower);
        int randomTargetPos = Random.Range(0, 1);
        if(randomTargetPos == 0)
        {
            targetPos = new Vector2(transform.position.x, lower);
        }
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

    private (float,float) CalculateRandomRange()
    {
        float distance = Random.Range(bountry/2f, bountry * 2f);

        float lowerLimit = Random.Range(-bountry, bountry - distance);

        float upperLimit = Random.Range(lowerLimit + distance, bountry);

        return (lowerLimit, upperLimit);
    }

    void Move()
    {

        if (Mathf.Abs(upper - transform.position.y) <= 0.1f)
        {
            targetPos = new Vector2(transform.position.x, lower);
        }
        if (Mathf.Abs(lower - transform.position.y) <= 0.1f)
        {
            targetPos = new Vector2(transform.position.x, upper);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
