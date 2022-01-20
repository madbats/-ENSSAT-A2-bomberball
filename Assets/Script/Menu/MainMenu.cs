using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string s1;
    public string s2;
    public string s3;
    public GameObject p;
    public void NouvellePartie(){
        SceneManager.LoadScene(s1);
    }
    public void Quitter(){
        Application.Quit();
    }
    public void ParametreMenu(){
        p.SetActive(true);
    }

    public void Editeur(){
        SceneManager.LoadScene(s2);
    }

    public void CreatedLevel()
    {
        SceneManager.LoadScene(s3);
    }
}
