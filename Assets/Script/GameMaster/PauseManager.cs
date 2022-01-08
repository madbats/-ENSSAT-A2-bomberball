using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //Variables
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
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
    void Paused()
    {
        //Afficher le menu Pause
        pauseMenuUI.SetActive(true);

        //Arrêter le temps du jeu
        Time.timeScale = 0;

        //Changer le statut du jeu
        gameIsPaused = true;
    }

    void Resume()
    {
        //Désactiver le menu Pause
        pauseMenuUI.SetActive(false);

        //Relancer le temps du jeu
        Time.timeScale = 1;

        //Changer le statut du jeu
        gameIsPaused = false;
    }
}

