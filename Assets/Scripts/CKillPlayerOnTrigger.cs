using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKillPlayerOnTrigger : MonoBehaviour
{
    public CGameManager m_gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_gameManager.KillPlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SkryperBoundary")
        {
            gameObject.transform.position -= new Vector3(0, 0.1f, 0);
        }
    }
}
