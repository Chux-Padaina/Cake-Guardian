using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float damageAmount = 5f;

    private GameManager gm;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.position * -0.1f * moveSpeed;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.position * -0.1f * moveSpeed;

        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destructor"))
        {
            Destroy(gameObject);
            gm.currentHP -= damageAmount;
        }
    }

    private void OnMouseDown()
    {
        if (gm.hittable)
        {
            Destroy(gameObject);
        }
    }
}
