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


    // Start is called before the first frame update
    void Start()
    {
       
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
        PlayerPrefs.SetInt("Vie", this.gameObject.GetComponent<LifeManager>().vieNiveau);
        PlayerPrefs.SetInt("Score", this.gameObject.GetComponent<ScoreManager>().scorePartie);
        PlayerPrefs.SetInt("Seed", mapObject.GetComponent<Map>().seed);
        PlayerPrefs.SetInt("CampaignLevel", mapObject.GetComponent<Map>().number);


    }

    //Chargement effectué lorsqu'on décide de continuer une partie (dans le menu principal)
    public int[] Load()
    {
        //INITIALISATION
        int[] tab = new int[4];
        
        /*
         valeur = PlayerPrefs.GetInt(key, defaultValue); //defaultValue est une valeur par défaut si key ne correspond à aucune valeur.
         */
        tab[0] = PlayerPrefs.GetInt("Vie", 3);
        tab[1] = PlayerPrefs.GetInt("Score", 0);
        tab[2] = PlayerPrefs.GetInt("Seed", 0);
        tab[3] = PlayerPrefs.GetInt("CampaignLevel", 0);

        return tab;
    }

    public void Reset()
    {
        //On enregistre dans PlayerPrefs les valeurs par défauts.
        PlayerPrefs.SetInt("Vie", this.gameObject.GetComponent<GameMaster>().maxLives);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Seed", 0);
        PlayerPrefs.SetInt("CampaignLevel", 0);
    }
}
