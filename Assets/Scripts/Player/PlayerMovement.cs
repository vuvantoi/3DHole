using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSmoothness = 0.15f;

    private Vector3 moveDirection = Vector3.forward; // Hướng ban đầu

    private void Update()
    {
        UpdateDirectionFromInput();
        MovePlayer();
        //RotatePlayer();
    }

    private void UpdateDirectionFromInput()
    {
        Vector2 input = InputManager.Instance.MoveInput;

        if (input.sqrMagnitude > 0.01f) // Nếu có input
        {
            moveDirection = new Vector3(input.x, 0f, input.y).normalized;
        }
        // Nếu không input → giữ nguyên moveDirection hiện tại
    }
    private void MovePlayer()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void RotatePlayer() // xoay hướng của player
    {
        if (moveDirection.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness);
    }
}
