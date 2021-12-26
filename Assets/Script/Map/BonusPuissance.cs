using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPuissance : Bonus
{
    override
    public bool OnConsumption() //Gestion de la consommation de l'item
    {
        GameObject qqc;
        qqc = Instantiate(sol, gameObject.transform.position, Quaternion.identity);
        qqc.transform.SetParent(transform.parent, false);
        qqc.transform.parent.GetComponent<Map>().mapItemsList[(int)transform.position.x, (int)transform.position.y] = qqc.GetComponent<MapItem>();
        Destroy(this.gameObject);

        return false;
    }

    override
    public bool CheckEnd()
    {
        return false;
    }

    override
    public void Destruction()
    {

    }
}
