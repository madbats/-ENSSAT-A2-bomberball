using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum items { SOL = 0, 
    OBJET_PUISSANCE = 1, 
    OBJET_DEPLACEMENT = 2, 
    OBJET_POUSSEE = 3, 
    OBJET_GODMOD = 4, 
    MUR_CASSABLE = 10, 
    MUR_CASSABLE_PUISSANCE = 11, 
    MUR_CASSABLE_DEPLACEMENT = 12, 
    MUR_CASSABLE_POUSSEE = 13, 
    MUR_CASSABLE_GODMODE = 14, 
    MUR_INCASSABLE = 20 }; 

public class Map : MonoBehaviour { 
    public int[,] symboleMap = new int[11, 13]; // liste stylisés des objets de la carte
    public MapItem[,] mapItemsList = new MapItem[11,13]; //liste de tous les objets (au sens large) de la carte.     
    public int seed; //seed de la génération
    public int difficulty; //difficulté du niveau
    public int number; //Niveau de la campagne


    // Start is called before the first frame update
    void Start()    
    {      

    }      
    
    // Update is called once per frame
    void Update()     
    {

    } 
}