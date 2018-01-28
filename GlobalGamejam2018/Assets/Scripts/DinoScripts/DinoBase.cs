using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBase : MonoBehaviour, IDamageable
{
    [Header("Base dino properties")]
    public string speciesName   = "Unnamed species";
    public int health           = 1;
    public float speed          = 10.0f;
    public float panicSpeed     = 20.0f;
    public int scoreWorth       = 10;
    public float babyScoreMultyplier = 2.0f;

    public float changedirectionChance = 0.5f;
    public Vector2 travelTime = new Vector2(0.5f, 3.0f);

    [HideInInspector] public bool isChild = false;

    private bool m_IsAlive = true;
    private bool m_IsMovingRight = true;
    private float m_TravelTimeCounter = 0.0f;
    private bool m_InPanic = false;

    [Header("Dino size")]
    public Transform body = null;
    public Transform head = null;
    public Transform headpivot = null;
    public Vector3 childSize = Vector3.one;

    [Header("Base dino particle effects")]
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
    private BoxCollider2D m_Collider                            = null;

    private float m_UniqueRandomValue                           = 0.1f;

    private void Start()
    {
        Init();
    }

    public void Init(bool becomeChild = false)
    {
        m_UniqueRandomValue = Random.Range(-0.1f, 0.1f);

        m_Animator = GetComponent<Animator>();
        m_SoundEffectManager = GetComponent<SoundEffectManager>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();

        if(m_Animator == null)
            Debug.LogError("No animator found on this object", this);

        if (m_SoundEffectManager == null)
            Debug.LogError("No sound effect manager found on this object", this);


        if (becomeChild)
        {
            BecomeChild();
        }

        if (Random.value < 0.5f)
            m_IsMovingRight = !m_IsMovingRight;

        Vector2 newScale = transform.localScale;
        newScale.x += m_UniqueRandomValue;
        newScale.y += m_UniqueRandomValue;

        float uniqueAnimationSpeed = m_Animator.GetFloat("Speed") * Random.Range(1.0f, 1.1f);

        m_Animator.SetFloat("Speed", uniqueAnimationSpeed);

        if (m_IsMovingRight)
        {
            //newScale.x *= 1;
            Flip();
            transform.localScale = newScale;
        }
        else
        {
            //newScale.x *= -1;
            transform.localScale = newScale;
        }

        m_IsAlive = true;
        m_InPanic = false;
    }

    public void PlayStepSound()
    {
        //AttemptPlaySound(m_MovementClips);
    }

    public void InitiatePanic()
    {
        if (m_InPanic)
            return;

        m_InPanic = true;
        speed = panicSpeed;
        m_Animator.SetFloat("Speed", m_Animator.GetFloat("Speed") * 2);
    }

    private void Update()
    {
        if(!m_IsAlive)
            return;

        if (transform.position.y < -200.0f)
        {
            scoreWorth = 0;
            Die(true);
        }

        head.position = headpivot.position;
        head.rotation = headpivot.rotation;

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

    public void Die(bool dieInSilence = false, bool givesScore = true)
    {
        m_IsAlive = false;

        m_Animator.enabled = false;
        m_SpriteRenderer.enabled = false;

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false;
        }

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        head.GetComponent<SpriteRenderer>().enabled = false;
        foreach (GameObject bodyPartSystem in m_FixedBodyPartSystems)
        {
            GameObject newPartSystem = Instantiate(bodyPartSystem, transform.position, Quaternion.identity);
            ParticleSystem particleSystemComp = newPartSystem.GetComponent<ParticleSystem>();
            particleSystemComp.Play();
            
        }
        /*
        GameObject particlesystemObject = Instantiate(m_BloodParticleSystem, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particlesystemObject.GetComponent<ParticleSystem>();
        particleSystem.Play();

    */
        GameObject.FindGameObjectWithTag("Manager").SendMessage("SpeciesDied", speciesName);

        if(givesScore)
            GameManager.Get.score += scoreWorth;

        if(!dieInSilence)
            AttemptPlaySound(m_DeathClips);

        Destroy(this.gameObject, 5.0f);
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

    private void BecomeChild()
    {
        body.localScale = childSize;
        head.transform.position = headpivot.position;

        Vector2 colliderBound = body.GetComponent<SpriteRenderer>().sprite.bounds.size / 2;
        m_Collider.size = colliderBound;
        m_Animator.SetFloat("Speed", m_Animator.GetFloat("Speed") * 2);
        scoreWorth = (int)(scoreWorth * babyScoreMultyplier);
        health = 1;
        //m_Collider.offset
    }

    public bool IsAlive()
    {
        return m_IsAlive;
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
