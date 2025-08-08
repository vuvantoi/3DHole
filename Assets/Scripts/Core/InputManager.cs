using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseSingleton<InputManager>
{
    [SerializeField] private Joystick joystick;
    public Vector2 MoveInput { get; private set; }

    private void Update()
    {
        // Lấy từ joystick
        Vector2 mobileInput = GetMobileInput();

        // Lấy từ bàn phím
        Vector2 pcInput = GetPCInput();

        // Cộng hai loại input lại
        MoveInput = (mobileInput + pcInput);

        // Giới hạn độ dài vector để không vượt quá 1
        if (MoveInput.magnitude > 1f)
            MoveInput = MoveInput.normalized;
    }

    private Vector2 GetMobileInput()
    {
        if (joystick == null) return Vector2.zero;
        return new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    private Vector2 GetPCInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
