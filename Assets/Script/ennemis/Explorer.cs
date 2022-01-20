using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Le Watchman est un type d'ennemi qui suit un chemin entre deux point et poursuit le joueur quand il le voit
/// </summary>
public class Explorer : Ennemis
{
    /// <summary>
    /// Distance maximum de vision pour détecter le chemin le plus long et le joueur
    /// </summary>
    int vision = 3;
    protected override void CheckTarget()
    {
        GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
        if (Vector2.Distance(transform.position, currentTarget) < 1f)
        {
            
            currentTarget = GetComponent<PathFinding>().FindFurthestPoint(vision);
        }
        GetComponent<PathFinding>().SwitchTarget(currentTarget);
        //GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
    }
}