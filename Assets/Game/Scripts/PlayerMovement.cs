using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private bool isButtonPressed;

    private Vector2 moveInput;

    private void Start()
    {
        if (!characterController)
            characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!isButtonPressed)
            moveInput = move.action.ReadValue<Vector2>();

        Vector3 moveVector = new Vector3(moveInput.x, 0f, 0f);
        characterController.Move(moveVector * movementSpeed * Time.deltaTime);
    }

    public void MoveLeftDown()
    {
        isButtonPressed = true;
        moveInput.x = -1f;
    }

    public void MoveRightDown()
    {
        isButtonPressed = true;
        moveInput.x = 1f;
    }

    public void MoveStop()
    {
        isButtonPressed = false;
        moveInput.x = 0f;
    }
}
