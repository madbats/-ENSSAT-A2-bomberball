using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// G�re le score du joueur, son affichage et sa modification
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public Text scoreDisplay;
    public int scoreNiveau = 0;
    public int scorePartie = 0;

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "Score : " + scoreNiveau;
    }

    /// <summary>
    /// Lors d'une victoire du joueur, ajoute le score 
    /// </summary>
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
