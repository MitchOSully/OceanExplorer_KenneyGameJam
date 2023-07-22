using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSwitchLandWaterControls : MonoBehaviour
{
    public GameObject m_player;

    private TarodevController.PlayerController      m_playerControllerLand;
    private TarodevController.PlayerControllerWater m_playerControllerWater;
    private float m_fEnterYPos;

    private void Start()
    {
        m_playerControllerLand = m_player.GetComponent<TarodevController.PlayerController>();
        m_playerControllerWater = m_player.GetComponent<TarodevController.PlayerControllerWater>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_fEnterYPos = collision.gameObject.transform.position.y;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float fExitYPos = collision.gameObject.transform.position.y;
            float fTolerance = 0.9f * collision.bounds.extents.y; // If player passed over waterline,
                                                                  // their position should have changed
                                                                  // by exactly collision.bounds.extents.y
            //NOTE: The bigger the bounds of the other collider, the more likely this is going to work.
            //     A better method should be used in the future to ensure it NEVER fails. But this is good enough for now
            if (fExitYPos > m_fEnterYPos + fTolerance)
            {
                m_playerControllerLand.enabled = true;
                m_playerControllerWater.enabled = false;
            }
            else if (fExitYPos < m_fEnterYPos - fTolerance)
            {
                m_playerControllerLand.enabled = false;
                m_playerControllerWater.enabled = true;
            }
        }
    }
}
