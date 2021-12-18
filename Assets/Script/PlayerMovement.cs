using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Attributs
    public float movingSpeed;
    public float smoothTime;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public Bomb bomb;

    // Update is called once per frame
    void FixedUpdate()
    {
        /* float horizontalMovement = Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement, verticalMovement); */

        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if (Input.GetKeyDown(KeyCode.UpArrow) && y+1 < 5) {
        	MovePlayer(x,y+1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && y-1 > -5) {
        	MovePlayer(x,y-1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && x-1 > -6) {
        	MovePlayer(x-1,y);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && x+1 < 6) {
        	MovePlayer(x+1,y);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Bomb newBomb = Instantiate(bomb, new Vector3(x, y, 0), Quaternion.identity);
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
