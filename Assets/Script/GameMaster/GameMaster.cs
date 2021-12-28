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
    GameObject playerObject;
    GameObject mapObject;
    public int maxLives;

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        mapObject = Instantiate(map, gameObject.transform.position, Quaternion.identity);
        mapObject.name = "Map";
        mapObject.transform.SetParent(transform.parent, false);
        playerObject = Instantiate(player, gameObject.transform.position, Quaternion.identity);
        playerObject.name = "Player";
        playerObject.transform.SetParent(transform.parent, false);
        mapObject.GetComponent<Map>().Build();
        this.gameObject.GetComponent<ScoreManager>().Reset();
        this.gameObject.GetComponent<LifeManager>().player = playerObject;
        this.gameObject.GetComponent<LifeManager>().Reset(maxLives);
        Destroy(GameObject.Find("GameMenu"));
    }

    public void Win()
    {
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
        Destroy(playerObject);
        Destroy(mapObject);
        GameObject gameOverObject = Instantiate(gameOver, gameObject.transform.position, Quaternion.identity);
        gameOverObject.transform.SetParent(transform.parent, false);
        gameOverObject.name = "GameMenu";
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(NewGame);
    }
}
