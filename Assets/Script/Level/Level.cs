using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject map;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCLick()
    {
        GameObject fond = GameObject.Find("Fond");
        GameObject line;

        GameObject qqc;

        mapObject = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);

        for(int j = 0; j < 11; j++)
        {
            line = fond.transform.GetChild(j).gameObject;
            for(int i = 0; i < 13; i++)
            {
                if(line.transform.GetChild(i).name == "entree")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 21;
                }
                else if (line.transform.GetChild(i).name == "mur_cassable")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 10;
                }
                else if (line.transform.GetChild(i).name == "mur_cassable_godMode")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 14;
                }
                else if (line.transform.GetChild(i).name == "mur_cassable_poussee")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 13;
                }
                else if (line.transform.GetChild(i).name == "mur_cassable_puissance")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 11;
                }
                else if (line.transform.GetChild(i).name == "mur_cassable_vitesse")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 12;
                }
                else if (line.transform.GetChild(i).name == "mur_incassable")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 20;
                }
                else if (line.transform.GetChild(i).name == "sortie")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 22;
                }
                else if (line.transform.GetChild(i).name == "sol")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 0;
                }
                else if (line.transform.GetChild(i).name == "zombie")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 0;
                    qqc = Instantiate(zombie, new Vector3(0, 0, 0), Quaternion.identity);
                    mapObject.GetComponent<Map>().mapEnnemisList[i, j] = qqc;
                }
                else if (line.transform.GetChild(i).name == "explorer")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 0;
                    qqc = Instantiate(explorer, new Vector3(0, 0, 0), Quaternion.identity);
                    mapObject.GetComponent<Map>().mapEnnemisList[i, j] = qqc;
                }
                else if (line.transform.GetChild(i).name == "watchman")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 0;
                    qqc = Instantiate(watchman, new Vector3(0, 0, 0), Quaternion.identity);
                    mapObject.GetComponent<Map>().mapEnnemisList[i, j] = qqc;
                }
                else if (line.transform.GetChild(i).name == "hunter")
                {
                    mapObject.GetComponent<Map>().symboleMap[i, j] = 0;
                    qqc = Instantiate(hunter, new Vector3(0, 0, 0), Quaternion.identity);
                    mapObject.GetComponent<Map>().mapEnnemisList[i, j] = qqc;
                }
            }
        }
    }
}
