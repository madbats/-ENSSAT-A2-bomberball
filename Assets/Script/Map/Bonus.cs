using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : Sol
{
    public int duration;
    public int power;
    public GameObject sol;

    // Start is called before the first frame update
    void Start()
    {
        this.isConsumable = true;
    }

    public void OnDestroy()
    {
    }

    public abstract void OnConsumption(); //Gestion de la consommation de l'item
    
}
