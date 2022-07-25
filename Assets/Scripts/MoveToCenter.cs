using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    private void Update()
    {
        transform.Translate(-transform.position * moveSpeed * Time.deltaTime / 10);
    }
}
