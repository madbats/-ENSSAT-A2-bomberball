using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object bonus de la map
/// </summary>
public abstract class Bonus : Sol
{
    /// <summary>
    /// Durée du bonus
    /// </summary>
    public float duration;

    public int power;
    public GameObject sol;
    /// <summary>
    /// Date de début de l'effet du bonus
    /// </summary>
    protected float startTime;
    /// <summary>
    /// Valeur en score de la consommation du bonus
    /// </summary>
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
    /// <returns>temps restant</returns>
    public float RemainningTime()
    {
        return startTime + duration - Time.time;
    }
}
