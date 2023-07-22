using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CGameManager : MonoBehaviour
{
    public GameObject m_player;
    public GameObject m_canvas;

    public TextMeshProUGUI m_scoreText;

    public Image m_clockImage;
    public float m_fDayLength = 60; //60 seconds = 1 day in-game
    public Sprite m_oclock12, m_oclock130, m_oclock3, m_oclock430, m_oclock6, m_oclock730, m_oclock9, m_oclock1030;
    public SpriteRenderer m_darkness;
    public float m_fMaxDarkness = 0.7f; //Alpha component of the black panel infront of camera come nighttime. Between 0 and 1

    public GameObject m_allTreasuresFoundMessage;

    private TarodevController.PlayerController m_playerControllerLand;
    private TarodevController.PlayerControllerWater m_playerControllerWater;

    private int m_iScore = 0;
    private GameObject[] m_treasures;

    private float m_fTimer = 0;

    private void Start()
    {
        //Initialise two playercontrollers
        m_playerControllerLand = m_player.GetComponent<TarodevController.PlayerController>();
        m_playerControllerWater = m_player.GetComponent<TarodevController.PlayerControllerWater>();

        //Assign 'this' to each treasure in scene
        m_treasures = GameObject.FindGameObjectsWithTag("Treasure");
        foreach (GameObject treasure in m_treasures)
        {
            treasure.GetComponent<CCollectible>().m_gameManager = this;
        }
    }

    void Update()
    {
        ////////////////////////DEBUG ONLY////////////////////////////////
        //Check for restarting
        if (Input.GetKeyUp(KeyCode.R))
        {
            RestartPosition();
            m_playerControllerLand.enabled = true;
            m_playerControllerWater.enabled = false;
            RestartTime();
        }
        ////////////////////////DEBUG ONLY////////////////////////////////

        //Update clock
        m_fTimer += Time.deltaTime;
        UpdateClock();
        DarkenScene();
    }

    public int GetScore()
    {
        return m_iScore;
    }
    public void IncreaseScore(int iInc = 1)
    {
        m_iScore += iInc;
        m_scoreText.SetText("Treasures Found: " + m_iScore);
        if (m_iScore >= m_treasures.Length)
        {
            DisplayAllTreasuresFoundMessage();
        }
    }

    public void GoToBed()
    {
        //1. Turn off controls
        m_playerControllerLand.enabled = false;
        //2. Turn player to face door
        //3. Fade scene to black
        //4. Restart time
        RestartTime();
        //5. Put player back in starting position
        RestartPosition();
        //6. Turn controls back on
        m_playerControllerLand.enabled = true;
    }

    public void TalkToDad()
    {
        Debug.Log("Talking to dad");
    }

    /// /////////////////////////PRIVATES////////////////////////////////

    private void RestartTime()
    {
        m_fTimer = 0;
        m_darkness.color = new Color(0, 0, 0, 0);
        m_clockImage.sprite = m_oclock12;
    }

    private void RestartPosition()
    {
        //m_player.transform.position = new Vector3(0.64f, -1.36f, 0); //On the jetty
        m_player.transform.position = new Vector3(-10.2f, -1.559f, 0); //Next to dad
    }

    private void UpdateClock()
    {
        if (m_fTimer > m_fDayLength * 8 / 8)
        {
            Debug.Log("Nighttime");
            m_clockImage.sprite = m_oclock12;
        }
        else if (m_fTimer > m_fDayLength * 7 / 8)
        {
            m_clockImage.sprite = m_oclock1030;
        }
        else if (m_fTimer > m_fDayLength * 6 / 8)
        {
            m_clockImage.sprite = m_oclock9;
        }
        else if (m_fTimer > m_fDayLength * 5 / 8)
        {
            m_clockImage.sprite = m_oclock730;
        }
        else if (m_fTimer > m_fDayLength * 4 / 8)
        {
            m_clockImage.sprite = m_oclock6;
        }
        else if (m_fTimer > m_fDayLength * 3 / 8)
        {
            m_clockImage.sprite = m_oclock430;
        }
        else if (m_fTimer > m_fDayLength * 2 / 8)
        {
            m_clockImage.sprite = m_oclock3;
        }
        else if (m_fTimer > m_fDayLength * 1 / 8)
        {
            m_clockImage.sprite = m_oclock130;
        }
    }

    private void DarkenScene()
    {
        //Make it darker in the last half of the day
        float fFraction = 0.5f;
        if (m_fDayLength * fFraction < m_fTimer && m_fTimer < m_fDayLength)
        {
            float fAlpha = m_fMaxDarkness * (m_fTimer - m_fDayLength * fFraction) / (m_fDayLength * (1 - fFraction));
            m_darkness.color = new Color(0, 0, 0, fAlpha);
        }
    }

    private void DisplayAllTreasuresFoundMessage()
    {
        GameObject messagePanel = Instantiate(m_allTreasuresFoundMessage);
        messagePanel.transform.SetParent(m_canvas.transform);
        messagePanel.transform.localPosition = new Vector3(0, -208, 0);
        messagePanel.transform.localScale = new Vector3(1, 1, 1);

        string sMsg = "You found the last treasure! Take it back to Seaside Joe and save the shop!";
        messagePanel.GetComponent<CButtonFadeDisplay>().Display(sMsg);
    }
}
