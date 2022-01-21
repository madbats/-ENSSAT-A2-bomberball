using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineType1 : Line
{
    public GameObject mur_incassable;

    public Transform trash;
    // Start is called before the first frame update
    void Start()
    {
        GameObject qqc;
        for(int i = 0; i < 13; i++)
        {
            qqc = Instantiate(mur_incassable, new Vector3(0, 0, 0), Quaternion.identity);
            qqc.transform.position = new Vector3(0, 0, 0);
            qqc.transform.SetParent(this.transform);
            qqc.name = mur_incassable.name;
            qqc.GetComponent<Draggable>().trash = trash;
            qqc.GetComponent<Draggable>().parentToReturnTo = GameObject.Find("Fond").transform;
            qqc.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
