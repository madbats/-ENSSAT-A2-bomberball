using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Ennemis
{
    int vision = 3;
    bool chase = false;

    protected override void CheckTarget()
    {
        if (!chase)
        {
            // comportement : poursuit le joueur si dans son champs de vision
            Debug.Log("Not Chassing");
            if (Vector2.Distance(this.transform.position, gameMaster.GetComponent<GameMaster>().playerObject.transform.position) < vision)
            {
                Debug.Log("Starting Chase");
                //save = currentTarget;
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position;
                chase = true;
            }
            else if (Vector2.Distance(transform.position, currentTarget) < 1f)
            { 
                currentTarget = GetComponent<PathFinding>().FindFurthestPoint(vision);
            }
        }
        else
        {
            // comportement : abandon de poursuite
            Debug.Log("Chassing");
            if (Vector2.Distance(transform.position, currentTarget) > vision)
            {
                Debug.Log("Lost Target");
                currentTarget = GetComponent<PathFinding>().FindFurthestPoint(vision);
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
