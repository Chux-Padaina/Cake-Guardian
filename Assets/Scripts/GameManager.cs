using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject pointer;

    public float speedMulti = 1;

    [SerializeField] GameObject musicPlayer;

    [SerializeField] Animator centerCircleAnimator;
    [SerializeField] Animator targetAnimator;

    [SerializeField] GameObject CAFE;
    [SerializeField] GameObject GUARDIAN;

    [SerializeField] GameObject[] images;

    [SerializeField] GameObject cafeIN;
    [SerializeField] GameObject guardianIN;

    public float currentHP = 100;
    public bool hittable = false;

    [SerializeField] TMP_Text hptext;

    [SerializeField] int maxCakeClicks = 16;
    [SerializeField] int currentCakeClicks = 0;
    int cakeIndex = 0;

    public float multiplier = 1;

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
        if (currentHP <= 0)
        {
            GameOver();
        }
        if(cakeIndex == 8)
        {
            GameSuccess();
        }

        if(gameState == State.Pause)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                CAFE.SetActive(true);
                GUARDIAN.SetActive(true);
                gameState = State.Cafe;
                guardianIN.SetActive(false);
                cafeIN.SetActive(true);
            }
        }
        else
        {
            hptext.text = currentHP.ToString();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameState == State.Cafe)
                {
                    gameState = State.Guardian;

                    guardianIN.SetActive(true);
                    cafeIN.SetActive(false);

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

                    guardianIN.SetActive(false);
                    cafeIN.SetActive(true);

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
                cakeIndex = currentCakeClicks / 2;

                switch (cakeIndex)
                {
                    case 0: multiplier = 1;break;
                    case 1: multiplier = 0.7f;break;
                    case 2: multiplier = 0.6f; speedMulti = 1.15f; break;
                    case 3: multiplier = 0.5f; speedMulti = 1.25f; break;
                    case 4: speedMulti = 1.5f; break;
                    case 5: speedMulti = 2f; break;
                    case 6: speedMulti = 2.5f; break;
                    case 7: speedMulti = 3f; break;
                    default: multiplier = 1; break;
                }

                for(int i = 0; i <= cakeIndex; i++)
                {
                    images[i].SetActive(true);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    centerCircleAnimator.SetTrigger("Clicked");

                    if (pointerScript.onTarget2)
                    {
                        MoveTarget();
                        StartCoroutine(SetSafeMode());
                        currentCakeClicks++;
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
                hittable = true;
                if (Input.GetMouseButtonDown(0))
                {
                    //CHECK IF CLICK ON ASTEROID, THEN DESTROY
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

    void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    void GameSuccess()
    {
        SceneManager.LoadScene(3);
    }
}
