using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusiqueMenuDelai : MonoBehaviour
{
    public float timeLeft=15.348f;
    public AudioSource son;
    public bool lance=false;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 15.348f;
        son = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.deltaTime);
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 && !lance)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
