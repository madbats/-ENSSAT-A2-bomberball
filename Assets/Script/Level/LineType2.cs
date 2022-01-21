using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineType2 : Line
{
    public GameObject mur_incassable;
    public GameObject sol;

    public Transform trash;
    // Start is called before the first frame update
    void Start()
    {
        GameObject qqc;
        for (int i = 0; i < 13; i++)
        {
            if(i%2 == 0)
            {
                qqc = Instantiate(mur_incassable, new Vector3(0, 0, 0), Quaternion.identity);
                qqc.name = mur_incassable.name;
            }
            else
            {
                qqc = Instantiate(sol, new Vector3(0, 0, 0), Quaternion.identity);
                qqc.name = sol.name;
            }
            qqc.transform.position = new Vector3(0, 0, 0);
            qqc.transform.SetParent(this.transform);
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
