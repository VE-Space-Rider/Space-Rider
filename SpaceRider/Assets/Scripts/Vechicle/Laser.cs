using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] Transform laserHead;
    [SerializeField] Transform laserStick;
    [SerializeField] float angle;
    [SerializeField] float angleRatio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveLaserHead();
        MoveLaserStick();
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private void MoveLaserHead()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - laserHead.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        laserHead.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void MoveLaserStick()
    {
        //float rot = Input.mousePosition.normalized.x * angle;
        float rot = Camera.main.ScreenToViewportPoint(Input.mousePosition).normalized.x * angle;
        laserStick.rotation = Quaternion.Euler(0f, 0f, rot);
    }
}
