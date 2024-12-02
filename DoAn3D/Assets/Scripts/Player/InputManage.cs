using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManage
{
    public void create (){}

    public float onTouchMoveScreen => Input.GetAxis("Mouse X");

    public Vector3 isMoveTouch(InputAction.CallbackContext context) => context.ReadValue<Vector3>();
    public bool isJumpPressed => Input.GetKeyDown(KeyCode.Space);
    public bool isNearAttackPressed => Input.GetKeyDown(KeyCode.Mouse0);
    public bool isFarAttackPressed => Input.GetKeyDown(KeyCode.Mouse1);
    
}
