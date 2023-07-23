using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDialogueTrigger : MonoBehaviour
{
    public CDialogue m_dialogue;

    public void TriggerDialog()
    {
        FindObjectOfType<CGameManager>().TalkToDad(m_dialogue);
    }
}
