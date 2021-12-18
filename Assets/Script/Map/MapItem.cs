using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapItem : MonoBehaviour
{
    public Sprite sprite; //Sprite de l'objet
    public bool isBreakable; //Objet cassable
    public bool isConsumable; //Objet consommable

    public bool light=false;
    public bool discover=false;



}
