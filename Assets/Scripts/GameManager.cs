using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject pointer;

    [SerializeField] int numberOfClicks = 5;
    private int currentClicks = 0;
    public bool gameActive = true;

    public float score = 1000f;
    public bool isSafe = false;

    private PointerBehaviour pointerScript;
    private float prevPos = 0;

    private void Start()
    {
        pointerScript = pointer.GetComponent<PointerBehaviour>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pointerScript.onTarget2)
            {
                MoveTarget();
                StartCoroutine(SetSafeMode());
                currentClicks++;
                if (currentClicks == numberOfClicks)
                {
                    gameActive = false;
                }
                if (!pointerScript.onTarget)
                {
                    score -= 10;
                }
            }           
            else
            {
                score -= 25;
            }
        }
    }

    void MoveTarget()
    {
        if (gameActive == false)
        {
            target.SetActive(false);
            return;
        }
        float pos;
        pos = Random.Range(0, 360);
        print(pos);
        target.transform.eulerAngles = new Vector3(0, 0, pos);
        prevPos = pos;
    }

    IEnumerator SetSafeMode()
    {
        isSafe = true;
        yield return new WaitForSeconds(1f);
        isSafe = false;
    }
}
