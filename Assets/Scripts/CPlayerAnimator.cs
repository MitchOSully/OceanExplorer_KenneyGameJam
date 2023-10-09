using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerAnimator : MonoBehaviour
{
    public TarodevController.CJoePlayerController m_playerController;
    public Animator m_animator;

    [System.NonSerialized]
    public bool m_bIsTalking = false, m_bIsDead = false;

    private void Update()
    {
        //Obtain relevant info from controller scripts
        float fHorizontalInput = m_playerController.Input.X;
        float fHorizontalVelocity = m_playerController.Velocity.x;    
        
        //Make player face in the correct direction
        if (fHorizontalInput > 0.01f) //Facing right
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (fHorizontalInput < -0.01f || m_bIsTalking) //Facing left
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Set animation parameters
        m_animator.SetFloat("Speed", Mathf.Abs(fHorizontalVelocity));
        m_animator.SetBool("IsJumping", m_playerController.enabled && !m_playerController.IsUnderwater() && !m_playerController.Grounded);
        m_animator.SetBool("IsSwimming", m_playerController.enabled && m_playerController.IsUnderwater());
        m_animator.SetBool("IsTalking", m_bIsTalking);
        m_animator.SetBool("IsDead", m_bIsDead);
    }
}
