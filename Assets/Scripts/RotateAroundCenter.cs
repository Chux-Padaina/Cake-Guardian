using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCenter : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;

    private void Update()
    {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.forward, -rotationSpeed * Time.deltaTime);
    }
}
