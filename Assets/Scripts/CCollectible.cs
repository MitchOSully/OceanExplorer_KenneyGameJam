using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCollectible : MonoBehaviour
{
    public CGameManager m_gameManager;
    public GameObject m_collectParticlePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collect();
        }
    }

    public void Collect()
    {
        Instantiate(m_collectParticlePrefab, transform.position, transform.rotation);
        //TO DO: play sound
        m_gameManager.IncreaseScore();
        Destroy(gameObject);
    }
}
