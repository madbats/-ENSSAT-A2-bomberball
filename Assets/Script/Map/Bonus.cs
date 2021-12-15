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
        OnConsumption();
    }

    public void OnConsumption() //Gestion de la consommation de l'item
    {
        GameObject qqc;
        qqc = Instantiate(sol, gameObject.transform.position, Quaternion.identity);
        qqc.transform.SetParent(transform.parent, false);
        qqc.transform.parent.GetComponent<Map>().mapItemsList[(int)transform.position.x, (int)transform.position.y] = qqc.GetComponent<MapItem>();
    }
}
