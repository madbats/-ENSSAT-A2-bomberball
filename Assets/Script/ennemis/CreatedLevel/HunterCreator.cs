using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterCreator : ExplorerCreator
{
    int vision = 3;
    bool chase = false;

    protected override void CheckTarget()
    {
        GetComponent<PathFindingCreator>().CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
        if (!chase)
        {
            // comportement : poursuit le joueur si dans son champs de vision
            //Debug.Log("Not Chassing");
            if (GetComponent<PathFindingCreator>().FindPlayer(vision, gameMaster.GetComponent<GameMasterCreator>().playerObject.transform.position))
            {
                //Debug.Log("Starting Chase");
                //save = currentTarget;
                this.currentTarget = gameMaster.GetComponent<GameMasterCreator>().playerObject.transform.position;
                chase = true;
            }
            else if (Vector2.Distance(transform.position, currentTarget) < 1f)
            {
                currentTarget = GetComponent<PathFindingCreator>().FindFurthestPoint(vision);
            }
        }
        else
        {
            // comportement : abandon de poursuite
            //Debug.Log("Chassing");
            if (GetComponent<PathFindingCreator>().FindPlayer(vision, gameMaster.GetComponent<GameMasterCreator>().playerObject.transform.position))
            {
                //Debug.Log("Lost Target");
                currentTarget = GetComponent<PathFindingCreator>().FindFurthestPoint(vision);
                chase = false;
            }
            else
            {
                //Debug.Log("Continuing Chase");
                this.currentTarget = gameMaster.GetComponent<GameMasterCreator>().playerObject.transform.position;
            }
        }

        GetComponent<PathFindingCreator>().SwitchTarget(currentTarget);
    }
}
