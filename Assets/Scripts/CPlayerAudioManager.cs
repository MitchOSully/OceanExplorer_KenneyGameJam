using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CPlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_sandWalkClips;

    private AudioSource m_audioSource;
    private TarodevController.CJoePlayerController m_playerController;

    private float m_nTimeSincePlayStart = -1;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_playerController = GetComponent<TarodevController.CJoePlayerController>();
    }

    void Update()
    {
        if (!m_playerController.m_bUnderwater && Math.Abs(m_playerController.Velocity.x) > 0 && m_playerController.Grounded)
        {
            if (m_nTimeSincePlayStart < 0) // Haven't been playing sound. Start now.
            {
                m_audioSource.clip = SelectRandomClip();
                m_audioSource.Play();
                m_nTimeSincePlayStart = 0;
            }
            else if (m_nTimeSincePlayStart > m_audioSource.clip.length) // Finished playing walking sound. Switch to other sound and start again.
            {
                if (m_audioSource.clip.name == "SnowWalk")
                    m_audioSource.clip = SelectRandomClip();
                else
                    m_audioSource.clip = SelectRandomClip();
                m_audioSource.Play();
                m_nTimeSincePlayStart = 0;
            }
            else
            {
                m_nTimeSincePlayStart += Time.deltaTime;
            }
        }
        else // Underwater or no longer walking or in the air
        {
            //m_audioSource.Stop(); // Stopping audio clip here feels stunted
            m_nTimeSincePlayStart = -1;
        }
    }

    AudioClip SelectRandomClip()
    {
        System.Random rnd = new System.Random();
        int idx = rnd.Next(0, m_sandWalkClips.Length);
        return m_sandWalkClips[idx];
    }
}
