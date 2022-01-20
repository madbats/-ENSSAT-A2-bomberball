using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Le Watchman est un type d'ennemi suit un chemin entre deux point et poursuit le joueur quand il le voit
/// </summary>
public class Watchman : Ennemis
{
    /// <summary>
    /// Dernier point ciblé avant le poursuite
    /// </summary>
    Vector2 save;
    /// <summary>
    /// Distance maximum de vision pour détecter le chemin le plus long et le joueur
    /// </summary>
    int vision = 3;
    /// <summary>
    /// Indique si le joueur est actuellement é la poursuite du joueur
    /// </summary>
    bool chase = false;

    protected override void CheckTarget()
    {
        GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
        if (!chase)
        {
            //Debug.Log("Not Chassing");
            if (GetComponent<PathFinding>().FindPlayer(vision,gameMaster.GetComponent<GameMaster>().playerObject.transform.position))
            {
                Debug.Log("Starting Chase");
                save = currentTarget;
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position; 
                //GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
                chase = true;
            }
            else if (Vector2.Distance(transform.position, currentTarget) < .1f)
            {
                //Debug.Log("On Target");
                if (currentTarget == waypoint1)
                {
                    currentTarget = waypoint2;
                }
                else if (currentTarget == waypoint2)
                {
                    currentTarget = waypoint1;
                }
            }
        }
        else
        {
            // comportement : abandon de poursuite
            Debug.Log("Chassing");
            //GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            if (!GetComponent<PathFinding>().FindPlayer(vision, gameMaster.GetComponent<GameMaster>().playerObject.transform.position))
            {
                Debug.Log("Lost Target");
                currentTarget = save;
                //GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
                chase = false;
            }
            else
            {
                //Debug.Log("Continuing Chase");
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position;
                //GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            }
        }
        GetComponent<PathFinding>().SwitchTarget(currentTarget);
    }
}
