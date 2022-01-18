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
            Debug.LogWarning("Il y a plus d'une instance de DataManager dans la scène");
            return;
        }
    }

    /////////////////////////////////////////////    METHODES     /////////////////////////////////////////////////////////////////
    
    //Sauvegarde effectué lorsqu'on quitte une partie depuis le menu Pause
    public void Save()
    {
        /*
         //La clé correspond au nom associé à la valeur.
         PlayerPrefs.SetInt(key, valeur);
         //(Pour windows) Les données sont sauvegardés dans : Ordinateur\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\bomberball
         */
        //PlayerPrefs.SetInt("Vie", this.gameObject.GetComponent<LifeManager>().vieNiveau);
        PlayerPrefs.SetInt("Score", GetComponent<ScoreManager>().scorePartie);
        PlayerPrefs.SetInt("Seed", GetComponent<GameMaster>().seed);
        PlayerPrefs.SetInt("CampaignLevel", GetComponent<GameMaster>().number);
    }

    //Chargement effectué lorsqu'on décide de continuer une partie (dans le menu principal)
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
        //On enregistre dans PlayerPrefs les valeurs par défauts.
        PlayerPrefs.SetInt("Vie", this.gameObject.GetComponent<GameMaster>().maxLives);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Seed", 0);
        PlayerPrefs.SetInt("CampaignLevel", 0);
    }*/
}
