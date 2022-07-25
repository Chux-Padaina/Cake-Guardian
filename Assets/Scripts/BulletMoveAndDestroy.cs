using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveAndDestroy : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;

    private void Update()
    {
        DestoryOnOutOfBounds();
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void DestoryOnOutOfBounds()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if(x >= 20 || x <= -20 || y >= 40 || y <= -40)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Destroy(collision.gameObject);
        }
    }
}
