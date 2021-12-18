using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum items { 
    SOL = 0, 
    OBJET_PUISSANCE = 1, 
    OBJET_DEPLACEMENT = 2, 
    OBJET_POUSSEE = 3, 
    OBJET_GODMOD = 4, 
    MUR_CASSABLE = 10, 
    MUR_CASSABLE_PUISSANCE = 11, 
    MUR_CASSABLE_DEPLACEMENT = 12, 
    MUR_CASSABLE_POUSSEE = 13, 
    MUR_CASSABLE_GODMODE = 14, 
    MUR_INCASSABLE = 20 
}; 

public class Map : MonoBehaviour { 
    public int[,] symboleMap = new int[13, 11]; // liste stylisés des objets de la carte
    public MapItem[,] mapItemsList = new MapItem[13,11]; //liste de tous les objets (au sens large) de la carte.     
    public int seed; //seed de la génération
    public int difficulty; //difficulté du niveau
    public int number; //Niveau de la campagne
    public GameObject sol;
    public GameObject objet_puissance;
    public GameObject objet_deplacement;
    public GameObject objet_poussee;
    public GameObject objet_godmode;
    public GameObject mur_cassable;
    public GameObject mur_cassable_puissance;
    public GameObject mur_cassable_deplacement;
    public GameObject mur_cassable_poussee;
    public GameObject mur_cassable_godmode;
    public GameObject mur_incassable;

    private int[,] testMap = { 
            { 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 },
            {20,10,0,0,0,0,0,0,0,0,0,10,20 },
            {20,10,20,14,20,0,20,0,20,0,20,0,20 },
            {20,0,0,0,0,0,0,0,12,0,0,0,20 },
            {20,0,20,0,20,0,20,0,20,0,20,0,20 },
            {20,0,0,0,0,0,0,14,0,0,0,0,20 },
            {20,0,20,11,20,0,20,0,20,0,20,0,20 },
            {20,0,0,0,0,0,13,0,0,0,0,0,20 },
            {20,0,20,0,20,0,20,0,20,0,20,0,20 },
            {20,12,0,0,0,12,0,0,0,0,0,14,20 },
            { 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 }
    };

    // Start is called before the first frame update
    void Start()    
    {
        GameObject newObject;
        GameObject qqc;
        for (int i = 0; i < 11; i++)
        {
            for(int j = 0; j < 13; j++)
            {
                switch (testMap[10-i, j])
                {
                    case 0:
                        newObject = sol;
                        break;
                    case 1:
                        newObject = objet_puissance;
                        break;
                    case 2:
                        newObject = objet_deplacement;
                        break;
                    case 3:
                        newObject = objet_poussee;
                        break;
                    case 4:
                        newObject = objet_godmode;
                        break;
                    case 10:
                        newObject = mur_cassable;
                        break;
                    case 11:
                        newObject = mur_cassable_puissance;
                        break;
                    case 12:
                        newObject = mur_cassable_deplacement;
                        break;
                    case 13:
                        newObject = mur_cassable_poussee;
                        break;
                    case 14:
                        newObject = mur_cassable_godmode;
                        break;
                    case 20:
                        newObject = mur_incassable;
                        break;
                    default:
                        newObject = mur_cassable;
                        break;
                }
                qqc = Instantiate(newObject, new Vector3(j,i), Quaternion.identity);
                qqc.transform.SetParent(transform, false);
                mapItemsList[j, i] = qqc.GetComponent<MapItem>();
            }
        }
        Debug.Log(mapItemsList[1, 1].transform.position);
        Debug.Log(mapItemsList[1, 5].transform.position);
        Debug.Log(mapItemsList[10, 2].transform.position);

    }
}