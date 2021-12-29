using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    public int puissance;
    public bool poussee;
    Bonus[] bonusList;
    int nbBonus;

    public float vitesseTime = 0;
    public float puissanceTime = 0;
    public float pousseeTime = 0;
    public float godModeTime = 0;

    void Start()
    {
        bonusList = new Bonus[30];
        nbBonus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameMaster gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        MapItem[,] mapItemsList = GameObject.Find("Map").GetComponent<Map>().mapItemsList;

        int x = (int)this.transform.position.x;
        int y = (int)this.transform.position.y;
        
        if (mapItemsList[x, y] is Bonus)
        {
            if (((Bonus)mapItemsList[x, y]).OnConsumption())
            {
                bonusList[nbBonus] = (Bonus)mapItemsList[x, y];
                nbBonus += 1;
            }
            
        }
        vitesseTime = 0;
        puissanceTime = 0;
        pousseeTime = 0;
        godModeTime = 0;
        foreach (Bonus bonus in bonusList)
        {
            
            if(bonus != null)
            {
                if (bonus.CheckEnd())
                {
                    bonus.Destruction();

                }
                else
                {
                    if(bonus is BonusVitesse)
                    {
                        vitesseTime =Mathf.Max(vitesseTime, bonus.RemainningTime());
                    }
                    if (bonus is BonusPuissance)
                    {
                        puissanceTime = Mathf.Max(puissanceTime, bonus.RemainningTime());
                    }
                    if (bonus is BonusPoussee)
                    {
                        pousseeTime =Mathf.Max(pousseeTime, bonus.RemainningTime());
                    }
                    if (bonus is BonusGodMod)
                    {
                        godModeTime =Mathf.Max(godModeTime, bonus.RemainningTime());
                    }
                }
            }
        }

        gm.vitesseTime = vitesseTime;
        gm.puissanceTime = puissanceTime;
        gm.pousseeTime = pousseeTime;
        gm.godModeTime = godModeTime;
        
    }

}
