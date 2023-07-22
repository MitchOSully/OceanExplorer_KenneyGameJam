using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShowPromptWhileInTrigger : MonoBehaviour
{
    public GameObject m_prompt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_prompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_prompt.SetActive(false);
        }
    }
}
