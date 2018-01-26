using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBase : MonoBehaviour
{
    [Header("Base dino properties")]
    public int health       = 1;
    public float speed      = 10.0f;

    public float changedirectionChance = 0.5f;
    public Vector2 travelTime = new Vector2(0.5f, 3.0f);

    private bool m_IsMovingRight = true;
    private float m_TravelTimeCounter = 0.0f;

    private void Start()
    {
        if (Random.value < 0.5f)
            m_IsMovingRight = !m_IsMovingRight;

        Vector2 newScale = transform.localScale;

        if (m_IsMovingRight)
        {
            newScale.x *= -1;
            transform.localScale = newScale;
        }
        else
        {
            newScale.x *= 1;
            transform.localScale = newScale;
        }
    }

    public void Init()
    {
        
    }

    private void Update()
    {
        m_TravelTimeCounter -= Time.deltaTime;

        if (m_TravelTimeCounter <= 0.0f)
        {
            if (Random.value < changedirectionChance)
            {
                Flip();
            }
            else
            {
                m_TravelTimeCounter = RandomTravelTime();
            }
        }

        Vector2 newPosition = Vector2.zero;

        if (m_IsMovingRight)
           newPosition = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else if(!m_IsMovingRight)
            newPosition = new Vector2(transform.position.x + speed * Time.deltaTime * -1.0f, transform.position.y);

        transform.position = newPosition;
    }

    private void Flip()
    {
        m_IsMovingRight = !m_IsMovingRight;
        Vector2 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        m_TravelTimeCounter = RandomTravelTime();
    }

    private float RandomTravelTime()
    {
        return Random.Range(travelTime.x, travelTime.y);
    }


}
