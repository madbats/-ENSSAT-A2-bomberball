using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemis : MonoBehaviour
{
    public int scoreValue;
    public int[] pointA; //[x,y]
    public int[] pointB; //[x,y]

    public int vitesse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill() { 
        GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau += scoreValue;
        Destroy(gameObject);
    }
}
