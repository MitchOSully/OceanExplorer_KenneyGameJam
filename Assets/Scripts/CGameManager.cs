using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CGameManager : MonoBehaviour
{
    public GameObject m_player;
    public TextMeshProUGUI m_scoreText;

    private int m_iScore = 0;
    private GameObject[] m_treasures;
    
    private TarodevController.PlayerController      m_playerControllerLand;
    private TarodevController.PlayerControllerWater m_playerControllerWater;

    private void Start()
    {
        m_playerControllerLand = m_player.GetComponent<TarodevController.PlayerController>();
        m_playerControllerWater = m_player.GetComponent<TarodevController.PlayerControllerWater>();

        m_treasures = GameObject.FindGameObjectsWithTag("Treasure");
        foreach (GameObject treasure in m_treasures)
        {
            treasure.GetComponent<CCollectible>().m_gameManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            m_player.transform.position = new Vector3(0.64f, -1.36f, 0);
            m_playerControllerLand.enabled = true;
            m_playerControllerWater.enabled = false;
        }
    }

    public int GetScore()
    {
        return m_iScore;
    }
    public void IncreaseScore(int iInc = 1)
    {
        m_iScore += iInc;
        m_scoreText.SetText("Treasures Found: " + m_iScore);
    }
}
