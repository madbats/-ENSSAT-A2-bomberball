using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bonus GodMod present sur la map
/// </summary>
public class BonusGodMod : Bonus
{

    override
    public bool OnConsumption() 
    {
        if (!consumed)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = onceConsumed;
            consumed = true;
            // = 5.0f;
            power += 1;

            startTime = Time.time;
            Debug.Log("Début Bonus God Mod");
            GameObject.Find("GameMaster").GetComponent<LifeManager>().hasGodMode = true;
            GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau += scoreValue;
            return true;
        }
        return false;
    }

    override
    public bool CheckEnd()
    {
        if (Time.time >= startTime + duration && !end) 
        {
            Debug.Log("Fin Bonus God Mod");
            power -= 1;
            GameObject.Find("GameMaster").GetComponent<LifeManager>().hasGodMode = false;
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
