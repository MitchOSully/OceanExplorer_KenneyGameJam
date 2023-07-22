using UnityEngine;

public class CCameraFollow : MonoBehaviour
{
    public Transform m_targetTransform;
    public float m_fSmoothSpeed = 0.125f; //(0-1) Higher this value is, the faster the camera will lock onto target
    public Vector3 m_offset = new Vector3(0, 0, -1);
    public float m_fMaxHeight = -1.507f;
    public float m_fLeftLimit = -8f;

    private Vector3 m_velocity = Vector3.zero;

    //Run right after Update(), after target has done all its movement
    private void LateUpdate()
    {
        Vector3 desiredPosition = m_targetTransform.position + m_offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref m_velocity, m_fSmoothSpeed);

        float fNewX = smoothedPosition.x > m_fLeftLimit ? smoothedPosition.x : m_fLeftLimit;
        float fNewY = smoothedPosition.y < m_fMaxHeight ? smoothedPosition.y : m_fMaxHeight;

        transform.position = new Vector3(fNewX, fNewY, smoothedPosition.z);
        //NOTE to self: For some reason, calling transform.position.Set(x, y, z) wasn't changing the value of transform.position,
        //so I'm reduced to doing a heap allocation every frame :(. Oh well, this game jam isn't about performance
    }
}
