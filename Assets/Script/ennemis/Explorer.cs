using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : Ennemis
{
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