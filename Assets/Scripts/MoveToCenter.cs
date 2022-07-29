using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float damageAmount = 5f;
    [SerializeField] ParticleSystem blast;
    private GameManager gm;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.position * -0.1f * moveSpeed * gm.speedMulti;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.position * -0.1f * moveSpeed;

        gm = FindObjectOfType<GameManager>();
        rb.AddTorque(Random.Range(-25, 25));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destructor"))
        {
            if(gm.gameState == GameManager.State.Guardian)
            {
                ParticleSystem x = Instantiate(blast, gm.transform.position, transform.rotation);
                StartCoroutine(DesBlast(x));
            }
            Destroy(gameObject);
            gm.currentHP -= damageAmount;
        }
    }

    private void OnMouseDown()
    {
        if (gm.hittable)
        {
            ParticleSystem x = Instantiate(blast, transform.position, transform.rotation);
            StartCoroutine(DesBlast(x));
            Destroy(gameObject);
        }
    }

    IEnumerator DesBlast(ParticleSystem x)
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(x);
    }
}
