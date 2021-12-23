using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Attributs
    public float movingSpeed;
    public float smoothTime;
    public bool BombSet=false;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public Bomb bomb;

    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        /* float horizontalMovement = Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement, verticalMovement); */
        MapItem[,] mapItemsList = GameObject.Find("Map").GetComponent<Map>().mapItemsList;


        int x = (int)this.transform.position.x;
        int y = (int)this.transform.position.y;
        if (Input.GetKeyDown(KeyCode.UpArrow) && mapItemsList[x,y+1] is Sol) {
            Debug.Log("Up");
        	MovePlayer(x,y+1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && mapItemsList[x,y-1] is Sol) {
            Debug.Log("Down");
        	MovePlayer(x,y-1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && mapItemsList[x-1,y] is Sol) {
            Debug.Log("Left");
        	MovePlayer(x-1,y);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && mapItemsList[x+1,y] is Sol) {
            Debug.Log("Right");
        	MovePlayer(x+1,y);
        }

        if (Input.GetKeyDown(KeyCode.B) && !BombSet)
        {
            Debug.Log("Bomb");
            BombSet=true;
            Bomb newBomb = Instantiate(bomb, new Vector3(x, y, -10), Quaternion.identity);
            newBomb.transform.SetParent(this.transform.parent, false);
        }

       


    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        /*Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);*/
        transform.position = new Vector3(_horizontalMovement, _verticalMovement, 0);
    }
}
