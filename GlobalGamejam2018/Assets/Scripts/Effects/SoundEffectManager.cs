using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float pitchDifference = 0.05f;
    [HideInInspector] public AudioSource audioSource;

    private float m_minPitch = 1;
    private float m_maxPitch = 1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        m_minPitch -= pitchDifference;
        m_maxPitch += pitchDifference;
    }

    /// <summary>
    /// Plays the passed sound at an random pitch
    /// </summary>
    /// <param name="clipToPlay">Clip to play</param>
    public void PlaySound(AudioClip clipToPlay)
    {
        SetRandomPitch();

        audioSource.PlayOneShot(clipToPlay);
    }

    /// <summary>
    /// Plays an random sound from the given array
    /// </summary>
    /// <param name="clipsToPlay">Clips to play</param>
    public void PlaySound(AudioClip[] clipsToPlay)
    {
        PlaySound(clipsToPlay[Random.Range(0, clipsToPlay.Length)]);
    }

    private void SetRandomPitch()
    {
        audioSource.pitch = Random.Range(m_minPitch, m_maxPitch);
    }
}
