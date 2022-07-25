using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBehaviour : MonoBehaviour
{
    public bool onTarget = false;
    public bool onTarget2 = false;

    public GameManager gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainTarget"))
            onTarget = true;
        if (collision.CompareTag("SecondaryTarget"))
            onTarget2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainTarget"))
            onTarget = false;
        if (collision.CompareTag("SecondaryTarget"))
        {
            onTarget2 = false;
            if(!gm.isSafe)
            gm.score -= 25;
        }
    }
}
