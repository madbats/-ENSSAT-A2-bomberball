using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int[,] symboleMap = new int[13, 11]; // liste stylis�s des objets de la carte
    public MapItem[,] mapItemsList = new MapItem[13,11]; //liste de tous les objets (au sens large) de la carte.
    //public Ennemis[,] mapEnnemisList = new Ennemis[13, 11]; //liste de tous les ennemis (au sens large) de la carte.
    public GameObject[,] mapEnnemisList;
    public int seed; //seed de la g�n�ration
    public int difficulty; //difficult� du niveau
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
    public GameObject entree;
    public GameObject sortie;

    public GameObject zombie;
    public GameObject explorer;
    public GameObject watchman;
    public GameObject hunter;
    public GameObject waypoint;

    public Vector3 positionEntree;

    private int[,] testMap = { 
            { 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 },
            {20,10,0,0,0,0,0,0,0,0,0,10,20 },
            {20,10,20,14,20,0,20,0,20,22,20,0,20 },
            {20,0,0,0,0,0,0,0,12,0,0,0,20 },
            {20,0,20,0,20,0,20,0,20,0,20,0,20 },
            {20,0,0,0,0,0,0,14,0,0,0,0,20 },
            {20,0,20,11,20,0,20,0,20,0,20,0,20 },
            {20,0,0,0,0,0,13,0,0,0,0,0,20 },
            {20,21,20,0,20,0,20,0,20,0,20,0,20 },
            {20,12,0,0,0,12,0,0,0,0,0,14,20 },
            { 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 }
    };
    private GameObject[,] testMapEnnemis = {
            { null, null, null, null, null, null, null, null, null, null, null, null, null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null },
            { null, null, null, null, null, null, null, null, null, null, null, null, null }};

    
    public void Build(int[,] map,GameObject[,] ennemis)
    {
        GameObject newObject;
        GameObject qqc;
        int item;
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                item = map[i, j];
                if (item == 0) {
                    newObject = sol;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                }
                else if (item == 1) {
                    newObject = objet_puissance;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                }
                else if (item == 2)
                {
                    newObject = objet_deplacement;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                }
                else if (item == 3)
                {
                    newObject = objet_poussee;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                }
                else if (item == 4)
                {
                    newObject = objet_godmode;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                }
                else if (item == 10)
                {
                    newObject = mur_cassable;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                }
                else if (item == 11 || item == 111 || item == 112) {
                    newObject = mur_cassable_puissance;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                    if(item == 111)
                        qqc.GetComponent<MurCassable>().duration *= 1.5f;
                    if(item == 112)
                        qqc.GetComponent<MurPuissance>().puissance+= 1; 
                }
                else if (item == 12 || item == 121 || item == 122)
                {
                    newObject = mur_cassable_deplacement;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                    if (item == 121)
                        qqc.GetComponent<MurCassable>().duration *= 2f;
                    if (item == 122)
                        qqc.GetComponent<MurCassable>().duration *= 3f;
                } 
                else if (item == 13 || item == 131 || item == 132) { 
                    newObject = mur_cassable_poussee;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                    if (item == 131)
                        qqc.GetComponent<MurCassable>().duration *= 1.5f;
                    if (item == 132)
                        qqc.GetComponent<MurCassable>().duration *= 2f;
                } 
                else if (item == 14 || item == 141 || item == 142) { 
                    newObject = mur_cassable_godmode;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                    if (item == 141)
                        qqc.GetComponent<MurCassable>().duration *= 2f;
                    if (item == 142)
                        qqc.GetComponent<MurCassable>().duration *= 3f;
                } 
                else if (item == 20) { 
                    newObject = mur_incassable;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                } 
                else if (item == 21) { 
                    newObject = entree;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                } 
                else if (item == 22) { 
                    newObject = sortie;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                } 
                else 
                { 
                    newObject = mur_cassable;
                    qqc = Instantiate(newObject, new Vector3(i, j), Quaternion.identity);
                } 
                
                qqc.transform.SetParent(transform, false);
                mapItemsList[i, j] = qqc.GetComponent<MapItem>();
                if (newObject == entree)
                {
                    positionEntree = qqc.transform.position;
                }
                
            }
        }
        mapEnnemisList = ennemis;
    }

}