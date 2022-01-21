using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bonus Puissance present sur la map
/// </summary>
public class BonusPuissanceCreator : Bonus
{

    /// <summary>
    /// Agmentation de la puissance fournie par la bonus
    /// </summary>
    public int puissance=1;

    override
    public bool OnConsumption() //Gestion de la consommation de l'item
    {
        if (!consumed)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = onceConsumed;
            consumed = true;
            //duration = 30.0f;
            power += 1;

            startTime = Time.time;
            Debug.Log("D�but Bonus Puissance");

            //Effet du bonus 
            Debug.Log(PlayerBonusCreator.puissance);
            PlayerBonusCreator.puissance += 1;
            Debug.Log("Poweeeer!");
            Debug.Log(PlayerBonusCreator.puissance);

            GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau += scoreValue;
            return true;
        }
        return false;
    }

    override
    public bool CheckEnd()
    {
        if (Time.time >= startTime + duration && !end) //Supprimer le bonus de la liste ou assez longue ? Problème avec le destroy ?
        {
            Debug.Log("Fin Bonus Puissance");
            power -= 1;

            //fin de l'effet bonus
            PlayerBonusCreator.puissance -= 1;


            end = true;
            return true;
        }

        return false;
    }

    override
    public void Destruction()
    {
        GameObject qqc;
        qqc = Instantiate(sol, gameObject.transform.position, Quaternion.identity);
        qqc.transform.SetParent(transform.parent, false);
        qqc.transform.parent.GetComponent<Map>().mapItemsList[(int)transform.position.x, (int)transform.position.y] = qqc.GetComponent<MapItem>();
        Destroy(this.gameObject);
    }
}
