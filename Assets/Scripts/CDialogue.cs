using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CDialogue 
{
    public string m_sName;
    [TextArea(3, 10)]
    public string[] m_aSentences;
}
