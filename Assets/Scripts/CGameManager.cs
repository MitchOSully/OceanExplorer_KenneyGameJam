using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Pathfinding;

public class CGameManager : MonoBehaviour
{
    public GameObject m_player;
    public GameObject m_canvas;
    public Sprite m_alivePlayerSprite, m_deadPlayerSprite;

    public TextMeshProUGUI m_scoreText;

    public Image m_clockImage;
    public float m_fDayLength = 60; //60 seconds = 1 day in-game
    public Sprite m_oclock12, m_oclock130, m_oclock3, m_oclock430, m_oclock6, m_oclock730, m_oclock9, m_oclock1030;
    public SpriteRenderer m_darkness;
    public float m_fMaxDarkness = 0.7f; //Alpha component of the black panel infront of camera come nighttime. Between 0 and 1
    
    public GameObject m_skryperPrefab;

    public GameObject m_gameOverPanel;

    public GameObject m_allTreasuresFoundMessage;

    /// ////////////////PRIVATES//////////////////////////

    private TarodevController.PlayerController m_playerControllerLand;
    private TarodevController.PlayerControllerWater m_playerControllerWater;

    private int m_iScore = 0;
    private GameObject[] m_treasures;

    private float m_fTimer = 0;
    private bool m_bGoingToBed = false;

    private GameObject[] m_skrypers;
    private bool m_bSkrypersActive = false;

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
            RespawnPlayer();
            m_playerControllerLand.enabled = true;
            m_playerControllerWater.enabled = false;
            RestartTime();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            SpawnSkrypers(10, 10);
        }
        ////////////////////////DEBUG ONLY////////////////////////////////

        //Update clock
        m_fTimer += Time.deltaTime;
        UpdateClock();
        DarkenScene();
        if (m_fTimer > m_fDayLength && !m_bSkrypersActive)
        {
            SpawnSkrypers(10, 10);
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
        if (m_iScore >= m_treasures.Length)
        {
            DisplayAllTreasuresFoundMessage();
        }
    }

    public void GoToBed()
    {
        m_bGoingToBed = true;
        StartCoroutine(GoToBed(1, 0.5f, 0.25f));
    }

    public void TalkToDad()
    {
        Debug.Log("Talking to dad");
    }

    public void KillPlayer()
    {
        m_player.GetComponent<SpriteRenderer>().sprite = m_deadPlayerSprite;
        m_player.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        m_playerControllerLand.enabled = false;
        m_playerControllerWater.enabled = false;

        m_gameOverPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// /////////////////////////PRIVATES////////////////////////////////

    private IEnumerator GoToBed(float fFadeOutTime, float fBlackTime, float fFadeInTime)
    {
        //1. Turn off controls
        m_playerControllerLand.enabled = false;
        //2. Turn player to face door
        //3. Fade scene to black
        Color OGColor = m_darkness.color;
        for (float t = 0; t < fFadeOutTime; t += Time.deltaTime)
        {
            m_darkness.color = Color.Lerp(OGColor, Color.black, Mathf.Min(1, t/fFadeOutTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //4. Stay black for a little while
        for (float t = 0; t < fBlackTime; t += Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //5. Destroy enemies
        DestroySkrypers();
        //6. Restart time
        RestartTime();
        //7. Put player back in starting position
        RespawnPlayer();
        //8. Fade scene back in
        OGColor = m_darkness.color;
        for (float t = 0; t < fFadeInTime; t += Time.deltaTime)
        {
            m_darkness.color = Color.Lerp(Color.black, Color.clear, Mathf.Min(1, t/fFadeInTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //9. Turn controls back on
        m_playerControllerLand.enabled = true;

        m_bGoingToBed = false;
    }

    private void RestartTime()
    {
        m_fTimer = 0;
        m_darkness.color = new Color(0, 0, 0, 0);
        m_clockImage.sprite = m_oclock12;
    }

    private void RespawnPlayer()
    {
        //m_player.transform.position = new Vector3(0.64f, -1.36f, 0); //On the jetty
        m_player.transform.position = new Vector3(-10.2f, -1.559f, 0); //Next to dad
        m_player.transform.rotation = Quaternion.identity;

        m_player.GetComponent<SpriteRenderer>().sprite = m_alivePlayerSprite;
        m_player.GetComponent<Rigidbody2D>().gravityScale = 0;
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
        if (!m_bGoingToBed)
        {
            //Make it darker in the last half of the day
            float fFraction = 0.5f;
            if (m_fDayLength * fFraction < m_fTimer && m_fTimer < m_fDayLength)
            {
                float fAlpha = m_fMaxDarkness * (m_fTimer - m_fDayLength * fFraction) / (m_fDayLength * (1 - fFraction));
                m_darkness.color = new Color(0, 0, 0, fAlpha);
            }
        }
    }

    private void SpawnSkrypers(int iNumToSpawn, float fSpawnRadius)
    {
        m_skrypers = new GameObject[iNumToSpawn];

        const int MAX_NUM_TRIES = 1000;
        int iNumSpawned = 0, iNumTries = 0;
        while (iNumSpawned < iNumToSpawn && iNumTries < MAX_NUM_TRIES)
        {
            Vector3 spawnPoint = m_player.transform.position + Random.insideUnitSphere * fSpawnRadius;
            if (!Physics.CheckSphere(spawnPoint, 1) && spawnPoint.y < -3)
            {
                m_skrypers[iNumSpawned] = Instantiate(m_skryperPrefab, spawnPoint, Quaternion.identity);
                m_skrypers[iNumSpawned].GetComponent<CKillPlayerOnTrigger>().m_gameManager = this;
                m_skrypers[iNumSpawned].GetComponent<AIDestinationSetter>().target = m_player.transform;
                iNumSpawned++;
            }
            iNumTries++;
        }

        //if (iNumSpawned > 0)
        //{
            m_bSkrypersActive = true;
        //}
    }

    private void DestroySkrypers()
    {
        foreach (GameObject skryper in m_skrypers)
        {
            if (skryper != null)
            {
                Destroy(skryper);
            }
        }
        m_skrypers = new GameObject[0];
        
        m_bSkrypersActive = false;
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
