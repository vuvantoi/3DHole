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
        RotatePlayer();
    }

    /// <summary>
    /// Lấy hướng từ joystick, nếu không input thì giữ hướng cũ
    /// </summary>
    private void UpdateDirectionFromInput()
    {
        Vector2 input = InputManager.Instance.MoveInput;

        if (input.sqrMagnitude > 0.01f) // Nếu có input
        {
            moveDirection = new Vector3(input.x, 0f, input.y).normalized;
        }
        // Nếu không input → giữ nguyên moveDirection hiện tại
    }

    /// <summary>
    /// Player luôn di chuyển về hướng moveDirection
    /// </summary>
    private void MovePlayer()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Xoay player theo hướng moveDirection
    /// </summary>
    private void RotatePlayer()
    {
        if (moveDirection.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness);
    }
}
