using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class g�n�rique des ennemis
/// </summary>
public abstract class Ennemis : MonoBehaviour
{
    public List<Node> path;
    public int scoreValue;
    public float speed;
    public int life;
    //waypoints
    public Vector2 waypoint1, waypoint2;
    public Vector2 currentTarget;

    protected GameObject gameMaster;

    protected float startTime;


    // Start is called before the first frame update
    void Start()
    {
        startTime = (float)Time.time;
        //currentTarget = waypoint1;
        gameMaster = GameObject.Find("GameMaster");
        GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
    }

    void Update()
    {
        if (speed < (float)Time.time - (float)startTime)
        {
            startTime = Time.time;
            //Debug.Log("SeekingPath");
            CheckTarget();
            Move();
        }
        if (Vector2.Distance(this.transform.position, gameMaster.GetComponent<GameMaster>().playerObject.transform.position) < 1f)
        {
            gameMaster.GetComponent<LifeManager>().Death();
        }
    }

    /// <summary>
    /// Appell� lors d'une explosion pour signaler que l'ennemie est touch�
    /// </summary>
    public void Kill()
    {
        life--;
        if (life == 0)
        {
            GameObject.Find("GameMaster").GetComponent<ScoreManager>().scoreNiveau += scoreValue;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Appel� lors de chaque d�placement pour d�placer l'ennemies vers ca cible actuelle
    /// </summary>
    void Move()
    {
        GetComponent<PathFinding>().SeekPath();
        if (path.Count>0) {
            GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[(int)transform.position.x, (int)transform.position.y] = null;
            transform.position = new Vector2(path[0].x, path[0].y);
            GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[path[0].x, path[0].y] = gameObject;
            path.Remove(path[0]);
        }
        else
        {
            if (currentTarget == waypoint1)
            {
                currentTarget = waypoint2;
            }
            else
            {
                currentTarget = waypoint1;
            }
        }
    }

    /// <summary>
    /// Appel� lors de chaque d�placement pour d�terminer la cible actuelle
    /// </summary>
    abstract protected void CheckTarget();

    
}
