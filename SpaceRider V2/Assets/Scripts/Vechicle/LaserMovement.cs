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
        if(Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0) && attack.GetTarget())
        {
            MoveLaserHead(attack.GetTarget().transform.position);
            MoveLaserStick(attack.GetTarget().transform.position);
        }
        else
        {
            MoveLaserHead(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            MoveLaserStick(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        }
        
    }

    private void MoveLaserHead(Vector3 target)
    {
        Vector3 diff = target - laserHead.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        laserHead.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void MoveLaserStick(Vector3 target)
    {
        float rot = target.normalized.x * angle;
        laserStick.rotation = Quaternion.Euler(0f, 0f, rot);
    }
}
