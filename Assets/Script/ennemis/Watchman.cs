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
            //Debug.Log("Not Chassing");
            GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            if (GetComponent<PathFinding>().FindPlayer(vision,gameMaster.GetComponent<GameMaster>().playerObject.transform.position))
            {
                //Debug.Log("Starting Chase");
                save = currentTarget;
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position; 
                GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
                chase = true;
            }
            else if (Vector2.Distance(transform.position, currentTarget) < 1f)
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
            //Debug.Log("Chassing");
            GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            if (!GetComponent<PathFinding>().FindPlayer(vision, gameMaster.GetComponent<GameMaster>().playerObject.transform.position))
            {
                //Debug.Log("Lost Target");
                currentTarget = save;
                GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
                chase = false;
            }
            else
            {
                //Debug.Log("Continuing Chase");
                this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position;
                GetComponent<PathFinding>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            }
        }
        GetComponent<PathFinding>().SwitchTarget(currentTarget);
    }
}
