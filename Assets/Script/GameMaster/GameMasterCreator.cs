using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterCreator : GameMaster
{
    /*public GameObject map;
    public GameObject player;
    public GameObject gameOver;
    public GameObject gameWon;
    public bool endOfGame = false;
    public GameObject playerObject;*/
    GameObject mapObject;
    /*public int maxLives;

    public float vitesseTime = 0;
    public float puissanceTime = 0;
    public float pousseeTime = 0;
    public float godModeTime = 0;

    public Text vitesseText;
    public Text puissanceText;
    public Text pousseeText;
    public Text godModeText;*/

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    /*public void Again()
    {
        NewGame();
    }*/

    void NewGame()
    {
        endOfGame = false;
        mapObject = Instantiate(map, gameObject.transform.position, Quaternion.identity);
        mapObject.name = "Map";
        mapObject.transform.SetParent(transform.parent, false);
        
        mapObject.GetComponent<MapCreator>().Build(this.GetComponent<MapReader>().FetchMap());
        mapObject.GetComponent<MapCreator>().mapEnnemisList = this.GetComponent<MapReader>().mapEnnemisList;
        //this.GetComponent<AstarPath>().Scan();
        playerObject = Instantiate(player, mapObject.GetComponent<MapCreator>().positionEntree, Quaternion.identity);
        playerObject.name = "Player";
        playerObject.transform.SetParent(transform.parent, false);

        this.gameObject.GetComponent<ScoreManager>().Reset();
        this.gameObject.GetComponent<LifeManager>().player = playerObject;
        this.gameObject.GetComponent<LifeManager>().Reset(maxLives);
        Destroy(GameObject.Find("GameMenu"));
    }

    public new void Win()
    {
        endOfGame = true;
        Destroy(playerObject);
        Destroy(mapObject);
        Destroy(GameObject.Find("Bomb(Clone)"));
        GameObject gameWonObject = Instantiate(gameWon, gameObject.transform.position, Quaternion.identity);
        gameWonObject.transform.SetParent(transform.parent, false);
        gameWonObject.name = "GameMenu";
        this.gameObject.GetComponent<ScoreManager>().Win();
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(Again);
    }

    /*public void GameOver()
    {
        endOfGame = true;
        Destroy(playerObject);
        Destroy(mapObject);
        Destroy(GameObject.Find("Bomb(Clone)"));
        GameObject gameOverObject = Instantiate(gameOver, gameObject.transform.position, Quaternion.identity);
        gameOverObject.transform.SetParent(transform.parent, false);
        gameOverObject.name = "GameMenu";
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(Again);
    }*/

    // Update is called once per frame
    void Update()
    {
        vitesseText.text = "" + vitesseTime;
        puissanceText.text = "" + puissanceTime;
        pousseeText.text = "" + pousseeTime;
        godModeText.text = "" + godModeTime;
    }
}
