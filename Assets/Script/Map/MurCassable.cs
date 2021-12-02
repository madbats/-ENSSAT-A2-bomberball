using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MurCassable : Mur
{
    // Start is called before the first frame update
    void Start()
    {
        this.isBreakable = true;
        
    }

    public abstract void OnDestroy();

    // Update is called once per frame
    void Update()
    {
        
    }
}
