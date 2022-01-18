using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GameMaster est le script de base pour l'entité GameMaster.
/// Il gère la création des niveaux
/// </summary>
public class GameMaster : MonoBehaviour
{
    public int seed; 
    public int number;

    public GameObject map;
    public GameObject player;
    public GameObject gameOver;
    public GameObject gameWon;
    public bool endOfGame = false;
    public GameObject playerObject;
    GameObject mapObject;
    public int maxLives;

    public float vitesseTime=0;
    public float puissanceTime = 0;
    public float pousseeTime = 0;
    public float godModeTime = 0;

    public Text vitesseText;
    public Text puissanceText;
    public Text pousseeText;
    public Text godModeText;

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    /// <summary>
    /// Next génère le prochain niveau du jeu
    /// </summary>
    public void Next()
    {
        number++;
        NewGame();
    }

    /// <summary>
    /// Again est appelé pour regénérer le dernier niveau
    /// </summary>
    public void Again()
    {
        NewGame();
    }

    /// <summary>
    /// NewGame génère le niveau pour le numéro et seed
    /// </summary>
    void NewGame()
    {
        endOfGame = false;
        mapObject = Instantiate(map, gameObject.transform.position, Quaternion.identity);
        mapObject.name = "Map";
        mapObject.transform.SetParent(transform.parent, false);
        playerObject = Instantiate(player, gameObject.transform.position, Quaternion.identity);
        playerObject.name = "Player";
        playerObject.transform.SetParent(transform.parent, false);

        mapObject.GetComponent<Map>().Build(this.GetComponent<MapGenerator>().FetchMap(seed, number),this.GetComponent<MapGenerator>().PlaceEnnemie());
        //this.GetComponent<AstarPath>().Scan();
        this.gameObject.GetComponent<ScoreManager>().Reset();
        this.gameObject.GetComponent<LifeManager>().player = playerObject;
        this.gameObject.GetComponent<LifeManager>().Reset(maxLives);
        Destroy(GameObject.Find("GameMenu"));
    }

    /// <summary>
    /// Indique la fin du niveau car le joueur à gagné
    /// </summary>
    public void Win()
    {
        endOfGame = true;
        Destroy(playerObject);
        Destroy(mapObject);
        Destroy(GameObject.Find("Bomb(Clone)"));
        GameObject gameWonObject = Instantiate(gameWon, gameObject.transform.position, Quaternion.identity);
        gameWonObject.transform.SetParent(transform.parent, false);
        gameWonObject.name = "GameMenu";
        this.gameObject.GetComponent<ScoreManager>().Win();
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(Next);
    }

    /// <summary>
    /// Indique la fin du niveau car le joueur à perdu
    /// </summary>
    public void GameOver()
    {
        endOfGame = true;
        Destroy(playerObject);
        Destroy(mapObject);
        Destroy(GameObject.Find("Bomb(Clone)"));
        GameObject gameOverObject = Instantiate(gameOver, gameObject.transform.position, Quaternion.identity);
        gameOverObject.transform.SetParent(transform.parent, false);
        gameOverObject.name = "GameMenu";
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(Again);
    }

    /// <summary>
    /// Update is called once per frame.
    /// Modifie l'interface pour afficher le temps restant
    /// </summary>
    void Update()
    {
        vitesseText.text = "" + vitesseTime;
        puissanceText.text = "" + puissanceTime;
        pousseeText.text = "" + pousseeTime;
        godModeText.text = "" + godModeTime;
    }
}
