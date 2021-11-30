using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VechicleEnergy : MonoBehaviour
{
    [SerializeField] float energyDistance;
    [SerializeField] SpriteRenderer energyFill;

    [SerializeField] private TextMeshProUGUI energyText;

    private float energyMetersToEnd;
    private float lasPosFilled;
    private float maxFill;
    private float energyReduction;
    // Start is called before the first frame update
    void Start()
    {
        lasPosFilled = transform.position.x;
        maxFill = energyFill.size.x;
        energyReduction = maxFill / energyDistance;
        StartCoroutine(CalculateEnerg());
    }

    private void Update()
    {
        //DisplayScore();
        //DisplayEnergy();
    }

    IEnumerator CalculateEnerg()
    {
        while (true)
        {
            energyMetersToEnd = transform.position.x - lasPosFilled;
            energyFill.size = new Vector2((energyDistance - energyMetersToEnd) * energyReduction, energyFill.size.y);
            if (energyMetersToEnd >= energyDistance)
            {
                //linePrefab.GetComponent<SurfaceEffector2D>().enabled = false;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void Refill()
    {
        lasPosFilled = transform.position.x;
        energyFill.size = new Vector2(maxFill, energyFill.size.y);
    }

    private void DisplayEnergy()
    {
        float gasToDisplay = (energyDistance - energyMetersToEnd) / 10f;
        energyText.text = gasToDisplay.ToString("F1") + " lT";
    }
}
