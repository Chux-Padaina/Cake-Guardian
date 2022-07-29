using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] float xDist;
    [SerializeField] float yRange;
    [SerializeField] float spawnDelay;
    [SerializeField] GameObject Guardian;
    [SerializeField] GameManager gm;

    float currentTime = Mathf.Infinity;

    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >=spawnDelay * gm.multiplier)
        {
            SpawnMonster();
            currentTime = 0;
        }
    }

    void SpawnMonster()
    {
        Vector2 pos1 = new Vector2(xDist, Random.Range(yRange, -yRange));
        Vector2 pos2 = new Vector2(-xDist, Random.Range(yRange, -yRange));

        GameObject mons1 = Instantiate(monster, pos1, monster.transform.rotation);
        GameObject mons2 = Instantiate(monster, pos2, monster.transform.rotation);

        float zRotRandom;

        zRotRandom = Random.Range(0, 360);
        
        mons1.transform.eulerAngles = new Vector3(0, 0, zRotRandom);
        mons1.transform.parent = Guardian.transform;

        zRotRandom = Random.Range(0, 360);
        mons2.transform.eulerAngles = new Vector3(0, 0, zRotRandom);
        mons2.transform.parent = Guardian.transform;

        if (gm.gameState == GameManager.State.Cafe)
        {
            mons1.GetComponent<SpriteRenderer>().enabled = false;
            mons2.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
