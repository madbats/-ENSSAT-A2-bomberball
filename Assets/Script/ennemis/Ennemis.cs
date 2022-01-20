using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class générique des ennemis
/// </summary>
public abstract class Ennemis : MonoBehaviour
{
    /// <summary>
    /// Liste des points à prendre pour atteindre la cible
    /// </summary>
    public List<Node> path;

    /// <summary>
    /// Valeur en score de l'ennemi
    /// </summary>
    public int scoreValue;
    /// <summary>
    /// vitesse de déplacement de l'ennemi
    /// </summary>
    public float speed;
    /// <summary>
    /// Nombre de mort que peut subir l'ennemi avant de mourir
    /// </summary>
    public int life;
    /// <summary>
    /// Les points entres lesquel voyage l'ennemi
    /// </summary>
    public Vector2 waypoint1, waypoint2;
    /// <summary>
    /// Cible actuelle de l'ennemi
    /// </summary>
    public Vector2 currentTarget;

    protected GameObject gameMaster;
    /// <summary>
    /// Date du dernier déplacement
    /// </summary>
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
    /// Appellé lors d'une explosion pour signaler que l'ennemie est touché
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
    /// Appelé lors de chaque déplacement pour déplacer l'ennemies vers ca cible actuelle
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
    /// Appelé lors de chaque déplacement pour déterminer la cible actuelle
    /// </summary>
    abstract protected void CheckTarget();

    
}
