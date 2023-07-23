using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCollectible : MonoBehaviour
{
    public CGameManager m_gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_gameManager.IncreaseScore();
            Destroy(gameObject);
        }
    }
}