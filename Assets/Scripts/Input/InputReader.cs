﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName ="Input Reader",menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, InputController.IPlayerActions
{

    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction LeftClick = delegate { };
    public event UnityAction LeftReleaseEvent = delegate { };
    public event UnityAction RightClick = delegate { };
    public event UnityAction RightReleaseEvent = delegate { };

    private InputController gameInput;
    private Vector2 mousePosition;

    public Vector2 MousePosition
    {
        get
        {
            return mousePosition;
        }
    }
    public void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new InputController();
            gameInput.Player.SetCallbacks(this);
        }
        EnablePlayerInput();
    }

    public void EnablePlayerInput()
    {
        gameInput.Player.Enable();
    }
    public void EnableDialogueInput()
    {
        gameInput.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent.Invoke();
        }
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            LeftClick.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            LeftReleaseEvent.Invoke();
        }
    }
        public void OnDrag(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                RightClick.Invoke();
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                RightReleaseEvent.Invoke();
            }
        }


        

}
