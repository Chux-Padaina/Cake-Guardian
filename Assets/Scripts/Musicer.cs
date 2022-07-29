using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicer : MonoBehaviour
{
    public GameObject musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Music") == null)
        {
            Instantiate(musicPlayer);
        }
    }
}
