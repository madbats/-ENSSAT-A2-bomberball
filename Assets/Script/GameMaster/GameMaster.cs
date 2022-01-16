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

    public float vitesseTime=0;
    public float puissanceTime = 0;
    public float pousseeTime = 0;
    public float godModeTime = 0;

    public Text vitesseText;
    public Text puissanceText;
    public Text pousseeText;
    public Text godModeText;

    public static GameMaster instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameMaster dans la sc�ne");
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        //R�cup�ration des donn�es sauvegard�es dans PlayerPrefs
        int[] tab = new int[4];
        tab = this.gameObject.GetComponent<DataManager>().Load();
        int vie = tab[0];
        if(vie<=0)
        {
            vie = maxLives;
        }
        int Score = tab[1];
        int Seed = tab[2];
        int CampaignLevel = tab[3];

        endOfGame = false;
        mapObject = Instantiate(map, gameObject.transform.position, Quaternion.identity);
        mapObject.name = "Map";
        mapObject.transform.SetParent(transform.parent, false);
        playerObject = Instantiate(player, gameObject.transform.position, Quaternion.identity);
        playerObject.name = "Player";
        playerObject.transform.SetParent(transform.parent, false);
        
        //Initialisation de la map
        mapObject.GetComponent<Map>().Build();
        mapObject.GetComponent<Map>().seed = Seed;
        mapObject.GetComponent<Map>().number = CampaignLevel;

        this.GetComponent<AstarPath>().Scan();

        //Initialisation du score
        this.gameObject.GetComponent<ScoreManager>().Reset();//Remise � 0 du score de la partie
        this.gameObject.GetComponent<ScoreManager>().scorePartie = Score;//Chargement du score global enregistr�

        this.gameObject.GetComponent<LifeManager>().player = playerObject;

        //this.gameObject.GetComponent<LifeManager>().Reset(maxLives);
        //ALTERNATIVE
        this.gameObject.GetComponent<LifeManager>().Reset(vie);

        this.gameObject.GetComponent<PauseManager>().Resume();
        Destroy(GameObject.Find("GameMenu"));
        
    }

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

        //Sauvegarde des donn�es de la partie (apr�s l'appel � Win() pour prendre en compte le nouvel ajout au score)
        this.gameObject.GetComponent<DataManager>().Save();

        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(NewGame);
    }

    public void GameOver()
    {
        endOfGame = true;
        Destroy(playerObject);
        Destroy(mapObject);
        Destroy(GameObject.Find("Bomb(Clone)"));
        GameObject gameOverObject = Instantiate(gameOver, gameObject.transform.position, Quaternion.identity);
        gameOverObject.transform.SetParent(transform.parent, false);
        gameOverObject.name = "GameMenu";
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(NewGame);
    }

    // Update is called once per frame
    void Update()
    {
        vitesseText.text = "" + vitesseTime;
        puissanceText.text = "" + puissanceTime;
        pousseeText.text = "" + pousseeTime;
        godModeText.text = "" + godModeTime;
        /*if (!endOfGame)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (mapObject.GetComponent<Map>().mapEnnemisList[i, j] != null && !(mapObject.GetComponent<Map>().mapEnnemisList[i, j].GetComponent<Bomb>()))
                    {
                        //Debug.Log("ennemis sur case " + i + " , " + j);
                        if (Vector2.Distance(playerObject.transform.position, mapObject.GetComponent<Map>().mapEnnemisList[i, j].transform.position) < 0.3f)
                        {
                            this.GetComponent<LifeManager>().Death();
                            this.GetComponent<ScoreManager>().scoreNiveau += 20;
                        }
                    }
                }
            }
        }*/
    }
}
