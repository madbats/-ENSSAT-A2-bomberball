using UnityEngine;

public class DataManager : MonoBehaviour
{
    //Attributs
    public static DataManager instance;
    public GameObject mapObject;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DataManager dans la sc�ne");
            return;
        }
    }

    /////////////////////////////////////////////    METHODES     /////////////////////////////////////////////////////////////////
    
    //Sauvegarde effectu� lorsqu'on quitte une partie depuis le menu Pause
    public void Save()
    {
        /*
         //La cl� correspond au nom associ� � la valeur.
         PlayerPrefs.SetInt(key, valeur);
         //(Pour windows) Les donn�es sont sauvegard�s dans : Ordinateur\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\bomberball
         */
        //PlayerPrefs.SetInt("Vie", this.gameObject.GetComponent<LifeManager>().vieNiveau);
        PlayerPrefs.SetInt("Score", GetComponent<ScoreManager>().scorePartie);
        PlayerPrefs.SetInt("Seed", GetComponent<GameMaster>().seed);
        PlayerPrefs.SetInt("CampaignLevel", GetComponent<GameMaster>().number);
    }

    //Chargement effectu� lorsqu'on d�cide de continuer une partie (dans le menu principal)
    public void Load()
    {
        //INITIALISATION
        GetComponent<ScoreManager>().scorePartie = PlayerPrefs.GetInt("Score", -1);
        GetComponent<GameMaster>().seed = PlayerPrefs.GetInt("Seed", -1);
        GetComponent<GameMaster>().number = PlayerPrefs.GetInt("CampaignLevel", 1);
    }
    /*
    public void Reset()
    {
        //On enregistre dans PlayerPrefs les valeurs par d�fauts.
        PlayerPrefs.SetInt("Vie", this.gameObject.GetComponent<GameMaster>().maxLives);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Seed", 0);
        PlayerPrefs.SetInt("CampaignLevel", 0);
    }*/
}
