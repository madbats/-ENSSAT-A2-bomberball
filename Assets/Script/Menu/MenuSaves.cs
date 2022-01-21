using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSaves : MonoBehaviour
{
    public GameObject main;
    private int gamePlayed = -1;
    public GameObject save1;
    public GameObject delete1;
    public GameObject save2;
    public GameObject delete2;
    public GameObject save3;
    public GameObject delete3;
    public GameObject save4;
    public GameObject delete4;

    void OnEnable()
    {
        LoadSaves();
    }

    private void LoadSaves()
    {
        if (PlayerPrefs.GetInt("CampaignLevel1", -1) != -1)
        {
            save1.GetComponentsInChildren<Text>()[0].text = "Load Save 1 Score :" + PlayerPrefs.GetInt("Score1", -1);
            delete1.SetActive(true);
            delete1.GetComponent<Button>().onClick.AddListener(delegate { Delete(1); });
        }
        else
        {
            save1.GetComponentsInChildren<Text>()[0].text = "New Save 1 ";
            delete1.SetActive(false);
        }
        save1.GetComponent<Button>().onClick.AddListener(delegate { LoadSave(1); });

        if (PlayerPrefs.GetInt("CampaignLevel2", -1) != -1)
        {
            save2.GetComponentsInChildren<Text>()[0].text = "Load Save 2 Score :" + PlayerPrefs.GetInt("Score2", -1);
            delete2.SetActive(true);
            delete2.GetComponent<Button>().onClick.AddListener(delegate { Delete(2); });
        }
        else
        {
            save2.GetComponentsInChildren<Text>()[0].text = "New Save 2 ";
            delete2.SetActive(false);
        }
        save2.GetComponent<Button>().onClick.AddListener(delegate { LoadSave(2); });

        if (PlayerPrefs.GetInt("CampaignLevel3", -1) != -1)
        {
            save3.GetComponentsInChildren<Text>()[0].text = "Load Save 3 Score :" + PlayerPrefs.GetInt("Score3", -1);
            delete3.SetActive(true);
            delete3.GetComponent<Button>().onClick.AddListener(delegate { Delete(3); });
        }
        else
        {
            save3.GetComponentsInChildren<Text>()[0].text = "New Save 3 ";
            delete3.SetActive(false);
        }
        save3.GetComponent<Button>().onClick.AddListener(delegate { LoadSave(3); });

        if (PlayerPrefs.GetInt("CampaignLevel4", -1) != -1)
        {
            save4.GetComponentsInChildren<Text>()[0].text = "Load Save 4 Score :" + PlayerPrefs.GetInt("Score4", -1);
            delete4.SetActive(true);
            delete4.GetComponent<Button>().onClick.AddListener(delegate { Delete(4); });
        }
        else
        {
            save4.GetComponentsInChildren<Text>()[0].text = "New Save 4 ";
            delete4.SetActive(false);
        }
        save4.GetComponent<Button>().onClick.AddListener(delegate { LoadSave(4); });
    }

    public void LoadSave(int save)
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score" + save, 0));
        PlayerPrefs.SetInt("Seed", PlayerPrefs.GetInt("Seed" + save, -1));
        PlayerPrefs.SetInt("CampaignLevel", PlayerPrefs.GetInt("CampaignLevel" + save, 1));
        PlayerPrefs.SetInt("Game" , save);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SampleScene");
    }

    public void SaveGame()
    {
        //PlayerPrefs.DeleteAll();
        gamePlayed = PlayerPrefs.GetInt("Game", -1);
        Debug.Log("Saving game " + gamePlayed);
        if (gamePlayed!=-1)
        {
            Debug.Log("Score was " + PlayerPrefs.GetInt("Score", 0));
            Debug.Log("Seed was " + PlayerPrefs.GetInt("Seed", -1));
            Debug.Log("CampaignLevel was " + PlayerPrefs.GetInt("CampaignLevel", 1));
            PlayerPrefs.SetInt("Score"+ gamePlayed, PlayerPrefs.GetInt("Score", 0));
            PlayerPrefs.SetInt("Seed"+ gamePlayed, PlayerPrefs.GetInt("Seed", -1));
            PlayerPrefs.SetInt("CampaignLevel"+ gamePlayed, PlayerPrefs.GetInt("CampaignLevel", 1));
            PlayerPrefs.Save();
            gamePlayed = -1;
        }
    }

    public void Delete(int save)
    {
        PlayerPrefs.SetInt("Score" + save, 0);
        PlayerPrefs.SetInt("Seed" + save, -1);
        PlayerPrefs.SetInt("CampaignLevel" + save, -1);
        if(PlayerPrefs.GetInt("Game", -1) == save)
        {
            PlayerPrefs.SetInt("Game", -1);
        }
        PlayerPrefs.Save();
        LoadSaves();
    }

    public void QuitterSaves()
    {
        gameObject.SetActive(false);
        main.SetActive(true);
        PlayerPrefs.Save();
    }
}
