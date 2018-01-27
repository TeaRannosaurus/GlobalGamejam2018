using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Scroll Properties")]
    public float scrollBoundary = 50.0f;
    public float scrollSpeed    = 10.0f;

    [Header("Shake properties")]
    public float shakeDuration = 0.0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector2 origin;

    private void Start()
    {
        origin = transform.position;
    }

    private void Update()
    {

        if (shakeDuration > 0)
        {
            Vector3 newPosition = new Vector3(origin.x + Random.insideUnitCircle.x * shakeAmount, origin.y + Random.insideUnitCircle.y * shakeAmount, transform.position.z);
            transform.localPosition = newPosition;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }

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

    public void ShakeCamera(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeAmount = intensity;
    }

}
