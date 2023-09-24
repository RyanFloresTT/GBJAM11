using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static Action OnAPressed;
    public static Action OnBPressed;
    public static Action OnStartPressed;
    public static Action OnSelectPressed;

    private static PlayerInputActions playerInput;

    void Awake() {
        playerInput = new();
    }
    private void OnEnable() {
        playerInput.Player.Enable();

        playerInput.Player.A.performed += Handle_A_Pressed;
        playerInput.Player.B.performed += Handle_B_Pressed;
        playerInput.Player.Start.performed += Handle_Start_Pressed;
        playerInput.Player.Select.performed += Handle_Select_Pressed;
    }

    private void Start() {
        Player.OnPlayerDeath += Handle_PlayerDeath;
    }

    private void Handle_PlayerDeath() {
        DisableMovementInput();
    }

    private void Handle_A_Pressed(InputAction.CallbackContext obj) {
        OnAPressed?.Invoke();
    }
    private void Handle_B_Pressed(InputAction.CallbackContext obj)
    {
        OnBPressed?.Invoke();
    }
    private void Handle_Start_Pressed(InputAction.CallbackContext obj)
    {
        OnBPressed?.Invoke();
    }
    private void Handle_Select_Pressed(InputAction.CallbackContext obj)
    {
        OnBPressed?.Invoke();
    }
    private void OnDisable() {
        playerInput.Player.Disable();
    }

    public static void DisableMovementInput() {
        playerInput.Player.Analog.Disable();
    }

    public static void EnableMovementInput() {
        playerInput.Player.Analog.Enable();

    }

    public static Vector2 GetAnalogVectorNormalized() => playerInput.Player.Analog.ReadValue<Vector2>().normalized;
    public static Vector2 GetAnalogVector() => playerInput.Player.Analog.ReadValue<Vector2>();
}
