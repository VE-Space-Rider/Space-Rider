using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VechicleAttackEnergy : MonoBehaviour
{
    [SerializeField] SpriteRenderer energyFill;
    [SerializeField] float energyReduction;
    [SerializeField] float energyRegeneration;

    private float regenerationTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        FillEnergy();
    }

    public void ReduceEnergy()
    {
        //If vechicle has energy
        if(energyFill.size.x > 0)
        {
            //Reduce energy sprite size
            energyFill.size = new Vector2(energyFill.size.x - (energyReduction * Time.deltaTime), energyFill.size.y);
        }
    }

    public void FillEnergy()
    {
        //If vechicle energy is not full
        if (energyFill.size.x < 2.77)
        {
            //Regenerate energy (Increase sprite size)
            energyFill.size = new Vector2(energyFill.size.x + (energyRegeneration * Time.deltaTime), energyFill.size.y);
        }
    }

    //Checks if it has enough energy to perform an attack
    public bool CanAttack()
    {
        return energyFill.size.x > 0;
    }
}
