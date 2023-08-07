using UnityEngine;
using UnityEngine.InputSystem;

public class CPhoneControls : MonoBehaviour
{
    public Vector2 m_moveVector = Vector2.zero;

    private InputActions m_input = null;
    
    private void Awake()
    {
        m_input = new InputActions();
    }

    private void OnEnable()
    {
        m_input.Enable();
        m_input.Player.Movement.performed += OnMovementPerformed;
        m_input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        m_input.Disable();
        m_input.Player.Movement.performed -= OnMovementPerformed;
        m_input.Player.Movement.canceled -= OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        m_moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        m_moveVector = Vector2.zero;
    }
}
