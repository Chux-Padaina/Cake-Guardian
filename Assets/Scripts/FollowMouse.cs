using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Camera mainCamera;

    Vector3 mousePos;
    Vector3 objectPos;
    float angle;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 5.23f; //The distance between the camera and object
        objectPos = mainCamera.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
