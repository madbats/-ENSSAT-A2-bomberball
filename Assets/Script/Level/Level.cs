using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject grid;

    public GameObject map;
    public GameObject player;

    public GameObject sol;
    public GameObject mur_cassable;
    public GameObject mur_cassable_puissance;
    public GameObject mur_cassable_deplacement;
    public GameObject mur_cassable_poussee;
    public GameObject mur_cassable_godmode;
    public GameObject mur_incassable;
    public GameObject entree;
    public GameObject sortie;

    public GameObject zombie;
    public GameObject explorer;
    public GameObject watchman;
    public GameObject hunter;

    public GameObject mapObject;

    // Start is called before the first frame update
    void Start()
    {
        GameObject qqc;
        qqc = Instantiate(grid, new Vector3(0, 0, 0), Quaternion.identity);
        qqc.transform.SetParent(GameObject.Find("Fond").transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
