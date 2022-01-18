using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    //Variables
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public string s;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    //M�thodes

    //Cette m�thode g�le le jeu et affiche le menu pause quand on appuie sur la touche echap
    void Paused()
    {
        //Arr�ter le temps du jeu
        Time.timeScale = 0;

        //Afficher le menu Pause
        pauseMenuUI.SetActive(true);
           
        //Changer le statut du jeu
        gameIsPaused = true;
    }

    //Cette m�thode permet de quitter le menu pause et de relancer le jeu. (bouton "resume" ou appui sur la touche echap)
    public void Resume()
    {
        //D�sactiver le menu Pause
        pauseMenuUI.SetActive(false);

        //Changer le statut du jeu
        gameIsPaused = false;

        //Relancer le temps du jeu
        Time.timeScale = 1;
    }

    //Cette m�thode permet d'aller au menu principal quand on clique sur le bouton Exit du menu de pause
    public void LoadMainMenu()
    {
        //On reprend le jeu avant d'aller au menu principal 
        GetComponent<GameMaster>().Save();

        //On charge le menu principal
        SceneManager.LoadScene(s);
    }
}

