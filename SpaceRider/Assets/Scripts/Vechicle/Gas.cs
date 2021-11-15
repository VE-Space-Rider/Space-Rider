using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gas : MonoBehaviour
{
    [SerializeField] float gasUnits;
    [SerializeField] SpriteRenderer gasFill;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gasText;

    private float distance;

    private float gasDistance;
    private float lasPosFilled;
    private float maxFill;
    private float gassReduction;
    // Start is called before the first frame update
    void Start()
    {
        lasPosFilled = transform.position.x;
        maxFill = gasFill.size.x;
        gassReduction = maxFill / gasUnits;
        StartCoroutine(CalculateDistance());
        StartCoroutine(CalculateGas());
    }

    private void Update()
    {
        DisplayScore();
        DisplayGas();
    }

    IEnumerator CalculateDistance()
    {

        while(true)
        {
            distance = transform.position.x;
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator CalculateGas()
    {
        while (true)
        {
            gasDistance = transform.position.x - lasPosFilled;
            gasFill.size = new Vector2((gasUnits - gasDistance) * gassReduction, gasFill.size.y);
            if(gasDistance >= gasUnits)
            {
                //linePrefab.GetComponent<SurfaceEffector2D>().enabled = false;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void RefillGas()
    {
        lasPosFilled = transform.position.x;
        gasFill.size = new Vector2(maxFill, gasFill.size.y);
    }

    private void DisplayScore()
    {
        float score = distance * 10f;
        
        if(score > 10000f)
        {
            float thousand = score / 1000f;
            scoreText.text = thousand.ToString("F") + "1K";
        }
        else if(score > 100000f)
        {
            float million = score / 1000000f;
            scoreText.text = million.ToString("F") + "1M";
        }
        else
        {
            scoreText.text = score.ToString("F");
        }
    }

    private void DisplayGas()
    {
        float gasToDisplay = (gasUnits - gasDistance) / 10f;
        gasText.text = gasToDisplay.ToString("F1") + " lT";
    }
}
