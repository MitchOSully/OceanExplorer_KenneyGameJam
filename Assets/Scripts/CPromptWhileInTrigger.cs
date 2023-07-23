using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CPromptWhileInTrigger : MonoBehaviour
{
    public GameObject m_prompt;
    public UnityEvent m_action;
    [System.NonSerialized]
    public bool m_bDialogueActive = false;

    private bool m_bInsideTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_bInsideTrigger = true;
            m_prompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_bInsideTrigger = false;
            m_prompt.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_bInsideTrigger = true; //Just in case
        }
    }

    private void Update()
    {
        if (m_bInsideTrigger && !m_bDialogueActive && Input.GetKeyUp(KeyCode.E))
        {
            m_action.Invoke();
        }

        if (m_bDialogueActive)
        {
            m_prompt.SetActive(false);
        }
        else if (m_bInsideTrigger)
        {
            m_prompt.SetActive(true);
        }
    }
}
