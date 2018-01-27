using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

public class DinoBase : MonoBehaviour, IDamageable
{
    [Header("Base dino properties")]
    public int health       = 1;
    public float speed      = 10.0f;

    public float changedirectionChance = 0.5f;
    public Vector2 travelTime = new Vector2(0.5f, 3.0f);

    private bool m_IsAlive = true;
    private bool m_IsMovingRight = true;
    private float m_TravelTimeCounter = 0.0f;


    [Header("Base dino effects")]
    [SerializeField] private GameObject m_BloodParticleSystem   = null;
    [SerializeField] private GameObject[] m_FixedBodyPartSystems= null;
    //[SerializeField] private Sprite[] m_FixedBodyParts          = null;
    //[SerializeField] private Sprite[] m_RandomBodyParts         = null;

    [Header("Sound effects")]
    [SerializeField] private AudioClip[] m_IdleClips            = null;
    [SerializeField] [Range(0.0f, 1.0f)] private float m_IdleGruntChange;
    [SerializeField] private AudioClip[] m_MovementClips        = null;
    [SerializeField] private AudioClip[] m_Hurtclips            = null;
    [SerializeField] private AudioClip[] m_DeathClips           = null;
    private SoundEffectManager m_SoundEffectManager             = null;

    private SpriteRenderer m_SpriteRenderer                     = null;
    private Animator m_Animator                                 = null;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_Animator = GetComponent<Animator>();
        m_SoundEffectManager = GetComponent<SoundEffectManager>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if(m_Animator == null)
            Debug.LogError("No animator found on this object", this);

        if (m_SoundEffectManager == null)
            Debug.LogError("No sound effect manager found on this object", this);


        if (Random.value < 0.5f)
            m_IsMovingRight = !m_IsMovingRight;

        Vector2 newScale = transform.localScale;

        if (m_IsMovingRight)
        {
            m_SpriteRenderer.flipX = true;
            transform.localScale = newScale;
        }
        else
        {
            m_SpriteRenderer.flipX = false;
            transform.localScale = newScale;
        }

        m_IsAlive = true;
    }

    public void PlayStepSound()
    {
        AttemptPlaySound(m_MovementClips);
    }

    private void Update()
    {
        if(!m_IsAlive)
            return;

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

        if(Random.value < m_IdleGruntChange)
            AttemptPlaySound(m_IdleClips);

        Vector2 newPosition = Vector2.zero;

        if (m_IsMovingRight)
           newPosition = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else if(!m_IsMovingRight)
            newPosition = new Vector2(transform.position.x + speed * Time.deltaTime * -1.0f, transform.position.y);

        transform.position = newPosition;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            AttemptPlaySound(m_Hurtclips);
        }
    }


    public void Die()
    {
        m_IsAlive = false;

        m_Animator.enabled = false;
        m_SpriteRenderer.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;

        foreach (GameObject bodyPartSystem in m_FixedBodyPartSystems)
        {
            GameObject newPartSystem = Instantiate(bodyPartSystem, transform.position, Quaternion.identity);
            ParticleSystem particleSystemComp = newPartSystem.GetComponent<ParticleSystem>();
            particleSystemComp.Play();
            //Destroy(newPartSystem, 5.0f);
            
        }
        /*
        GameObject particlesystemObject = Instantiate(m_BloodParticleSystem, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particlesystemObject.GetComponent<ParticleSystem>();
        particleSystem.Play();

    */
        AttemptPlaySound(m_DeathClips);
        Destroy(this.gameObject, 5.0f);
    }

    private void Flip()
    {
        m_IsMovingRight = !m_IsMovingRight;
        m_SpriteRenderer.flipX = m_IsMovingRight;
        m_TravelTimeCounter = RandomTravelTime();
    }

    private float RandomTravelTime()
    {
        return Random.Range(travelTime.x, travelTime.y);
    }

    protected void AttemptPlaySound(AudioClip[] potentialClips)
    {
        if (potentialClips == null || potentialClips.Length == 0)
        {
            Debug.LogError("An sound that was attempted to play was empty", this);
            return;
        }

        m_SoundEffectManager.PlaySound(potentialClips);
    }

}
