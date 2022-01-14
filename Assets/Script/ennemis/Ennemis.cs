using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ennemis : MonoBehaviour
{
    public List<Node> path;
    public int scoreValue;
    public float speed;
    //waypoints
    public Vector2 waypoint1, waypoint2;
    public Vector2 currentTarget;
    protected GameObject gameMaster;
    //public Transform[] waypoints;
    //public Transform target;
    //public int dest = 0;
    //
    ////public Path path;
    //public int currentWaypoint = 0;
    //public bool reachedEndOfPath = false;

    //public Seeker seeker;
    //public Rigidbody2D rb;

    protected float startTime;


    // Start is called before the first frame update
    void Start()
    {
        startTime = (float)Time.time;
        //currentTarget = waypoint1;
        gameMaster = GameObject.Find("GameMaster");
    }

    public void Kill()
    {
        GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau += scoreValue;
        Destroy(gameObject);
    }

    
}
