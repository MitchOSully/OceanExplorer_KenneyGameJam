using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCollectibe : MonoBehaviour
{
    public CGameManager m_gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_gameManager.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
