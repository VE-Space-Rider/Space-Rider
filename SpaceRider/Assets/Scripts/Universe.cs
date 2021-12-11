using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Universe", menuName="Create new Universe", order=0)]
public class Universe : ScriptableObject
{
    [Header("Universe")]
    public Color universeColor;
    public Color nebulaColor;
    public Color starColor;

    [Header("Enemy UFO")]
    public Color ufoColor1;
    public Color ufoColor2;
    public Color ufoColor3;
    public Color ufoColor4;

    [Header("Enemy Spaceship")]
    public Color shipColor1;
    public Color shipColor2;
    public Color shipColor3;
    public Color shipColor4;

    [Header("Meteor")]
    public Color meteorColor;
}
