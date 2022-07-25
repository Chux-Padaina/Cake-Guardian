using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject gunPoint;
    [SerializeField] GameObject bullet;

    [SerializeField] GameObject CAFE;
    [SerializeField] GameObject GUARDIAN;

    public enum State
    {
        Cafe, Guardian
    }

    public State gameState = State.Cafe;

    [SerializeField] int numberOfClicks = 5;
    private int currentClicks = 0;
    public bool gameActive = true;

    public float score = 1000f;
    public bool isSafe = false;

    private PointerBehaviour pointerScript;

    SpriteRenderer[] renderers;

    private void Start()
    {
        pointerScript = pointer.GetComponent<PointerBehaviour>();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(gameState == State.Cafe)
            {
                gameState = State.Guardian;

                renderers = CAFE.GetComponentsInChildren<SpriteRenderer>();
                foreach(SpriteRenderer x in renderers)
                {
                    x.enabled = false;
                }
                renderers = GUARDIAN.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer x in renderers)
                {
                    x.enabled = true;
                }
            }
            else if(gameState == State.Guardian)
            {
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
        if(gameState == State.Cafe)
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
        else if(gameState == State.Guardian)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var newBullet = Instantiate(bullet, gunPoint.transform.position, gunPoint.transform.rotation);
                newBullet.transform.parent = GUARDIAN.transform;
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
    }

    IEnumerator SetSafeMode()
    {
        isSafe = true;
        yield return new WaitForSeconds(1f);
        isSafe = false;
    }
}
