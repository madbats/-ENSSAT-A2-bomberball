using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : Ennemis
{
    void Update()
    {
        if (speed < (float)Time.time - (float)startTime)
        {
            startTime = Time.time;
            Debug.Log("SeekingPath");
            if (Vector2.Distance(transform.position, currentTarget) < 1f)
            {
                currentTarget = GetComponent<PathFinding>().FindFurthestPoint();
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
