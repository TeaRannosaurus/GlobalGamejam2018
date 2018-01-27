using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Scroll Properties")]
    public float scrollBoundary = 50.0f;
    public float scrollSpeed    = 10.0f;

    private void Update()
    {
        if (Input.mousePosition.x > Screen.width - scrollBoundary)
        {
            Vector3 newPosition = new Vector3(transform.position.x + scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        if (Input.mousePosition.x < 0 + scrollBoundary)
        {
            Vector3 newPosition = new Vector3(transform.position.x - scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }

}
