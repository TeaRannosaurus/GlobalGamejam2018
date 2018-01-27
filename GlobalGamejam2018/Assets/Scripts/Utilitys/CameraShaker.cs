using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [Header("Shake properties")]
    public float shakeDuration = 0.0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector2 m_Origin;
    private void Update()
    {

        if (shakeDuration > 0)
        {
            Vector3 newPosition = new Vector3(m_Origin.x + Random.insideUnitCircle.x * shakeAmount, m_Origin.y + Random.insideUnitCircle.y * shakeAmount, 0.0f);
            transform.localPosition = newPosition;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    public void ShakeCamera(float duration, float intensity)
    {
        Debug.Log("shake");
        shakeDuration = duration;
        shakeAmount = intensity;
    }
}
