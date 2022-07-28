using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject pointer;

    [SerializeField] Animator centerCircleAnimator;
    [SerializeField] Animator targetAnimator;

    [SerializeField] GameObject CAFE;
    [SerializeField] GameObject GUARDIAN;

    [SerializeField] float hitPoints = 100;

    public float currentHP = 100;
    public bool hittable = false;


    [SerializeField] float shiftTime = 30;
    float timeSinceShiftStart = 0;
    float remainingShiftTime;

    [SerializeField] TMP_Text shiftTimeText;

    

    public enum State
    {
        Cafe, Guardian, Pause
    }

    public State gameState;
    public bool gameActive = true;

    public float score = 1000f;
    public bool isSafe = false;

    private PointerBehaviour pointerScript;

    SpriteRenderer[] renderers;

    private void Start()
    {
        pointerScript = pointer.GetComponent<PointerBehaviour>();

        gameState = State.Pause;

        renderers = GUARDIAN.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer x in renderers)
        {
            x.enabled = false;
        }
        renderers = CAFE.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer x in renderers)
        {
            x.enabled = true;
        }

        CAFE.SetActive(false);
        GUARDIAN.SetActive(false);
    }

    private void Update()
    {
        remainingShiftTime = shiftTime - timeSinceShiftStart;

        int timeLeft = ((int)remainingShiftTime);

        shiftTimeText.text = "Shift Time " + timeLeft;


        if(gameState == State.Pause)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                CAFE.SetActive(true);
                GUARDIAN.SetActive(true);
                gameState = State.Cafe;
            }
        }
        else
        {
            timeSinceShiftStart += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.P))
            {
                CAFE.SetActive(false);
                GUARDIAN.SetActive(false);
                gameState = State.Pause;
                hittable = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameState == State.Cafe)
                {
                    gameState = State.Guardian;

                    renderers = CAFE.GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer x in renderers)
                    {
                        x.enabled = false;
                    }
                    renderers = GUARDIAN.GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer x in renderers)
                    {
                        x.enabled = true;
                    }
                }
                else if (gameState == State.Guardian)
                {
                    hittable = false;
                    gameState = State.Cafe;

                    renderers = GUARDIAN.GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer x in renderers)
                    {
                        x.enabled = false;
                    }
                    renderers = CAFE.GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer x in renderers)
                    {
                        x.enabled = true;
                    }
                }
            }
            if (gameState == State.Cafe)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    centerCircleAnimator.SetTrigger("Clicked");

                    if (pointerScript.onTarget2)
                    {
                        MoveTarget();
                        StartCoroutine(SetSafeMode());
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
            else if (gameState == State.Guardian)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //CHECK IF CLICK ON ASTEROID, THEN DESTROY
                    hittable = true;
                }
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
        target.transform.eulerAngles = new Vector3(0, 0, pos);

        targetAnimator.SetTrigger("Spawned");
    }

    IEnumerator SetSafeMode()
    {
        isSafe = true;
        yield return new WaitForSeconds(1f);
        isSafe = false;
    }
}
