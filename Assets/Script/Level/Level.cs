using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameObject sol;
    public GameObject mur_cassable;
    public GameObject mur_cassable_puissance;
    public GameObject mur_cassable_deplacement;
    public GameObject mur_cassable_poussee;
    public GameObject mur_cassable_godmode;
    public GameObject mur_incassable;
    public GameObject entree;
    public GameObject sortie;

    public GameObject zombie;
    public GameObject explorer;
    public GameObject watchman;
    public GameObject hunter;

    public string map = "";
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCLickPlay()
    {
        GameObject fond = GameObject.Find("Fond");
        GameObject line;

        for(int j = 0; j < 11; j++)
        {
            line = fond.transform.GetChild(j).gameObject;
            for(int i = 0; i < 13; i++)
            {
                if(line.transform.GetChild(i).name == "UIentree")
                {
                    map += 21;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable")
                {
                    map += 10;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable_godMode")
                {
                    map += 14;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable_poussee")
                {
                    map += 13;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable_puissance")
                {
                    map += 11;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable_vitesse")
                {
                    map += 12;
                }
                else if (line.transform.GetChild(i).name == "UImur_incassable")
                {
                    map += 20;
                }
                else if (line.transform.GetChild(i).name == "UIsortie")
                {
                    map += 22;
                }
                else if (line.transform.GetChild(i).name == "UIsol")
                {
                    map += "00";
                }
                else if (line.transform.GetChild(i).name == "UIzombie")
                {
                    map += 30;
                }
                else if (line.transform.GetChild(i).name == "UIexplorer")
                {
                    map += 31;
                }
                else if (line.transform.GetChild(i).name == "UIwatchman")
                {
                    map += 32;
                }
                else if (line.transform.GetChild(i).name == "UIhunter")
                {
                    map += 33;
                }
            }
        }
        PlayerPrefs.SetString("map", map);
        PlayerPrefs.Save();
    }

    public void OnCLickSave()
    {
        GameObject fond = GameObject.Find("Fond");
        GameObject line;

        for (int j = 0; j < 11; j++)
        {
            line = fond.transform.GetChild(j).gameObject;
            for (int i = 0; i < 13; i++)
            {
                if (line.transform.GetChild(i).name == "UIentree")
                {
                    map += 21;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable")
                {
                    map += 10;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable_godMode")
                {
                    map += 14;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable_poussee")
                {
                    map += 13;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable_puissance")
                {
                    map += 11;
                }
                else if (line.transform.GetChild(i).name == "UImur_cassable_vitesse")
                {
                    map += 12;
                }
                else if (line.transform.GetChild(i).name == "UImur_incassable")
                {
                    map += 20;
                }
                else if (line.transform.GetChild(i).name == "UIsortie")
                {
                    map += 22;
                }
                else if (line.transform.GetChild(i).name == "UIsol")
                {
                    map += "00";
                }
                else if (line.transform.GetChild(i).name == "UIzombie")
                {
                    map += 30;
                }
                else if (line.transform.GetChild(i).name == "UIexplorer")
                {
                    map += 31;
                }
                else if (line.transform.GetChild(i).name == "UIwatchman")
                {
                    map += 32;
                }
                else if (line.transform.GetChild(i).name == "UIhunter")
                {
                    map += 33;
                }
            }
        }
        PlayerPrefs.SetString("map", map);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
