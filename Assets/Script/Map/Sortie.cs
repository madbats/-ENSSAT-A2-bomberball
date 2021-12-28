using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortie : Sol
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x == transform.position.x && player.transform.position.y == transform.position.y) 
        {
        
            GameObject.Find("GameMaster").GetComponent<GameMaster>().Win();
        }
    }
}
