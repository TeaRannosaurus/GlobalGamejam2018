using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidBase : MonoBehaviour
{
    [Header("Astroid base properties")]
    public float movementSpeed = 10.0f;

    public float shakeAmount = 5.0f;
    public float shakeDuration = 1.0f;
    public Vector2 targetLocation;

    private bool m_HasShaken = false;

    public void Init(Vector2 targetLocation)
    {
        this.targetLocation = targetLocation;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetLocation, movementSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MonoBehaviour[] posibleDamagebales = other.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour posibleDamagebale in posibleDamagebales)
        {
            if (posibleDamagebale is IDamageable)
            {
                IDamageable damageable = (IDamageable) posibleDamagebale;
                damageable.TakeDamage(1);
            }
        }

        if (other.tag == "Enviorment")
        {
            if (!m_HasShaken)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShaker>().ShakeCamera(shakeDuration, shakeAmount);
                m_HasShaken = true;
                Destroy(gameObject, 2.0f);
            }
        }
    }
}
