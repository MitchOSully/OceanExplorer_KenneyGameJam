using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CDialogueManager : MonoBehaviour
{
    public CGameManager m_gameManager;
    public GameObject m_dialogueBoxPanel;
    public CPromptWhileInTrigger m_theOnlyPromptTrigger; //For scalability, this should be an array of triggers in the future
    public TextMeshProUGUI m_nameText, m_dialogueText;
    public Animator animator;

    //[System.NonSerialized]
    public bool m_bDialogueActive = false;

    private Queue<string> m_qSentences;

    void Start()
    {
        m_qSentences = new Queue<string>();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Exiting conversation");
        }
        if (m_bDialogueActive && RightArrowUp())
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(CDialogue dialogue)
    {
        m_bDialogueActive = true;
        m_theOnlyPromptTrigger.m_bDialogueActive = true;

        animator.SetBool("IsOpen", true);

        m_nameText.SetText(dialogue.m_sName);

        m_qSentences.Clear();
        foreach (string sSentence in dialogue.m_aSentences)
        {
            m_qSentences.Enqueue(sSentence);
        }

        m_dialogueBoxPanel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (m_qSentences.Count == 0) //No more sentences
        {
            EndDialogue();
        }
        else
        {
            string sSentence = m_qSentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sSentence));
        }
    }

    IEnumerator TypeSentence(string sSentence)
    {
        m_dialogueText.SetText("");
        foreach(char cLetter in sSentence.ToCharArray())
        {
            m_dialogueText.text += cLetter;
            yield return new WaitForSeconds(5*Time.deltaTime);
        }
    }

    private void EndDialogue()
    {
        m_bDialogueActive = false;
        m_theOnlyPromptTrigger.m_bDialogueActive = false;
        
        animator.SetBool("IsOpen", false);

        m_dialogueBoxPanel.SetActive(false);
        m_gameManager.FinishTalking();
    }

    private bool RightArrowUp()
    {
        return Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D);
    }
}
