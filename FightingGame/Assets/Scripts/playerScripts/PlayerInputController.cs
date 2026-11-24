using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 moveValue;
    public Vector2 lookValue;
    public void moveEvent(InputAction.CallbackContext callBack)
    {
        lookValue = callBack.ReadValue<Vector2>();
    }
    public void LookEvent(InputAction.CallbackContext callBack)
    {
        moveValue = callBack.ReadValue<Vector2>();
    }

    public void interactEvent()
    {
        Debug.Log("Player interact");
    }

    public void dodgeEvent()
    {
        Debug.Log("Player dodge");
    }

    public void attackEvent()
    {
        Debug.Log("Player attack");
    }
}
