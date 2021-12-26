using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    //Attributs
    /*public float movingSpeed;
    public float smoothTime;
    public bool BombSet = false;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public Bomb bomb;*/

    public int puissance;
    public int poussee;
    public Bonus[] bonusList;
    public int nbBonus;

    void Start()
    {
        bonusList = new Bonus[30];
        Debug.Log(bonusList.Length);
        nbBonus = 0;
        Debug.Log(nbBonus);
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
        

        /*Appel des fonctions pour les bonus*/

        if (mapItemsList[x, y] is Bonus)
        {
            if (((Bonus)mapItemsList[x, y]).OnConsumption())
            {
                bonusList[nbBonus] = (Bonus)mapItemsList[x, y];
                nbBonus += 1;
            }
            
        }

        foreach(Bonus bonus in bonusList)
        {
            
            if(bonus != null)
            {
                if (bonus.CheckEnd())
                {
                    bonus.Destruction();

                }
            }
        }


    }

}
