using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CDialogue 
{
    [TextArea(3, 10)]
    public string[] m_aSentences;
    public string m_sName;
}
