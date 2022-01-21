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

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;
    public bool musiquebonus = false;
    public bool musiquechange = false;
    public int nummusique = 0;

    void Start()
    {
        bonusList = new Bonus[30];
        nbBonus = 0;

        audioSource = GameObject.Find("Musique").GetComponent<AudioSource>();
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
        musiquebonus = false;
        foreach (Bonus bonus in bonusList)
        {
            if(bonus != null)
            {
                musiquebonus = true;
                if (bonus.CheckEnd())
                {
                    bonus.Destruction();

                }
                else
                {
                    musiquebonus = true;
                    if (bonus is BonusVitesse)
                    {
                        vitesseTime =Mathf.Max(vitesseTime, bonus.RemainningTime());
                        if (nummusique != 1  && !musiquechange)
                        {
                            musiquechange = true;
                            audioSource.clip = audioClipArray[1];
                            nummusique = 1;
                        }
                    }
                    if (bonus is BonusPuissance)
                    {
                        puissanceTime = Mathf.Max(puissanceTime, bonus.RemainningTime());
                        if (nummusique != 2 && !musiquechange)
                        {
                            musiquechange = true;
                            audioSource.clip = audioClipArray[2];
                            nummusique = 2;
                        }
                    }
                    if (bonus is BonusPoussee)
                    {
                        pousseeTime =Mathf.Max(pousseeTime, bonus.RemainningTime());
                        if (nummusique != 3 && !musiquechange)
                        {
                            musiquechange = true;
                            audioSource.clip = audioClipArray[3];
                            nummusique = 3;
                        }
                    }
                    if (bonus is BonusGodMod)
                    {
                        godModeTime =Mathf.Max(godModeTime, bonus.RemainningTime());
                        if (nummusique != 4 && !musiquechange)
                        {
                            audioSource.clip = audioClipArray[4];
                            musiquechange = true;
                            nummusique = 4;
                        }
                    }

                }
            }
            
        }

        if (!musiquebonus)
        {
            if (nummusique != 0)
            {
                musiquechange = true;
                audioSource.clip = audioClipArray[0];
                nummusique = 0;
            }
        }

        if (musiquechange)
        {
            audioSource.Play();
            musiquechange = false;
        }

        gm.vitesseTime = vitesseTime;
        gm.puissanceTime = puissanceTime;
        gm.pousseeTime = pousseeTime;
        gm.godModeTime = godModeTime;
        
    }

}
