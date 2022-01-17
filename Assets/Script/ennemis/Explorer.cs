using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Explorer : MonoBehaviour
{
    int vision = 3;
    protected override void CheckTarget()
    {
        if (Vector2.Distance(transform.position, currentTarget) < 1f)
        {
            GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            currentTarget = GetComponent<PathFinding>().FindFurthestPoint(vision);
        }
        GetComponent<PathFinding>().SwitchTarget(currentTarget);
        //GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
    }
}