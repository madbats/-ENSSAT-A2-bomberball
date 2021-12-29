using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //Attributs
    public float movingSpeed= 0.3f;
    public float smoothTime;
    public bool BombSet=false;
    public bool poussee=true;
    public int puissance = 1;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public Bomb bomb;
    private float startTime;
    private float holdTime;

    //public bool poussee = false;

    void Start(){
        startTime = Time.time;
        holdTime = 0.0f;
        movingSpeed = 0.5f;
        poussee = true;
        puissance = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /* float horizontalMovement = Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement, verticalMovement); */
        MapItem[,] mapItemsList = GameObject.Find("Map").GetComponent<Map>().mapItemsList;
        Ennemis[,] mapEnnemisList = GameObject.Find("Map").GetComponent<Map>().mapEnnemisList;

        bool firstUp = false;
        bool firstDown = false;
        bool firstLeft = false;
        bool firstRight = false;



        int x = (int)this.transform.position.x;
        int y = (int)this.transform.position.y;

        if (Input.GetKeyDown(KeyCode.UpArrow))//D�tection input bas
        {
            Debug.Log("Up");
            if (movingSpeed <= (float)Time.time - (float)startTime)//Pour eviter le bourrage
            {
                startTime = (float)Time.time;
                firstUp = true;
            }
            
        }

        if ((Input.GetKey(KeyCode.UpArrow) || firstUp) && mapItemsList[x,y+1] is Sol && !(mapEnnemisList[x, y+1] is Bomb)) { //Maintien de la touche
            
            holdTime = (float)Time.time - (float)startTime;
            if ( movingSpeed <= holdTime || firstUp)//On augmente si le temps de maintien est sup�rieur � la vitesse (=temps entre 2 d�placements)
            {
                MovePlayer(x, y + 1);
                startTime = Time.time;
                firstUp = false;
            }
        	
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Down");
            if (movingSpeed <= (float)Time.time - (float)startTime)
            {
                startTime = (float)Time.time;
                firstDown = true;
            }
        }

        if ((Input.GetKey(KeyCode.DownArrow) || firstDown) && mapItemsList[x, y - 1] is Sol && !(mapEnnemisList[x, y - 1] is Bomb))
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
            Debug.Log("Left");
            if (movingSpeed <= (float)Time.time - (float)startTime)
            {
                startTime = (float)Time.time;
                firstLeft = true;
            }
        }

        if ((Input.GetKey(KeyCode.LeftArrow) || firstLeft) && mapItemsList[x-1,y] is Sol && !(mapEnnemisList[x - 1, y] is Bomb))
        {

            holdTime = (float)Time.time - (float)startTime;
            if (movingSpeed <= holdTime || firstLeft)
            {
                MovePlayer(x-1, y);
                startTime = Time.time;
                firstLeft = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right");
            if (movingSpeed <= (float)Time.time - (float)startTime)
            {
                startTime = (float)Time.time;
                firstRight = true;
            }
        }
        Debug.Log(mapItemsList.Length);
        if ((Input.GetKey(KeyCode.RightArrow) || firstRight) && mapItemsList[x + 1, y] is Sol && !(mapEnnemisList[x + 1, y] is Bomb))
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
            BombSet=true;
            Bomb newBomb = Instantiate(bomb, new Vector3(x, y, -10), Quaternion.identity);
            newBomb.transform.SetParent(this.transform.parent, false);

            GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y] = newBomb;
            Debug.Log(GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y]);
        }
        
        if (poussee)
        {

            if ((Input.GetKeyDown(KeyCode.RightArrow)) && mapEnnemisList[x + 1, y] is Bomb) //Bombe sur le chemin
            {
                Debug.Log("bombe dessus");
                Debug.Log(mapItemsList[x + 2, y] is Sol);
                if(mapItemsList[x + 2, y] is Sol) //Sol derri�re la bombe
                {
                    Debug.Log("bombe boug�e");
                    GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x + 1, y] = null;
                    MoveBomb(x + 2, y);
                    GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x + 2, y] = new Bomb();
                }
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow)) && mapEnnemisList[x - 1, y] is Bomb) //Bombe sur le chemin
            {
                Debug.Log("bombe dessus");
                if (mapItemsList[x - 2, y] is Sol) //Sol derri�re la bombe
                {
                    Debug.Log("bombe boug�e");
                    GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x - 1, y] = null;
                    MoveBomb(x - 2, y);
                    GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x - 2, y] = new Bomb();
                }
            }

            if ((Input.GetKeyDown(KeyCode.UpArrow)) && mapEnnemisList[x, y+1] is Bomb) //Bombe sur le chemin
            {
                Debug.Log("bombe dessus");
                if (mapItemsList[x, y+2] is Sol) //Sol derri�re la bombe
                {
                    Debug.Log("bombe boug�e");
                    GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y + 1] = null;
                    MoveBomb(x, y+2);
                    GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y + 2] = new Bomb();
                }
            }

            if ((Input.GetKeyDown(KeyCode.DownArrow)) && mapEnnemisList[x, y-1] is Bomb) //Bombe sur le chemin
            {
                Debug.Log("bombe dessus");
                if (mapItemsList[x , y - 2] is Sol) //Sol derri�re la bombe
                {
                    Debug.Log("bombe boug�e");
                    GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y-1] = null;
                    MoveBomb(x, y-2);
                    GameObject.Find("Map").GetComponent<Map>().mapEnnemisList[x, y-2] = new Bomb();
                }
            }
        }
        

    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        /*Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);*/
        transform.position = new Vector3(_horizontalMovement, _verticalMovement, 0);
    }
    
    void MoveBomb(float _horizontalMovement, float _verticalMovement)
    {
        Bomb newbomb = GameObject.Find("Bomb(Clone)").GetComponent<Bomb>();
        newbomb.transform.position = new Vector3(_horizontalMovement, _verticalMovement, -3);

    }
}
