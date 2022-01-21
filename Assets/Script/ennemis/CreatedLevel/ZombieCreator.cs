using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCreator : EnnemisCreator
{
    protected override void CheckTarget()
    {
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
        GetComponent<PathFindingCreator>().SwitchTarget(currentTarget);
    }
}
