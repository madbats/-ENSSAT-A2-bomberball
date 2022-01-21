using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script de control de la bombe
/// </summary>
public class BombCreator : MonoBehaviour
{
    public float timeLeft;
    public int x;
    public int y;
    public int puissance;
    public Sprite FirstStage;
    public Sprite SecondStage;
    public Sprite ThirdStage;


    public GameObject ExplosionC;
    public GameObject ExplosionH;
    public GameObject ExplosionB;
    public GameObject ExplosionG;
    public GameObject ExplosionD;

    /*public AudioSource explosion;
    public float volume = 0.5f;*/

    public AudioSource explosion;

    public bool explosionlancee = false;



    // Start is called before the first frame update
    void Start()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        explosion = GameObject.Find("Explosion").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;


        if (timeLeft > 0.5 && timeLeft < 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = SecondStage;
        }
        else
        if (timeLeft <= 0.5 && timeLeft > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ThirdStage;
        }
        else
        if (timeLeft <= 0)
        {
            if (!explosionlancee)
            {
                explosionlancee = true;
                explosion.Play();
                Explosion();
            }
        }
        /*
        switch (timeLeft) {
        	case float i when i > 5 && i <= timeLeft*0.75:
        		//this.gameObject.GetComponent<SpriteRenderer>().sprite= SecondStage;
        		break;
        	case float i when i > 0 && i <= timeLeft*0.40:
        		//this.gameObject.GetComponent<SpriteRenderer>().sprite= ThirdStage;
        		break;
        	case float i when i <= 0:
                
                
        		break;
        	default:
        		break;
        }*/
    }

    /// <summary>
    /// Explosion de la bombe, l'ensemble des éléments sont détruit dans le rayon de la bombe
    /// </summary>
    void Explosion()
    {

        //Explosion
        GameObject newExplosionC = Instantiate(ExplosionC, new Vector3(x, y, -10), Quaternion.identity);
        newExplosionC.transform.SetParent(this.transform.parent, false);

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
        puissance = GameObject.Find("Player").GetComponent<PlayerBonusCreator>().puissance;
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
        for (int i = y + 1; i <= y + puissance && i < 13 && !blocked; i++)
        {
            if (mapItemsList[x, i] is MurIncassable)
            {
                blocked = true;
            }
            else
            {
                GameObject newExplosionH = Instantiate(ExplosionH, new Vector3(x, i, -10), Quaternion.identity);
                newExplosionH.transform.SetParent(this.transform.parent, false);
                if (mapItemsList[x, i].isBreakable)
                {
                    ((MurCassable)mapItemsList[x, i]).OnBreak();
                }
                foreach (GameObject ennemi in ennemis)
                {
                    if (Vector2.Distance(new Vector2(x, i), ennemi.transform.position) < .7f)
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
        // sud
        blocked = false;
        for (int i = y - 1; i >= y - puissance && i > 0 && !blocked; i--)
        {
            if (mapItemsList[x, i] is MurIncassable)
            {
                blocked = true;
            }
            else
            {
                GameObject newExplosionB = Instantiate(ExplosionB, new Vector3(x, i, -10), Quaternion.identity);
                newExplosionB.transform.SetParent(this.transform.parent, false);
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
                GameObject newExplosionD = Instantiate(ExplosionD, new Vector3(i, y, -10), Quaternion.identity);
                newExplosionD.transform.SetParent(this.transform.parent, false);
                if (mapItemsList[i, y].isBreakable)
                {
                    ((MurCassable)mapItemsList[i, y]).OnBreak();
                }
                foreach (GameObject ennemi in ennemis)
                {
                    if (Vector2.Distance(new Vector2(i, y), ennemi.transform.position) < .6f)
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
                GameObject newExplosionG = Instantiate(ExplosionG, new Vector3(i, y, -10), Quaternion.identity);
                newExplosionG.transform.SetParent(this.transform.parent, false);
                if (mapItemsList[i, y].isBreakable)
                {
                    ((MurCassable)mapItemsList[i, y]).OnBreak();
                }
                foreach (GameObject ennemi in ennemis)
                {
                    if (Vector2.Distance(new Vector2(i, y), ennemi.transform.position) < .6f)
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

        GameObject.Find("Player").GetComponent<PlayerMovementCreator>().BombSet = false;
        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y] = null;

        Destroy(this.gameObject);
        if (killPlayer)
        {
            GameObject.Find("GameMaster").GetComponent<LifeManagerCreator>().Death();
        }
        explosionlancee = false;
    }

}
