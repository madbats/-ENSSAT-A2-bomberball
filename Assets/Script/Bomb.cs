using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public float timeLeft = 15;
    public int x;
    public int y;
    public int puissance = 1;

    // Start is called before the first frame update
    void Start()
    {
        x = (int) transform.position.x;
        y = (int) transform.position.y;
     	gameObject.GetComponent<Renderer>().material.color= Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        switch (timeLeft) {
        	case float i when i > 5 && i <= 10:
        		gameObject.GetComponent<Renderer>().material.color= Color.yellow;
        		break;
        	case float i when i > 0 && i <= 5:
        		gameObject.GetComponent<Renderer>().material.color= Color.red;
        		break;
        	case float i when i <= 0:
        		Destroy(gameObject);
        		break;
        	default:
        		break;
        }
    }

    void Explosion() {
        GameObject[,] m = GameObject.Find("map").GetComponent<Map>().mapItems;
        if (x >= 0 && x < 5 && y >= 0 && y < 5) { 
            Destroy(m[x,y]);
        }
        
        for (int i = x - puissance; i < x; i++) {
            if (i >= 0) {
                Debug.Log("destruction ("+i+","+y+")");
                Destroy(m[i,y]);
            }
        }
        for (int i = x + 1; i <= x + puissance && i < 5; i++){
            Debug.Log("destruction ("+i+","+y+")");
            Destroy(m[i,y]);
        }
        for (int i = y - puissance; i < y; i++) {
            if (i >= 0) {
                Debug.Log("destruction ("+x+","+i+")");
                Destroy(m[x,i]);
            }
        }
        for (int i = y + 1; i <= y + puissance && i < 5; i++){
            Debug.Log("destruction ("+x+","+i+")");
            Destroy(m[x,i]);
        }
    }

    void OnDestroy() {
        Explosion();
    }
}
