using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère la vie du joueur, son affichage et sa mort
/// </summary>
public class LifeManager : MonoBehaviour
{
    public Text vieDisplay;
    public int vieNiveau;

    /// <summary>
    /// Indique si le joueur à un god mode active
    /// </summary>
    public bool hasGodMode;
    public GameObject player;
    public static LifeManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LifeManager dans la scéne");
            return;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (vieNiveau > 0)
        {
            vieDisplay.text = "Vie : " + vieNiveau;
        }
    }

    /// <summary>
    /// Appelé pour signaler la mort du joueur.
    /// Affiche le game over si le joueur n'a plus de vie
    /// </summary>
    public void Death()
    {
        if (!hasGodMode) {
            vieNiveau--;
            Respawn();
            if (vieNiveau < 1)
            {
                vieDisplay.text = "Game Over";
                gameObject.GetComponent<GameMaster>().GameOver();
            }
        }
    }

    /// <summary>
    /// Remet la joueur à la position initiale
    /// </summary>
    void Respawn()
    {
        player.transform.position = GameObject.Find("Map").GetComponent<Map>().positionEntree;
    }

    /// <summary>
    /// Met le nombre de vie é la valeur donnée et remet le joueur é la position initiale
    /// </summary>
    /// <param name="lives"></param>
    public void Reset(int lives)
    {
        vieNiveau = lives;
        Respawn();
    }
}
