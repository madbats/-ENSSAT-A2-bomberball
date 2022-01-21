using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La sortie sur la map
/// </summary>
public class SortieCreator : Sol
{
    public GameObject player;
    bool end;

    // Start is called before the first frame update
    void Start()
    {
        end = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            if (player.transform.position.x == transform.position.x && player.transform.position.y == transform.position.y)
            {
                end = true;
                Debug.Log("Gagne");
                GameObject.Find("GameMaster").GetComponent<GameMasterCreator>().Win();
            }
        }
    }
}
