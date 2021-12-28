using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeManager : MonoBehaviour
{
    public Text vieDisplay;
    public int vieNiveau;
    public bool hasGodMode;
    public GameObject player;
    public GameObject gameOver;

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
        if (!hasGodMode) {
            vieNiveau--;
            Respawn();
            if (vieNiveau < 1)
            {
                GameOver();
            }
        }
    }

    void Respawn()
    {
        player.transform.position = GameObject.Find("Map").GetComponent<Map>().positionEntree;
    }

    void GameOver()
    {
        vieDisplay.text = "Game Over";
        Destroy(GameObject.Find("Map"));
        Destroy(player);
        GameObject gameOverObject = Instantiate(gameOver, gameObject.transform.position, Quaternion.identity);
        gameOverObject.transform.SetParent(transform.parent, false);
        gameOverObject.name = "GameOver";
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(GameObject.Find("GameMaster").GetComponent<GameMaster>().NewGame);
    }

    public void Reset()
    {
        vieNiveau= 2;
        Respawn();
    }
}
