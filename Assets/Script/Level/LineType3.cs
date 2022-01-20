using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineType3 : MonoBehaviour
{
    public GameObject mur_incassable;
    public GameObject sol;

    public Transform trash;
    // Start is called before the first frame update
    void Start()
    {
        GameObject qqc;
        qqc = Instantiate(mur_incassable, new Vector3(0, 0, 0), Quaternion.identity);
        qqc.transform.position = new Vector3(0, 0, 0);
        qqc.transform.SetParent(this.transform);
        qqc.GetComponent<Draggable>().trash = trash;
        qqc.GetComponent<Draggable>().parentToReturnTo = GameObject.Find("Fond").transform;
        for (int i = 0; i < 11; i++)
        {
            qqc = Instantiate(sol, new Vector3(0, 0, 0), Quaternion.identity);
            qqc.transform.position = new Vector3(0, 0, 0);
            qqc.transform.SetParent(this.transform);
            qqc.GetComponent<Draggable>().trash = trash;
            qqc.GetComponent<Draggable>().parentToReturnTo = GameObject.Find("Fond").transform;
        }
        qqc = Instantiate(mur_incassable, new Vector3(0, 0, 0), Quaternion.identity);
        qqc.transform.position = new Vector3(0, 0, 0);
        qqc.transform.SetParent(this.transform);
        qqc.GetComponent<Draggable>().trash = trash;
        qqc.GetComponent<Draggable>().parentToReturnTo = GameObject.Find("Fond").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
