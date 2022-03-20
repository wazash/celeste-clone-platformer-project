using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    // Create getter 
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }

    [SerializeField, Tooltip("Time, how long input will 'hold' true in jump input value after press jump button")]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    // Getting movement vector2 value from Input System
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        // Normalized 'X' value
        NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        // Normalized 'Y' value
        NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    // Getting jump button value from Input System
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }

    /// <summary>
    /// Make jump input used (set to flase)
    /// </summary>
    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
