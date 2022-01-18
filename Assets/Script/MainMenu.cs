using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string s;
    public GameObject p;
    public GameObject saves;

    public void Continue()
    {
        gameObject.SetActive(false);
        saves.SetActive(true);
    }

    public void NouvellePartie()
    {
        SceneManager.LoadScene(s);
    }

    public void Quitter()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void ParametreMenu()
    {
        gameObject.SetActive(false);
        p.SetActive(true);
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //PlayerPrefs.DeleteAll();
        saves.GetComponent<MenuSaves>().SaveGame();
    }
}
