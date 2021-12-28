using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject map;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        GameObject mapObject = Instantiate(map, gameObject.transform.position, Quaternion.identity);
        mapObject.name = "Map";
        mapObject.transform.SetParent(transform.parent, false);
        mapObject.GetComponent<Map>().Build(); 
        GameObject playerObject = Instantiate(player, gameObject.transform.position, Quaternion.identity);
        playerObject.name = "Player";
        playerObject.transform.SetParent(transform.parent, false);
        this.gameObject.GetComponent<ScoreManager>().Reset();
        this.gameObject.GetComponent<LifeManager>().player = playerObject;
        this.gameObject.GetComponent<LifeManager>().Reset();
        Destroy(GameObject.Find("GameOver"));
    }
}
