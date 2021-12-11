using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VechicleAttackEnergy : MonoBehaviour
{
    [SerializeField] SpriteRenderer energyFill;
    [SerializeField] float energyReduction;
    [SerializeField] float energyRegeneration;


    private float regenerationTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FillEnergy();
    }

    public void ReduceEnergy()
    {
        if(energyFill.size.x > 0)
        {
            energyFill.size = new Vector2(energyFill.size.x - (energyReduction * Time.deltaTime), energyFill.size.y);
        }
    }

    public void FillEnergy()
    {
        if (energyFill.size.x < 2.77)
        {
            energyFill.size = new Vector2(energyFill.size.x + (energyRegeneration * Time.deltaTime), energyFill.size.y);
        }
    }

    public bool CanAttack()
    {
        return energyFill.size.x > 0;
    }
}
