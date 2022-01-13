using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ennemis : MonoBehaviour
{

    public int scoreValue;
    public int speed = 1;

    public Transform[] waypoints;
    public Transform target;
    public int dest = 0;

    public Path path;
    public int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    public Seeker seeker;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void InitPath()
    {
        target = waypoints[0];
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
             seeker.StartPath(this.transform.position, target.position, OnPathComplete);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject[,] mapEnnemisList = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList;

        if (path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        int x = (int)this.transform.position.x;
        int y = (int)this.transform.position.y;
        mapEnnemisList[x, y] = null;
        if (this.transform.position.x > x + .5f)
        {
            x++;
        }
        if(this.transform.position.y > y + .5f)
        {
            y++;
        }
        mapEnnemisList[x, y] = this.gameObject;

        float distance = Vector2.Distance(this.transform.position, path.vectorPath[currentWaypoint]);
        if (distance < 1f)
        {
            currentWaypoint++;
        }
        
        if(Vector2.Distance(transform.position, target.position) < 0.3f)
        {
            dest = (dest + 1) % waypoints.Length;
            target = waypoints[dest];
        }

        if(Vector2.Distance(GameObject.Find("Player").transform.position, this.transform.position) < .5f)
        {
            GameObject.Find("GameMaster").GetComponent<LifeManager>().Death();
        }
    }

    public void Kill() { 
        GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau += scoreValue;
        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[(int)this.transform.position.x, (int)this.transform.position.y] = null;
        Destroy(gameObject);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
