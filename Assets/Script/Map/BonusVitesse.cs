using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bonus de vitesse concommé par le joueur
/// </summary>
public class BonusVitesse : Bonus
{
    override
    public bool OnConsumption() //Gestion de la consommation de l'item
    {

        if (!consumed)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = onceConsumed;
            consumed = true;
            //duration = 5.0f;
            power += 1;

            startTime = Time.time;
            Debug.Log("D�but Bonus Vitesse pour ");
            GameObject.Find("Player").GetComponent<PlayerMovement>().movingSpeed /= 1.5f;

            GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau += scoreValue;
            return true;
        }
        return false;
    }

    override
    public bool CheckEnd()
    {
        if (Time.time >= startTime + duration && !end) //Supprimer le bonus de la liste ou assez longue ? Probl�me avec le destroy ?
        {
            Debug.Log("Fin Bonus Vitesse");
            power -= 1;
            GameObject.Find("Player").GetComponent<PlayerMovement>().movingSpeed *= 1.5f;
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
