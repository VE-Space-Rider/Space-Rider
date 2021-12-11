using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [Header("Basic Configuration")]
    [SerializeField] private float speed;
    [SerializeField] private float deleteTime = 2f;

    private float deleteTimer = 0f;
    LineRenderer thisLine;
    EdgeCollider2D edgeCol;
    int currentIndex = 0;

    int prevIndex = 1;
    bool hasStarted = false;
    bool begin = false;

    // Start is called before the first frame update
    void Start()
    {
        thisLine = GetComponent<LineRenderer>();
        edgeCol = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DeleteLine();
        DeleteOnComand();
        if(thisLine.positionCount > 2)
        {
            if (thisLine.GetPosition(0) == thisLine.GetPosition(thisLine.positionCount - 1))
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void DeleteLine()
    {
        deleteTimer += Time.deltaTime;

        if(deleteTimer >= deleteTime)
        {
            if (currentIndex < thisLine.positionCount - 1)
            {
                if(MovePointTonext(thisLine, currentIndex, currentIndex + 1))
                {
                    currentIndex++;
                }
            }
        }
    }

    public void StartDeleting()
    {
        begin = true;
    }

    private void DeleteOnComand()
    {
        if(begin)
        {
            if (!hasStarted)
            {
                prevIndex = thisLine.positionCount - 1;
                hasStarted = true;
            }
            if (prevIndex > 0)
            {
                if (MovePointToPrevious(thisLine, prevIndex, prevIndex - 1))
                {
                    prevIndex--;
                }
            }
        }
    }

    private bool MovePointTonext(LineRenderer line, int currentPointIndex, int nextPointIndex)
    {
        float distance = Vector3.Distance(line.GetPosition(currentPointIndex), line.GetPosition(nextPointIndex));
        Vector3 nextPos = Vector3.MoveTowards(line.GetPosition(currentPointIndex), line.GetPosition(nextPointIndex), speed * Time.deltaTime);
        if (distance > 0)
        {
            //Debug.Log(distance);
            line.SetPosition(currentPointIndex, nextPos);
            SetColliderPoints(currentPointIndex, nextPos);
            for (int i = 0; i < currentPointIndex; i++)
            {
                line.SetPosition(i, line.GetPosition(currentPointIndex));
                SetColliderPoints(i, line.GetPosition(currentPointIndex));
            }

            return false;
        }
        else
        {
            return true;
        }
    }

    private bool MovePointToPrevious(LineRenderer line, int currentPointIndex, int previousPointIndex)
    {
        float distance = Vector3.Distance(line.GetPosition(currentPointIndex), line.GetPosition(previousPointIndex));
        Vector3 nextPos = Vector3.MoveTowards(line.GetPosition(currentPointIndex), line.GetPosition(previousPointIndex), speed * Time.deltaTime);
        if (distance > 0)
        {
            line.SetPosition(currentPointIndex, nextPos);
            SetColliderPoints(currentPointIndex, nextPos);
            for (int i = line.positionCount -1; i > currentPointIndex; i--)
            {
                line.SetPosition(i, line.GetPosition(currentPointIndex));
                SetColliderPoints(i, line.GetPosition(currentPointIndex));
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SetColliderPoints(int index, Vector3 pos)
    {
        int pointCount = edgeCol.pointCount;
        Vector2[] points = edgeCol.points;
        points[index] = new Vector2(pos.x, pos.y);
        edgeCol.points = points;
    }
}
