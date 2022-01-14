using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Ennemis
{
    //dernière position avant chase
    //Vector2 save;
    int vision = 3;
    bool chase = false;

    void Update()
    {
        if (speed < (float)Time.time - (float)startTime)
        {
            startTime = Time.time;
            Debug.Log("SeekingPath");
            
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
                if (Vector2.Distance(transform.position, currentTarget) < 1f)
                {
                    currentTarget = GetComponent<PathFinding>().FindFurthestPoint();
                }
            }
            else
            {
                // comportement : abandon de poursuite
                Debug.Log("Chassing");
                if (Vector2.Distance(transform.position, currentTarget) > vision)
                {
                    Debug.Log("Lost Target");
                    currentTarget = GetComponent<PathFinding>().FindFurthestPoint();
                    chase = false;
                }
                else
                {
                    Debug.Log("Continuing Chase");
                    this.currentTarget = gameMaster.GetComponent<GameMaster>().playerObject.transform.position;
                }
            }

            GetComponent<PathFinding>().SwitchTarget(currentTarget);
            GetComponent<PathFinding>().SeekPath();

            foreach (Node n in path)
            {
                Debug.Log("" + path[0].x + " ; " + path[0].y);
            }
            GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[(int)transform.position.x, (int)transform.position.y] = null;
            transform.position = new Vector2(path[0].x, path[0].y);
            GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[path[0].x, path[0].y] = gameObject;
            path.Remove(path[0]);
        }
        if (Vector2.Distance(this.transform.position, gameMaster.GetComponent<GameMaster>().playerObject.transform.position) < 1f)
        {
            gameMaster.GetComponent<LifeManager>().Death();
        }
    }
}
