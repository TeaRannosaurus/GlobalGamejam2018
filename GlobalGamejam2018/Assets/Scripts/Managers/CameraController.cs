using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static float pixelsToUnits = 1.0f;
    public static float scale = 1.0f;

    [Header("Scroll Properties")]
    public float scrollBoundary = 50.0f;
    public float scrollSpeed    = 10.0f;

    public Vector2 nativeResolustion = new Vector2(240, 160);

    private Camera m_Camera;

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();

        if (m_Camera.orthographic)
        {
            scale = Screen.height / nativeResolustion.y;
            pixelsToUnits *= scale;
            m_Camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;
        }
    }

    private void Update()
    {
        if (!GameManager.Get.gameHasStarted)
            return;

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
