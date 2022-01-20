using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objet de base de chaque objet de la map
/// </summary>
public abstract class MapItem : MonoBehaviour
{
    public Sprite sprite; //Sprite de l'objet
    public bool isBreakable; //Objet cassable
    public bool isConsumable; //Objet consommable
}
