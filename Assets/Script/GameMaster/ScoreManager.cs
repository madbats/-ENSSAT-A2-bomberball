using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreDisplay;
    public int scoreNiveau;
    public int scorePartie = 0;
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ScoreManager dans la sc�ne");
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreNiveau = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "Score : " + scoreNiveau;
    }

    public void Win()
    {
        scorePartie += scoreNiveau; 
        GameObject.Find("ScoreTotal").GetComponent<Text>().text = "Score Total: " + scorePartie;
        Reset();
    }

    public void Reset()
    {
        scoreNiveau=0;
    }
}
