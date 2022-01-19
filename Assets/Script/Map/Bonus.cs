using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : Sol
{
    public float duration;
    public int power;
    public GameObject sol;
    protected float startTime;
    public int scoreValue;

    public bool consumed = false;
    public Sprite onceConsumed;
    public bool end = false;

    /// <summary>
    /// Gestion de la consommation de l'item
    /// </summary>
    /// <returns></returns>
    public abstract bool OnConsumption();

    /// <summary>
    /// Vérifie la fin des effets du bonus 
    /// </summary>
    /// <returns></returns>
    public abstract bool CheckEnd();

    /// <summary>
    /// Supprime le bonus de la map
    /// </summary>
    public abstract void Destruction();
    
    /// <summary>
    /// Calcule le temps restant des effets du bonus
    /// </summary>
    /// <returns></returns>
    public float RemainningTime()
    {
        return startTime + duration - Time.time;
    }
}
