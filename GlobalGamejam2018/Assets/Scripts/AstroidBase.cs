using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidBase : MonoBehaviour
{
    [Header("Astroid base properties")]
    public float movementSpeed = 10.0f;
    public Vector2 targetLocation;

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
    }
}
