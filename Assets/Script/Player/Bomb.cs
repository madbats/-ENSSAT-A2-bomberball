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
        this.gameObject.GetComponent<SpriteRenderer>().sprite = FirstStage;


        switch (timeLeft) {
        	case float i when i > 5 && i <= timeLeft*0.75:
        		this.gameObject.GetComponent<SpriteRenderer>().sprite= SecondStage;
        		break;
        	case float i when i > 0 && i <= timeLeft*0.40:
        		this.gameObject.GetComponent<SpriteRenderer>().sprite= ThirdStage;
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
        Transform player = GameObject.Find("Player").GetComponent<Transform>();
        GameObject[,] mapEnnemisList = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList;
        List<GameObject> ennemis = new List<GameObject>();
        List<GameObject> ennemisKilled = new List<GameObject>();
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (mapEnnemisList[i, j] != null)
                {
                    if (mapEnnemisList[i, j].GetComponent<Ennemis>())
                    {
                        ennemis.Add(mapEnnemisList[i, j]);
                    }
                }
            } 
        }
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        puissance = GameObject.Find("Player").GetComponent<PlayerBonus>().puissance;
        /**
         * Parcourt de chaque direction (nord,sud,est,ouest) et destruction des objects/ennemis
         */
        bool blocked;
        bool killPlayer = false;

        if (player.position.x == x && player.position.y == y)
        {
            killPlayer = true;
        }
        foreach (GameObject ennemi in ennemis)
        {
            if (Vector2.Distance(transform.position, ennemi.transform.position) < .6f)
            {
                ennemi.GetComponent<Ennemis>().Kill();
                ennemisKilled.Add(ennemi);
            }
        }
        foreach (GameObject ennemi in ennemisKilled)
        {
            ennemis.Remove(ennemi);
        }
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
                foreach(GameObject ennemi in ennemis)
                {
                    if(Vector2.Distance(new Vector2(x, i), ennemi.transform.position) < .6f)
                    {
                        ennemi.GetComponent<Ennemis>().Kill();
                        ennemisKilled.Add(ennemi);
                    }
                }
                foreach(GameObject ennemi in ennemisKilled)
                {
                    ennemis.Remove(ennemi);
                }
                ennemisKilled.Clear();
                if (player.position.x == x && player.position.y == i)
                {
                    killPlayer = true;
                }
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
                foreach (GameObject ennemi in ennemis)
                {
                    if (Vector2.Distance(new Vector2(x, i), ennemi.transform.position) < .6f)
                    {
                        ennemi.GetComponent<Ennemis>().Kill();
                        ennemisKilled.Add(ennemi);
                    }
                }
                foreach (GameObject ennemi in ennemisKilled)
                {
                    ennemis.Remove(ennemi);
                }
                ennemisKilled.Clear();
                if (player.position.x == x && player.position.y == i)
                {
                    killPlayer = true;
                }
            }
        }
        // est
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
                foreach (GameObject ennemi in ennemis)
                {
                    if (Vector2.Distance(new Vector2(i, y), ennemi.transform.position)<.6f)
                    {
                        ennemi.GetComponent<Ennemis>().Kill();
                        ennemisKilled.Add(ennemi);
                    }
                }
                foreach (GameObject ennemi in ennemisKilled)
                {
                    ennemis.Remove(ennemi);
                }
                ennemisKilled.Clear();
                if (player.position.x == i && player.position.y == y)
                {
                    killPlayer = true;
                }
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
                foreach (GameObject ennemi in ennemis)
                {
                    if (Vector2.Distance(new Vector2(i, y), ennemi.transform.position)<.6f)
                    {
                        ennemi.GetComponent<Ennemis>().Kill();
                        ennemisKilled.Add(ennemi);
                    }
                }
                foreach (GameObject ennemi in ennemisKilled)
                {
                    ennemis.Remove(ennemi);
                }
                ennemisKilled.Clear();
                if (player.position.x == i && player.position.y == y)
                {
                    killPlayer = true;
                }
            }
        }

        GameObject.Find("Player").GetComponent<PlayerMovement>().BombSet=false;
        GameObject.Find("GameMaster").GetComponent<AstarPath>().Scan();
        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y] = null;

        Destroy(this.gameObject);
        if (killPlayer)
        {
            GameObject.Find("GameMaster").GetComponent<LifeManager>().Death();
        }
    }
    
}
