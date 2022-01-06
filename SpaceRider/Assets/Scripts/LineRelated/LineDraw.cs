using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class LineDraw : MonoBehaviour
{
    [Header("Player Controlls Configuration")]
    [SerializeField] private KeyCode createLineButton = KeyCode.Mouse0;

    [Header("Line Properties:")]
    [SerializeField] private GameObject linePrefab;
    [SerializeField] float linePointsDistance = 0.1f;

    [Header("Ray & Ray Gun Properties:")]
    [SerializeField] private GameObject rayPrefab;
    [SerializeField] private GameObject rayGun;

    private List<Vector2> currentEdgeColPositions = new List<Vector2>();
    private LineRenderer currentLine;
    private List<GameObject> previousLines = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        CreateLine();
        DrawRay();
    }

    private void CreateLine()
    {
        if (Input.GetKeyDown(createLineButton))
        {
            
            //Delete Previous Line Before Making a new one
            if (previousLines.Count > 0)
            {
                if (previousLines[previousLines.Count - 1])
                {
                    previousLines[previousLines.Count - 1].GetComponent<Line>().StartDeleting();
                }
            }
            //Create new Line at (0,0,0)
            currentLine = CreateLine(linePrefab, Vector3.zero);
            //Create a new point in new line at mouse position
            CreatePoint(currentLine, GetMousePos());
        }
        if (Input.GetKey(createLineButton))
        {
            //Play sound effect
            FindObjectOfType<SoundEffects>().PlayLaserSoundEffect();
            if (currentLine)
            {
                //Diff = Distance between previous line point and mouse position.
                float diff = Vector3.Distance(currentLine.GetPosition(currentLine.positionCount - 1), GetMousePos());
                //If diff > a specific thresshold, Create a new point.
                if (diff > linePointsDistance)
                {
                    CreatePoint(currentLine, GetMousePos());
                }
            }
        }
        if (Input.GetKeyUp(createLineButton))
        {
            //Delete Ray
            //When the new line is completed, add it to the previousLines list
            if (currentLine)
            {
                previousLines.Add(currentLine.gameObject);
            }
            //Delete its cache
            currentLine = null;
            currentEdgeColPositions.Clear();
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private LineRenderer CreateLine(GameObject linePref, Vector3 position)
    {
        GameObject newLine = Instantiate(linePref, position, transform.rotation) as GameObject;
        LineRenderer line = newLine.GetComponent<LineRenderer>();
        return line;
    }

    private void CreatePoint(LineRenderer line, Vector3 newPoint)
    {
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, newPoint);
        Vector2 newPoint2D = new Vector2(newPoint.x, newPoint.y);
        currentEdgeColPositions.Add(newPoint2D);
        line.GetComponent<EdgeCollider2D>().points = currentEdgeColPositions.ToArray();
    }

    private void DrawRay()
    {
        if (currentLine)
        {
            rayPrefab.GetComponent<LightningBoltScript>().StartPosition = rayGun.transform.position;
            rayPrefab.GetComponent<LightningBoltScript>().EndPosition = GetMousePos();
        }
        else
        {
            rayPrefab.GetComponent<LightningBoltScript>().StartPosition = rayGun.transform.position;
            rayPrefab.GetComponent<LightningBoltScript>().EndPosition = rayGun.transform.position;
        }
    }
}
