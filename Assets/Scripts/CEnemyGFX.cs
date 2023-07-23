using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CEnemyGFX : MonoBehaviour
{
    public AIPath m_aiPath;

    void Update()
    {
        Vector3 aiVelocity = m_aiPath.desiredVelocity;

        if (aiVelocity.x >= 0.01f) //moving to right
        {
            //Flip
            transform.localScale = new Vector3(-1f, 1f, 1f);

            //Rotate
            float fZAngle = Vector3.Angle(Vector3.right, aiVelocity);
            if (aiVelocity.y < 0) //negative
            {
                fZAngle = -fZAngle; //Flip sign
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, fZAngle);
        }
        else if (aiVelocity.x <= -0.01f) //moving left
        {
            //Flip
            transform.localScale = new Vector3(1f, 1f, 1f);
            
            //Rotate
            float fZAngle = Vector3.Angle(Vector3.left, aiVelocity);
            if (aiVelocity.y > 0) //positive
            {
                fZAngle = -fZAngle; //Flip sign
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, fZAngle);
        }
    }
}
