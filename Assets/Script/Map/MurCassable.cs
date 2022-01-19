using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mur cassable de la carte
/// </summary>
public class MurCassable : Mur
{
    public GameObject sol;
    public int scoreValue;
    public double duration;
    public int puissance;

    // Start is called before the first frame update
    void Start()
    {
        this.isBreakable = true;
    }
    
    public void OnDestroy()
    {
    }
    
    /// <summary>
    /// Déclanché par l'explosion. Détruit le mur et le remplace par un sol
    /// </summary>
    public void OnBreak(){
        GameObject qqc;

        qqc=Instantiate(sol, gameObject.transform.position, Quaternion.identity);
        qqc.transform.SetParent(transform.parent,false);
        qqc.transform.parent.GetComponent<Map>().mapItemsList[(int)transform.position.x, (int)transform.position.y] = qqc.GetComponent<MapItem>();
        GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau+= scoreValue;
        if (qqc.GetComponent<Bonus>())
        {
            qqc.GetComponent<Bonus>().duration = (float)duration;

            if (qqc.GetComponent<BonusPuissance>())
            {
                qqc.GetComponent<BonusPuissance>().puissance = puissance;
            }
        }
        Destroy(this.gameObject);
    }
}
