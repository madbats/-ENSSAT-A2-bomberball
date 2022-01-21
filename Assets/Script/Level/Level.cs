using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public bool hasExit = false;
    public bool hasEnter = false;
    public bool validMap = true;

    int nbEnter;
    int nbExit;

    public Button playButton;
    public Button saveButton;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject fond = GameObject.Find("Fond");
        GameObject line;
        hasEnter = false;
        hasExit = false;
        validMap = true;
        nbEnter = 0;
        nbExit = 0;

        for (int j = 0; j < 11; j++)
        {
            line = fond.transform.GetChild(j).gameObject;
            if(line.transform.childCount != 13)
            {
                validMap = false;
            }
            else
            {
                for (int i = 0; i < 13; i++)
                {
                    if (line.transform.GetChild(i).gameObject.name == "UIentree")
                    {
                        hasEnter = true;
                        nbEnter++;
                    }
                    if (line.transform.GetChild(i).gameObject.name == "UIsortie")
                    {
                        hasExit = true;
                        nbExit++;
                    }
                }
            }
        }

        if(nbEnter > 1 || nbExit > 1)
        {
            validMap = false;
        }

        if(hasEnter && hasExit)
        {
            playButton.interactable = true;
            //playButton.gameObject.GetComponent<MeshRenderer>().material.color = new Color(250, 250, 250, 250);
            saveButton.interactable = true;
            //saveButton.gameObject.GetComponent<MeshRenderer>().material.color = new Color(250, 250, 250, 250);
        }

        if(!hasEnter || !hasExit || !validMap)
        {
            playButton.interactable = false;
            //playButton.gameObject.GetComponent<MeshRenderer>().material.color = new Color(250, 250, 250, 125);
            saveButton.interactable = false;
            //saveButton.gameObject.GetComponent<MeshRenderer>().material.color = new Color(250, 250, 250, 125);
        }
    }

    public void Lecture()
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
    }

    public void OnCLickPlay()
    {
        Lecture();
        SceneManager.LoadScene("CreatedLevel");
    }

    public void OnCLickSave()
    {
        Lecture();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnCLickReset()
    {
        SceneManager.LoadScene("LevelCreator");
    }
}
