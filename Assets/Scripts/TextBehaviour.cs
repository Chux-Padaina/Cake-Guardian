using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBehaviour : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameManager gm;
    private void Update()
    {
        if(gm.gameState == GameManager.State.Cafe)
        {
            text.text = "press SPACE to switch";
            text.color = new Color32(24, 145, 220, 255);
        }
        else if(gm.gameState == GameManager.State.Guardian)
        {
            text.text = "press SPACE to switch";
            text.color = new Color32(242, 136, 155, 255);
        }
        else
        {
            text.text = "press P to start";
        }
    }
}
