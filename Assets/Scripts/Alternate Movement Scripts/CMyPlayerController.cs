using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FrameInput
{
    public float X;
    public float Y;
    public bool JumpDown;
    public bool JumpUp;
}

public class CMyPlayerController : MonoBehaviour
{
    //Public members
    public float m_fJumpStrength = 25;
    public float m_fWalkSpeed = 10;
    public Rigidbody2D m_rb;
    public CPhoneControls m_phoneControls;
    //Private members
    private FrameInput Input;
    private bool m_bPhoneControlsActive = false;

    private void Update()
    {
        GatherInput();

        MoveCharacter();
    }

    #region Gather Input
    private void GatherInput()
    {
        if (m_bPhoneControlsActive)
        {
            Input = new FrameInput
            {
                JumpDown = m_phoneControls.m_bJumpDown,
                JumpUp = m_phoneControls.m_bJumpUp,
                X = m_phoneControls.m_moveVector.x,
                Y = m_phoneControls.m_moveVector.y //Y does nothing :)
            };
        }
        else
        {
            Input = new FrameInput 
            {
                JumpDown = UnityEngine.Input.GetButtonDown("Jump"),
                JumpUp = UnityEngine.Input.GetButtonUp("Jump"),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
                Y = UnityEngine.Input.GetAxisRaw("Vertical") //Y does nothing :)
            };
        }
    }
    #endregion

    #region Move
    private void MoveCharacter()
    {
        //Land
        //if (Input.JumpDown)
        //{
        //    m_rb.velocity = Vector2.up * m_fJumpStrength;
        //}
        //m_rb.velocity = new Vector2(In`put.X * m_fWalkSpeed, m_rb.velocity.y);

        //Water
        m_rb.velocity = new Vector2(Input.X * m_fWalkSpeed, Input.Y * m_fWalkSpeed);
    }
    #endregion
}
