using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [SerializeField] Transform laserHead;
    [SerializeField] Transform laserStick;
    [SerializeField] float angle;
    [SerializeField] float angleRatio;

    private LaserAttack attack;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<LaserAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //If its attacking and not drawing platform
        if(Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0) && attack.GetTarget())
        {
            //Set laser gun to look at target
            MoveLaserHead(attack.GetTarget().transform.position);
            MoveLaserStick(attack.GetTarget().transform.position);
        }
        else
        {
            //Set laser to look at mouse position
            MoveLaserHead(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            MoveLaserStick(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        }
        
    }

    private void MoveLaserHead(Vector3 target)
    {
        //Calculate difference
        Vector3 diff = target - laserHead.position;
        //Normaize to get the rotation
        diff.Normalize();
        //Calculate rotation to radians and convert to degrees
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //Make the rotation
        laserHead.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void MoveLaserStick(Vector3 target)
    {
        //Calculate the rotation
        float rot = target.normalized.x * angle;
        //Make the rotation
        laserStick.rotation = Quaternion.Euler(0f, 0f, rot);
    }
}
