using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ennemis : MonoBehaviour
{
    public int scoreValue;

    public Transform[] waypoints;
    public Transform target;
    public int dest = 0;

    public Path path;
    public int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    private Seeker seeker;

    public int vitesse;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
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
    void Update()
    {
        if(path == null)
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

        float distance = Vector2.Distance(this.transform.position, path.vectorPath[currentWaypoint]);

        if(distance < 1f)
        {
            currentWaypoint++;
        }
        
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            dest = (dest + 1) % waypoints.Length;
            target = waypoints[dest];
        }
    }

    public void Kill() { 
        GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau += scoreValue;
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
