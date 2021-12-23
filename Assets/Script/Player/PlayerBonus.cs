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

    void Start()
    {

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
        

        /*Appel des fonction pour les bonus*/

        if (mapItemsList[x, y] is BonusPuissance)
        {
            ((Bonus)mapItemsList[x, y]).OnConsumption();
        }

        if (mapItemsList[x, y] is BonusGodMod)
        {
            ((Bonus)mapItemsList[x, y]).OnConsumption();
        }

        if (mapItemsList[x, y] is BonusPoussee)
        {
            ((Bonus)mapItemsList[x, y]).OnConsumption();
        }

        if (mapItemsList[x, y] is BonusVitesse)
        {
            ((Bonus)mapItemsList[x, y]).OnConsumption();
        }


    }

}
