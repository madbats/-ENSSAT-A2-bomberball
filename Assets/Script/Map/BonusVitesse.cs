using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVitesse : Bonus
{
    override
    public void OnConsumption() //Gestion de la consommation de l'item
    {
        GameObject qqc;
        qqc = Instantiate(sol, gameObject.transform.position, Quaternion.identity);
        qqc.transform.SetParent(transform.parent, false);
        qqc.transform.parent.GetComponent<Map>().mapItemsList[(int)transform.position.x, (int)transform.position.y] = qqc.GetComponent<MapItem>();
        Destroy(this.gameObject);



    }
}
