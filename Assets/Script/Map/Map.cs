using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour { 
    public int[,] symboleMap = new int[13, 11]; // liste stylis�s des objets de la carte
    public MapItem[,] mapItemsList = new MapItem[13,11]; //liste de tous les objets (au sens large) de la carte.
    //public Ennemis[,] mapEnnemisList = new Ennemis[13, 11]; //liste de tous les ennemis (au sens large) de la carte.
    public GameObject[,] mapEnnemisList  = {
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null },
            { null,null,null,null,null,null,null,null,null,null,null }};
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

    // Start is called before the first frame update
    void Start()    
    {
        
    }

    private void Update()
    {
        int x;
        int y;
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (mapEnnemisList[i, j] != null)
                {
                    x = (int)mapEnnemisList[i, j].transform.position.x;
                    if (mapEnnemisList[i, j].transform.position.x > x + .5f)
                    {
                        x++;
                    }
                    y = (int)mapEnnemisList[i, j].transform.position.y;
                    if (mapEnnemisList[i, j].transform.position.y > y + .5f)
                    {
                        y++;
                    }
                    //on vérifie que l'ennemis enregistré est toujours là
                    if (x != i || y != j)
                    { //il y est plus
                        mapEnnemisList[i, j] = null;
                    }
                }
            }
        }
    }

    public void Build()
    {
        GameObject newObject;
        GameObject qqc;
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                switch (testMap[10 - i, j])
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
                    case 21:
                        newObject = entree;
                        break;
                    case 22:
                        newObject = sortie;
                        break;
                    default:
                        newObject = mur_cassable;
                        break;
                }
                qqc = Instantiate(newObject, new Vector3(j, i), Quaternion.identity);
                qqc.transform.SetParent(transform, false);
                mapItemsList[j, i] = qqc.GetComponent<MapItem>();
                if (newObject == entree)
                {
                    positionEntree = qqc.transform.position;
                }
            }
        }

        qqc = Instantiate(zombie, new Vector3(4, 5), Quaternion.identity);
        qqc.transform.SetParent(transform, false);
        mapEnnemisList[4, 5] = qqc;
        qqc.GetComponent<Zombie>().waypoints = new Transform[2];

        GameObject w = Instantiate(waypoint, new Vector3(1, 7, 0), Quaternion.identity);
        qqc.GetComponent<Zombie>().waypoints[0] = w.transform;

        w = Instantiate(waypoint, new Vector3(5, 4, 0), Quaternion.identity);
        qqc.GetComponent<Zombie>().waypoints[1] = w.transform;

        qqc.GetComponent<Zombie>().InitPath();
    }
}