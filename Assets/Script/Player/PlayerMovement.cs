using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool BombSet=false;

    public Bomb bomb;

    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        MapItem[,] mapItemsList = GameObject.Find("Map").GetComponent<Map>().mapItemsList;
        int x = (int)this.transform.position.x;
        int y = (int)this.transform.position.y;
        if (Input.GetKeyDown(KeyCode.UpArrow) && mapItemsList[x,y+1] is Sol) {
            Debug.Log("deplacement joueur haut");
        	MovePlayer(x,y+1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && mapItemsList[x,y-1] is Sol) {
            Debug.Log("deplacement joueur bas");
            MovePlayer(x,y-1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && mapItemsList[x-1,y] is Sol) {
            Debug.Log("deplacement joueur gauche");
            MovePlayer(x-1,y);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && mapItemsList[x+1,y] is Sol) {
            Debug.Log("deplacement joueur droit");
            MovePlayer(x+1,y);
        }

        if (Input.GetKeyDown(KeyCode.B) && !BombSet)
        {
            BombSet=true;
            Bomb newBomb = Instantiate(bomb, new Vector3(x, y, -10), Quaternion.identity);
            newBomb.transform.SetParent(this.transform.parent, false);
        }
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        transform.position = new Vector3(_horizontalMovement, _verticalMovement, 0);
        Debug.Log("position actuel :" + _horizontalMovement + ", " + _verticalMovement);
    }
}
