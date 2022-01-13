using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watchman : Ennemis
{
    public GameObject waypoint;
    GameObject save;
    int vision = 3;
    bool chase = false;

    // Start is called before the first frame update
    void Start()
    {
        this.scoreValue = 10;
        save = Instantiate(waypoint, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject gameMaster = GameObject.Find("GameMaster");
        // comportement : poursuit le joueur si dans son champs de vision
        if (!chase && Vector2.Distance(this.transform.position, gameMaster.GetComponent<GameMaster>().playerObject.transform.position) < vision)
        {
            save.transform.position = this.transform.position;
            this.target = gameMaster.GetComponent<GameMaster>().playerObject.transform;
            chase = true;
        }

        // comportement : abandon de poursuite
        if (chase)
        {
            if (Vector2.Distance(this.transform.position, save.transform.position) > vision || Vector2.Distance(this.transform.position, this.target.transform.position) > vision)
            {
                this.target = save.transform;
                chase = false;
            }
        }
    }
}
