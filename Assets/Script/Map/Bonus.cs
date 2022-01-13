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

    public abstract bool OnConsumption(); //Gestion de la consommation de l'item
    public abstract bool CheckEnd();
    public abstract void Destruction();
    
    public float RemainningTime()
    {
        return startTime + duration - Time.time;
    }
}
