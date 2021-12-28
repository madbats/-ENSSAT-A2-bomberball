using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeManager : MonoBehaviour
{
    public Text vieDisplay;
    public int vieNiveau;
    public bool hasGodMode;
    public Entree entree;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        vieNiveau = 3;
    }

    // Update is called once per frame
    void Update()
    {
        vieDisplay.text = "Vie : " + vieNiveau;
    }

    void Death()
    {
        if (!hasGodMode) {
            vieNiveau--;
            Reset();
            if (vieNiveau < 1)
            {
                GameOver();
            }
        }
    }

    void Reset()
    {
        player.transform.position = entree.transform.position;
    }

    void GameOver()
    {
    }
}
