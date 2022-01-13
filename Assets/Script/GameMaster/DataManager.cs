using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //Attributs
    public static DataManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DataManager dans la sc�ne");
            return;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    /////////////////////////////////////////////    METHODES     /////////////////////////////////////////////////////////////////
    
    //Sauvegarde effectu� lorsqu'on quitte une partie depuis le menu Pause
    public void SaveOnExit()
    {
        /*
         //La cl� correspond au nom associ� � la valeur.
         PlayerPrefs.SetInt(key, valeur);
         //(Pour windows) Les donn�es sont sauvegard�s dans : Ordinateur\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\bomberball

         */
        PlayerPrefs.SetInt("Vie",LifeManager.instance.vieNiveau);
        PlayerPrefs.SetInt("Score", ScoreManager.instance.scorePartie);
        PlayerPrefs.SetInt("Seed", GameMaster.instance.GetComponent<Map>().seed);
        PlayerPrefs.SetInt("CampaignLevel", GameMaster.instance.GetComponent<Map>().number);


    }

    //Chargement effectu� lorsqu'on d�cide de continuer une partie (dans le menu principal)
    public void LoadOnContinue()
    {
        /*
         valeur = PlayerPrefs.GetInt(key, defaultValue); //defaultValue est une valeur par d�faut si key ne correspond � aucune valeur.
         */
        LifeManager.instance.vieNiveau = PlayerPrefs.GetInt("Vie", 3);
        ScoreManager.instance.scorePartie = PlayerPrefs.GetInt("Score", 0);
        GameMaster.instance.GetComponent<Map>().seed = PlayerPrefs.GetInt("Seed", 0);
        GameMaster.instance.GetComponent<Map>().number = PlayerPrefs.GetInt("CampaignLevel", 0);


    }
}
