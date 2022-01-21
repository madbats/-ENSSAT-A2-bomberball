using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiqueMenuDelai : MonoBehaviour
{
    public float timeLeft=15.348f;
    public AudioSource son;
    public bool lance=false;
    // Start is called before the first frame update
    void Start()
    {

        son = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 && !lance)
        {
            son.Play();
            lance = true;
        }
    }
}
