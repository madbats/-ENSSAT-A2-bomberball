using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //Variables
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

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

    //Méthodes

    //Cette méthode gèle le jeu et affiche le menu pause quand on appuie sur la touche echap
    void Paused()
    {
        //Afficher le menu Pause
        pauseMenuUI.SetActive(true);

        //Arrêter le temps du jeu
        Time.timeScale = 0;

        //Changer le statut du jeu
        gameIsPaused = true;
    }

    //Cette méthode permet de quitter le menu pause et de relancer le jeu. (bouton "resume" ou appui sur la touche echap)
    public void Resume()
    {
        //Désactiver le menu Pause
        pauseMenuUI.SetActive(false);

        //Relancer le temps du jeu
        Time.timeScale = 1;

        //Changer le statut du jeu
        gameIsPaused = false;
    }

    //Cette méthode permet d'aller au menu principal quand on clique sur le bouton Exit du menu de pause
    public void LoadMainMenu()
    {
        /*
        //On reprend le jeu avant d'aller au menu principal 
        Resume();
        DataManager.instance.Save();
         */
    }
}

