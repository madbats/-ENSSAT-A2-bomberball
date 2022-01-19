using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Ennemis
{
    protected override  void CheckTarget()
    {
        GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
        if (Vector2.Distance(transform.position, currentTarget) < 1f)
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
        GetComponent<PathFinding>().SwitchTarget(currentTarget);
    }
}
