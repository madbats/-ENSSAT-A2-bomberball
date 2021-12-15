using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurCassable : Mur
{
    public GameObject sol;
    // Start is called before the first frame update
    void Start()
    {
        this.isBreakable = true;
    }
    
    public void OnDestroy()
    {
        GameObject qqc;
        qqc=Instantiate(sol, gameObject.transform.position, Quaternion.identity);
        qqc.transform.SetParent(transform.parent,false);

    }
}
