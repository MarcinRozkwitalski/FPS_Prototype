using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private WeaponSwitching m_WeaponSwitching;

    public Camera playerCamera;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 4.5f;
    public float gravity = 10f;
    
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    
    public bool canMove = true;

    public List<Animator> weaponAnimatorList;
        
    CharacterController characterController;

    private PlayerInput m_PlayerInput;
    private InputAction m_MoveAction;
    private InputAction m_EquipWeapon01;
    private InputAction m_EquipWeapon02;
    private InputAction m_EquipWeapon03;

    Vector2 currentMovement;
    bool movementPressed;

    void Start()
    {
        weaponAnimatorList = m_WeaponSwitching.weaponAnimatorList;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_MoveAction = m_PlayerInput.actions["Movement"];
        m_EquipWeapon01 = m_PlayerInput.actions["EquipWeapon01"];
        m_EquipWeapon02 = m_PlayerInput.actions["EquipWeapon02"];
        m_EquipWeapon03 = m_PlayerInput.actions["EquipWeapon03"];

        PlayerInputActions playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Movement.started += ctx => 
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };

        playerInputActions.Player.Movement.performed += ctx => 
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        
        playerInputActions.Player.Movement.canceled += ctx => 
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = false;
        };

        playerInputActions.Player.EquipWeapon01.started += ctx =>
        {
            m_WeaponSwitching.EnableWeapon(0);
        };

        playerInputActions.Player.EquipWeapon02.started += ctx =>
        {
            m_WeaponSwitching.EnableWeapon(1);
        };

        playerInputActions.Player.EquipWeapon03.started += ctx =>
        {
            m_WeaponSwitching.EnableWeapon(2);
        };
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * currentMovement.y : 0;
        float currentSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * currentMovement.x : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);
        
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            moveDirection.y = jumpPower;
        else
            moveDirection.y = movementDirectionY;
        
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        
        characterController.Move(moveDirection * Time.deltaTime);
        
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (movementPressed)
            weaponAnimatorList[m_WeaponSwitching.GetCurrentWeaponID()].SetBool("isWalking", true);

        if (!movementPressed)
        {
            if(weaponAnimatorList[m_WeaponSwitching.GetCurrentWeaponID()].gameObject.activeSelf)
                weaponAnimatorList[m_WeaponSwitching.GetCurrentWeaponID()].SetBool("isWalking", false);
        }
    }
}