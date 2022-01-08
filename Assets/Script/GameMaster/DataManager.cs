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
            Debug.LogWarning("Il y a plus d'une instance de DataManager dans la scène");
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

    //Méthodes

    public void Save()
    {
        /*
         //La clé correspond au nom associé à la valeur.
         PlayerPrefs.SetInt(key, valeur);
         //(Pour windows) Les données sont sauvegardés dans : Ordinateur\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\bomberball

         */
        PlayerPrefs.SetInt("Vie",LifeManager.instance.vieNiveau);
        PlayerPrefs.SetInt("Score", ScoreManager.instance.scorePartie);

    }

    public void Load()
    {
        /*
         valeur = PlayerPrefs.GetInt(key, defaultValue); //defaultValue est une valeur par défaut si key ne correspond à aucune valeur.
         */
        LifeManager.instance.vieNiveau = PlayerPrefs.GetInt("Vie", 3);
        ScoreManager.instance.scorePartie = PlayerPrefs.GetInt("Score", 0);
    }
}
