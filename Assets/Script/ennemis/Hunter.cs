using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Le Hunter est un type d'ennemi qui prend le chemin le plus long et poursuit le joueur quand il le voit
/// </summary>
public class Hunter : Ennemis
{
    /// <summary>
    /// Distance maximum de vision pour détecter le chemin le plus long et le joueur
    /// </summary>
    int vision = 3;

    /// <summary>
    /// Indique si le joueur est actuellement à la poursuite du joueur
    /// </summary>
    bool chase = false;

    protected override void CheckTarget()
    {
        GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
        if (!chase)
        {
            // comportement : poursuit le joueur si dans son champs de vision
            //Debug.Log("Not Chassing");
            if (GetComponent<PathFinding>().FindPlayer(vision, gameMaster.GetComponent<GameMaster>().playerObject.transform.position))
            {
                //Debug.Log("Starting Chase");
                //save = currentTarget;
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position;
                chase = true;
            }
            else if (Vector2.Distance(transform.position, currentTarget) < .1f)
            {
                currentTarget = GetComponent<PathFinding>().FindFurthestPoint(vision);
            }
        }
        else
        {
            // comportement : abandon de poursuite
            //Debug.Log("Chassing");
            if (GetComponent<PathFinding>().FindPlayer(vision, gameMaster.GetComponent<GameMaster>().playerObject.transform.position))
            {
                //Debug.Log("Lost Target");
                currentTarget = GetComponent<PathFinding>().FindFurthestPoint(vision);
                chase = false;
            }
            else
            {
                //Debug.Log("Continuing Chase");
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position;
            }
        }

        GetComponent<PathFinding>().SwitchTarget(currentTarget);
    }
}
