using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchmanCreator : EnnemisCreator
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
            GetComponent<PathFindingCreator>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            if (GetComponent<PathFindingCreator>().FindPlayer(vision, gameMaster.GetComponent<GameMasterCreator>().playerObject.transform.position))
            {
                //Debug.Log("Starting Chase");
                save = currentTarget;
                this.currentTarget = gameMaster.GetComponent<GameMasterCreator>().playerObject.transform.position;
                GetComponent<PathFindingCreator>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
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
            GetComponent<PathFindingCreator>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            if (!GetComponent<PathFindingCreator>().FindPlayer(vision, gameMaster.GetComponent<GameMasterCreator>().playerObject.transform.position))
            {
                //Debug.Log("Lost Target");
                currentTarget = save;
                GetComponent<PathFindingCreator>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
                chase = false;
            }
            else
            {
                //Debug.Log("Continuing Chase");
                this.currentTarget = gameMaster.GetComponent<GameMasterCreator>().playerObject.transform.position;
                GetComponent<PathFindingCreator>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
            }
        }
        GetComponent<PathFindingCreator>().SwitchTarget(currentTarget);
    }
}
