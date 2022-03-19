using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    // Create getter 
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }

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

    }
}
