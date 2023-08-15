using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CJumpButton : Button
{
    private CPhoneControls phoneControls;

    protected override void Start()
    {
        base.Start();
        phoneControls = GameObject.FindWithTag("Player").GetComponent<CPhoneControls>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (phoneControls != null)
        {
            phoneControls.m_bJumpDown = true;
        }
        else
        {
            Debug.Log("CJumpButton couldn't find CPhoneControls in \"Player\" gameobject");
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (phoneControls != null)
        {
            phoneControls.m_bJumpUp = true;
        }
        else
        {
            Debug.Log("CJumpButton couldn't find CPhoneControls in \"Player\" gameobject");
        }
    }
}