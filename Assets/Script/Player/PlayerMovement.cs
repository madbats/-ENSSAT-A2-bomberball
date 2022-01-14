using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //Attributs
    public float movingSpeed = 0.3f;
    public float smoothTime;
    public bool BombSet = false;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public GameObject bomb;
    private float startTime;
    private float holdTime;

    void Start()
    {
        startTime = Time.time;
        holdTime = 0.0f;
        movingSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        MapItem[,] mapItemsList = GameObject.Find("Map").GetComponent<Map>().mapItemsList;
        GameObject[,] mapEnnemisList = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList;

        bool firstUp = false;
        bool firstDown = false;
        bool firstLeft = false;
        bool firstRight = false;



        int x = (int)this.transform.position.x;
        int y = (int)this.transform.position.y;

        if (Input.GetKeyDown(KeyCode.UpArrow))//D�tection input bas
        {
            if (movingSpeed <= (float)Time.time - (float)startTime)//Pour eviter le bourrage
            {
                startTime = (float)Time.time;
                firstUp = true;
            }

        }
        if ((Input.GetKey(KeyCode.UpArrow) || firstUp) && mapItemsList[x, y + 1] is Sol && !(mapEnnemisList[x, y + 1] !=null))
        { //Maintien de la touche
            holdTime = (float)Time.time - (float)startTime;
            if (movingSpeed <= holdTime || firstUp)//On augmente si le temps de maintien est sup�rieur � la vitesse (=temps entre 2 d�placements)
            {
                MovePlayer(x, y + 1);
                startTime = Time.time;
                firstUp = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (movingSpeed <= (float)Time.time - (float)startTime)
            {
                startTime = (float)Time.time;
                firstDown = true;
            }
        }

        if ((Input.GetKey(KeyCode.DownArrow) || firstDown) && mapItemsList[x, y - 1] is Sol && !(mapEnnemisList[x, y - 1] != null))
        {
            holdTime = (float)Time.time - (float)startTime;
            if (movingSpeed <= holdTime || firstDown)
            {
                MovePlayer(x, y - 1);
                startTime = Time.time;
                firstDown = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (movingSpeed <= (float)Time.time - (float)startTime)
            {
                startTime = (float)Time.time;
                firstLeft = true;
            }
        }

        if ((Input.GetKey(KeyCode.LeftArrow) || firstLeft) && mapItemsList[x - 1, y] is Sol && !(mapEnnemisList[x - 1, y] != null))
        {

            holdTime = (float)Time.time - (float)startTime;
            if (movingSpeed <= holdTime || firstLeft)
            {
                MovePlayer(x - 1, y);
                startTime = Time.time;
                firstLeft = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (movingSpeed <= (float)Time.time - (float)startTime)
            {
                startTime = (float)Time.time;
                firstRight = true;
            }
        }

        if ((Input.GetKey(KeyCode.RightArrow) || firstRight) && mapItemsList[x + 1, y] is Sol && !(mapEnnemisList[x + 1, y] != null))
        {

            holdTime = (float)Time.time - (float)startTime;
            if (movingSpeed <= holdTime || firstRight)
            {
                MovePlayer(x + 1, y);
                startTime = Time.time;
                firstRight = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.B) && !BombSet)
        {
            BombSet = true;
            GameObject newBomb = Instantiate(bomb, new Vector3(x, y, -10), Quaternion.identity);
            newBomb.transform.SetParent(this.transform.parent, false);

            GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y] = newBomb;
            
        }

        if (GameObject.Find("Player").GetComponent<PlayerBonus>().poussee)
        {
            if (mapEnnemisList[x + 1, y] != null)
            {
                if ((Input.GetKeyDown(KeyCode.RightArrow)) && mapEnnemisList[x + 1, y].GetComponent<Bomb>()) //Bombe sur le chemin
                {
                    if (mapItemsList[x + 2, y] is Sol) //Sol derri�re la bombe
                    {
                        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x + 2, y] = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x + 1, y];
                        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x + 1, y] = null;
                        MoveBomb(x + 2, y);
                    }
                }
            }
            if (mapEnnemisList[x - 1, y] != null)
            {
                if ((Input.GetKeyDown(KeyCode.LeftArrow)) && mapEnnemisList[x - 1, y].GetComponent<Bomb>()) //Bombe sur le chemin
                {
                    if (mapItemsList[x - 2, y] is Sol) //Sol derri�re la bombe
                    {
                        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x - 2, y] = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x - 1, y];
                        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x - 1, y] = null;
                        MoveBomb(x - 2, y);
                    }
                }
            }

            if (mapEnnemisList[x, y + 1] != null)
            {
                if ((Input.GetKeyDown(KeyCode.UpArrow)) && mapEnnemisList[x, y + 1].GetComponent<Bomb>()) //Bombe sur le chemin
                {
                    if (mapItemsList[x, y + 2] is Sol) //Sol derri�re la bombe
                    {
                        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y + 2] = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y + 1];
                        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y + 1] = null;
                        MoveBomb(x, y + 2);
                    }
                }
            }

            if (mapEnnemisList[x, y - 1] != null)
            {
                if ((Input.GetKeyDown(KeyCode.DownArrow)) && mapEnnemisList[x, y - 1].GetComponent<Bomb>()) //Bombe sur le chemin
                {
                    if (mapItemsList[x, y - 2] is Sol) //Sol derri�re la bombe
                    {
                        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y - 2] = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y - 1];
                        GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y - 1] = null;
                        MoveBomb(x, y - 2);
                    }
                }
            }
        }

    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        transform.position = new Vector3(_horizontalMovement, _verticalMovement, 0);
    }

    void MoveBomb(float _horizontalMovement, float _verticalMovement)
    {
        Bomb newbomb = GameObject.Find("Bomb(Clone)").GetComponent<Bomb>();
        newbomb.transform.position = new Vector3(_horizontalMovement, _verticalMovement, -3);

    }
}
