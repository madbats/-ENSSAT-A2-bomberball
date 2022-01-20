using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeManagerCreator : MonoBehaviour
{
    public Text vieDisplay;
    public int vieNiveau;
    public bool hasGodMode;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (vieNiveau > 0)
        {
            vieDisplay.text = "Vie : " + vieNiveau;
        }
    }

    public void Death()
    {
        if (!hasGodMode)
        {
            vieNiveau--;
            Respawn();
            if (vieNiveau < 1)
            {
                vieDisplay.text = "Game Over";
                gameObject.GetComponent<GameMasterCreator>().GameOver();
            }
        }
    }

    void Respawn()
    {
        player.transform.position = GameObject.Find("Map").GetComponent<MapCreator>().positionEntree;
    }


    public void Reset(int lives)
    {
        vieNiveau = lives;
        Respawn();
    }
}
