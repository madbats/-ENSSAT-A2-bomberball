using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : Sol
{
    public float duration;
    public int power;
    public GameObject sol;
    protected float startTime;

    public bool consumed = false;
    public Sprite onceConsumed;
    public bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        this.isConsumable = true;
    }

    public void OnDestroy()
    {
    }

    public abstract bool OnConsumption(); //Gestion de la consommation de l'item
    public abstract bool CheckEnd();
    public abstract void Destruction();
    
}
