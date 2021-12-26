using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public float timeLeft;
    public int x;
    public int y;
    public int puissance;
    public Sprite FirstStage;
    public Sprite SecondStage;
    public Sprite ThirdStage;

    // Start is called before the first frame update
    void Start()
    {
        x = (int) transform.position.x;
        y = (int) transform.position.y;
     	this.gameObject.GetComponent<SpriteRenderer>().sprite= FirstStage;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        switch (timeLeft) {
        	case float i when i > 5 && i <= timeLeft*0.75:
        		gameObject.GetComponent<SpriteRenderer>().sprite= SecondStage;
        		break;
        	case float i when i > 0 && i <= timeLeft*0.40:
        		gameObject.GetComponent<SpriteRenderer>().sprite= ThirdStage;
        		break;
        	case float i when i <= 0:
                Explosion();
        		break;
        	default:
        		break;
        }
    }

    void Explosion() {
        MapItem[,] mapItemsList = GameObject.Find("Map").GetComponent<Map>().mapItemsList;
        //Ennemis[,] mapEnnemisList = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList;

        /**
         * Parcourt de chaque direction (nord,sud,est,ouest) et destruction des objects/ennemis
         */
        bool blocked;

        // nord
        blocked = false;
        for (int i= y+1; i <= y + puissance && i < 13 && !blocked; i++)
        {
            if (mapItemsList[x, i] is MurIncassable)
            {
                blocked = true;
            }
            else
            {
                if (mapItemsList[x, i].isBreakable)
                {
                    ((MurCassable)mapItemsList[x, i]).OnBreak();
                }
                //if(mapEnnemisList != null)
                //{
                //    mapEnnemisList[x, i].Kill();
                //}
            }
        }
        // sud
        blocked = false;
        for (int i = y - 1; i >= y - puissance && i >0 && !blocked; i--)
        {
            if (mapItemsList[x, i] is MurIncassable)
            {
                blocked = true;
            }
            else
            {
                if (mapItemsList[x, i].isBreakable)
                {
                    ((MurCassable)mapItemsList[x, i]).OnBreak();
                }
                //if (mapEnnemisList != null)
                //{
                //    mapEnnemisList[x, i].Kill();
                //}
            }
        }
        // east
        blocked = false;
        for (int i = x + 1; i <= x + puissance && i < 13 && !blocked; i++)
        {
            if (mapItemsList[i, y] is MurIncassable)
            {
                blocked = true;
            }
            else
            {
                if (mapItemsList[i, y].isBreakable)
                {
                    ((MurCassable)mapItemsList[i, y]).OnBreak();
                }
                //if (mapEnnemisList != null)
                //{
                //    mapEnnemisList[i, x].Kill();
                //}
            }
        }
        // ouest
        blocked = false;
        for (int i = x - 1; i >= x - puissance && i > 0 && !blocked; i--)
        {
            if (mapItemsList[i, y] is MurIncassable)
            {
                blocked = true;
            }
            else
            {
                if (mapItemsList[i, y].isBreakable)
                {
                    ((MurCassable)mapItemsList[i, y]).OnBreak();
                }
                //if (mapEnnemisList != null)
                //{
                //    mapEnnemisList[i, x].Kill();
                //}
            }
        }
        GameObject.Find("Player").GetComponent<PlayerMovement>().BombSet=false;
        
        Destroy(this.gameObject);
    }
}
