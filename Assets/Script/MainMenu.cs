using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string s;
    public void NouvellePartie(){
       SceneManager.LoadScene(s);
    }
    public void Quitter(){
        Application.Quit();
    }
    public void ParametreMenu(){
        //parametre.SetActive(true);
    }
}
