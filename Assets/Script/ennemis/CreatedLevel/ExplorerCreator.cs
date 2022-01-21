using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ExplorerCreator : EnnemisCreator
{
    int vision = 3;
    protected override void CheckTarget()
    {
        if (Vector2.Distance(transform.position, currentTarget) < 1f)
        {
            GetComponent<PathFindingCreator>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            currentTarget = GetComponent<PathFindingCreator>().FindFurthestPoint(vision);
        }
        GetComponent<PathFindingCreator>().SwitchTarget(currentTarget);
        //GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
    }
}