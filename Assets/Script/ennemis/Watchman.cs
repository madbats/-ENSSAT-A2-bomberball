using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watchman : Ennemis
{
    //dernière position avant chase
    Vector2 save;
    int vision = 3;
    bool chase = false;


    protected override void CheckTarget()
    {
        if (!chase)
        {
            Debug.Log("Not Chassing");
            if (Vector2.Distance(this.transform.position, gameMaster.GetComponent<GameMaster>().playerObject.transform.position) < vision)
            {
                Debug.Log("Starting Chase");
                save = currentTarget;
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position;
                chase = true;
            }
            else if (Vector2.Distance(transform.position, currentTarget) < 1f)
            {
                Debug.Log("On Target");
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
            if (Vector2.Distance(transform.position, currentTarget) > vision)
            {
                Debug.Log("Lost Target");
                currentTarget = save;
                chase = false;
            }
            else
            {
                Debug.Log("Continuing Chase");
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position;
            }
        }
        GetComponent<PathFinding>().SwitchTarget(currentTarget);
    }
}
