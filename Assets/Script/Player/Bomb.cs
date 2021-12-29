using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Ennemis
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
        /*if (x >= 0 && x < 5 && y >= 0 && y < 5) {
            if(mapItemsList[x,y].isBreakable)
                ((MurCassable)mapItemsList[x,y]).OnBreak();
        }*/
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        puissance = GameObject.Find("Player").GetComponent<PlayerBonus>().puissance;
        Debug.Log("Starting destroy");
        for (int i = x - puissance; i <= x+puissance && i<13; i++) {
            if (i >= 0 ) {
                if(mapItemsList[i,y].isBreakable)
                    ((MurCassable)mapItemsList[i,y]).OnBreak();
            }
        }
        
        Debug.Log("X finished");
        for (int i = y - puissance; i <= y+puissance && i<11; i++) {
            if (i >= 0 ) {
                if(mapItemsList[x,i].isBreakable)
                    ((MurCassable)mapItemsList[x,i]).OnBreak();
            }
        }
        Debug.Log("End destroy");
        GameObject.Find("Player").GetComponent<PlayerMovement>().BombSet=false;

        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y] = null;

        Debug.Log("destroy Me");
        Destroy(this.gameObject);
    }
    
}
