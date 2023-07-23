using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerAnimator : MonoBehaviour
{
    public TarodevController.PlayerController m_playerControllerLand;
    public TarodevController.PlayerControllerWater m_playerControllerWater;
    public Animator m_animator;

    [System.NonSerialized]
    public bool m_bIsTalking = false, m_bIsDead = false;

    private void Update()
    {
        float fHorizontalVelocity = 0;
        if (m_playerControllerLand.enabled)
        {
            fHorizontalVelocity = m_playerControllerLand.Velocity.x;
        }
        else if (m_playerControllerWater.enabled)
        {
            fHorizontalVelocity = m_playerControllerWater.Velocity.x;
        }

        //Make player face in the correct direction
        if (fHorizontalVelocity > 0.01f) //Facing right
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (fHorizontalVelocity < -0.01f || m_bIsTalking) //Facing left
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        m_animator.SetFloat("Speed", Mathf.Abs(fHorizontalVelocity));
        m_animator.SetBool("IsJumping", m_playerControllerLand.enabled && !m_playerControllerLand.Grounded);
        m_animator.SetBool("IsSwimming", !m_playerControllerLand.enabled && m_playerControllerWater.enabled);
        m_animator.SetBool("IsTalking", m_bIsTalking);
        m_animator.SetBool("IsDead", m_bIsDead);
    }
}
