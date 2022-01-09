using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Explorer : MonoBehaviour
{

    public int scoreValue;
    public int speed = 3;

    public Transform[] waypoints;
    public Transform target;
    public int dest = 0;

    public Path path;
    public int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    public bool immobile = false;

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
    void Update()
    {
        GameObject[,] mapEnnemisList = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList;

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
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
        if (this.transform.position.x > x + .5f)
        {
            x++;
        }
        int y = (int)this.transform.position.y;
        if (this.transform.position.y > y + .5f)
        {
            y++;
        }
        mapEnnemisList[x, y] = this.gameObject;

        float distance = Vector2.Distance(this.transform.position, path.vectorPath[currentWaypoint]);
        if (distance < 1f)
        {
            currentWaypoint++;
        }

        if (Vector2.Distance(transform.position, target.position) < 0.3f)
        {
            dest = (dest + 1) % waypoints.Length;
            //comportement exporateur
            Map map = GameObject.Find("Map").GetComponent<Map>();
            float dist = 0;
            Vector2 destMax = new Vector2(0,0);
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if(map.mapItemsList[i, j] is Sol)
                    {
                        if(Vector2.Distance(this.transform.position, map.mapItemsList[i, j].transform.position) > dist)
                        {
                            dist = Vector2.Distance(this.transform.position, map.mapItemsList[i, j].transform.position);
                            destMax = new Vector2(i, j);
                        }
                    }
                }
            }
            waypoints[dest].position = destMax;
            target = waypoints[dest];
        }
        if (Vector2.Distance(GameObject.Find("Player").transform.position, this.transform.position) < .5f)
        {
            GameObject.Find("GameMaster").GetComponent<LifeManager>().Death();
        }
    }

    public void Kill()
    {
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

    //renvoi le nombre de case à parcourir pour aller de la case [x, y] à la case [i,j]
    //renvoi -1 si la case [i, j] n'est pas accessible depuis la case [x, y]
    public int Accessible(Map map, int x, int y, int i, int j)
    {
        int[] dir = { -1, -1, -1, -1 }; //up, down, right, left
        if (x == i && y == j)
        {
            return 0;
        }
        if (map.mapItemsList[x, y + 1] is Sol)
        {
            dir[0] = 1 + Accessible(map, x, y + 1, i, j);
        }
        if (map.mapItemsList[x, y - 1]  is Sol)
        {
            dir[1] = 1 + Accessible(map, x, y - 1, i, j);
        }
        if (map.mapItemsList[x + 1, y] is Sol)
        {
            dir[2] = 1 + Accessible(map, x + 1, y, i, j);
        }
        if (map.mapItemsList[x - 1, y] is Sol)
        {
            dir[3] = 1 + Accessible(map, x - 1, y, i, j);
        }
        int min = dir[0];
        for (int k = 1; k < 4; k++)
        {
            if (min == -1)
            {
                min = dir[k];
            }
            else if (dir[k] != -1 && dir[k] < min)
            {
                min = dir[k];
            }
        }
        return min;
    }
}