using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject map;
    public GameObject player;
    public GameObject gameOver;
    public GameObject gameWon;
    public bool endOfGame = false;
    public GameObject playerObject;
    GameObject mapObject;
    public int maxLives;

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        endOfGame = false;
        mapObject = Instantiate(map, gameObject.transform.position, Quaternion.identity);
        mapObject.name = "Map";
        mapObject.transform.SetParent(transform.parent, false);
        playerObject = Instantiate(player, gameObject.transform.position, Quaternion.identity);
        playerObject.name = "Player";
        playerObject.transform.SetParent(transform.parent, false);
        mapObject.GetComponent<Map>().Build();
        this.GetComponent<AstarPath>().Scan();
        this.gameObject.GetComponent<ScoreManager>().Reset();
        this.gameObject.GetComponent<LifeManager>().player = playerObject;
        this.gameObject.GetComponent<LifeManager>().Reset(maxLives);
        Destroy(GameObject.Find("GameMenu"));
    }

    public void Win()
    {
        endOfGame = true;
        Destroy(playerObject);
        Destroy(mapObject);
        GameObject gameWonObject = Instantiate(gameWon, gameObject.transform.position, Quaternion.identity);
        gameWonObject.transform.SetParent(transform.parent, false);
        gameWonObject.name = "GameMenu";
        this.gameObject.GetComponent<ScoreManager>().Win();
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(NewGame);
    }

    public void GameOver()
    {
        endOfGame = true;
        Destroy(playerObject);
        Destroy(mapObject);
        GameObject gameOverObject = Instantiate(gameOver, gameObject.transform.position, Quaternion.identity);
        gameOverObject.transform.SetParent(transform.parent, false);
        gameOverObject.name = "GameMenu";
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(NewGame);
    }

    void Update()
    {
        if (!endOfGame)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (mapObject.GetComponent<Map>().mapEnnemisList[i, j] != null)
                    {
                        Debug.Log("ennemis sur case " + i + " , " + j);
                    }
                }
            }
        }
    }
}
