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
    float currentSpeedX = 0;
    float currentSpeedY = 0;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 4.5f;
    public float gravity = 10f;
    
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    
    public bool canMove = true, runPressed = false, spacebarPressed = false, leftMousePressed = false;

    public List<Animator> weaponAnimatorList;
        
    CharacterController characterController;

    private PlayerInput m_PlayerInput;
    private InputAction m_MoveAction;
    private InputAction m_SprintAction;
    private InputAction m_JumpAction;
    private InputAction m_UseWeaponAction;
    private InputAction m_EquipWeapon01Action;
    private InputAction m_EquipWeapon02Action;
    private InputAction m_EquipWeapon03Action;
    private InputAction m_ReloadWeaponAction;

    IWeaponUsage weaponUsage;

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
        m_SprintAction = m_PlayerInput.actions["Sprint"];
        m_JumpAction = m_PlayerInput.actions["Jump"];
        m_UseWeaponAction = m_PlayerInput.actions["UseWeapon"];
        m_ReloadWeaponAction = m_PlayerInput.actions["ReloadWeapon"];
        m_EquipWeapon01Action = m_PlayerInput.actions["EquipWeapon01"];
        m_EquipWeapon02Action = m_PlayerInput.actions["EquipWeapon02"];
        m_EquipWeapon03Action = m_PlayerInput.actions["EquipWeapon03"];

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

        playerInputActions.Player.Sprint.performed += ctx => runPressed = ctx.ReadValueAsButton();

        playerInputActions.Player.Jump.performed += ctx => spacebarPressed = ctx.ReadValueAsButton();

        playerInputActions.Player.Jump.canceled += ctx => spacebarPressed = ctx.ReadValueAsButton();

        playerInputActions.Player.UseWeapon.started += ctx => m_WeaponSwitching.weaponList[m_WeaponSwitching.GetCurrentWeaponID()].GetComponent<IWeaponUsage>().Use();

        playerInputActions.Player.ReloadWeapon.started += ctx => m_WeaponSwitching.weaponList[m_WeaponSwitching.GetCurrentWeaponID()].GetComponent<IWeaponUsage>().Reload();

        playerInputActions.Player.EquipWeapon01.started += ctx => m_WeaponSwitching.EnableWeapon(0);

        playerInputActions.Player.EquipWeapon02.started += ctx => m_WeaponSwitching.EnableWeapon(1);

        playerInputActions.Player.EquipWeapon03.started += ctx => m_WeaponSwitching.EnableWeapon(2);
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
       
        if (canMove)
        {
            if (runPressed)
            {
                currentSpeedX = runSpeed * currentMovement.y;
                currentSpeedY = runSpeed * currentMovement.x;
            }
            else
            {
                currentSpeedX = walkSpeed * currentMovement.y;
                currentSpeedY = walkSpeed * currentMovement.x;
            }

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);
        
        if (spacebarPressed && canMove && characterController.isGrounded)
            moveDirection.y = jumpPower;
        else
            moveDirection.y = movementDirectionY;
        
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        
        characterController.Move(moveDirection * Time.deltaTime);

        if (movementPressed)
            weaponAnimatorList[m_WeaponSwitching.GetCurrentWeaponID()].SetBool("isWalking", true);

        if (!movementPressed)
        {
            if(weaponAnimatorList[m_WeaponSwitching.GetCurrentWeaponID()].gameObject.activeSelf)
                weaponAnimatorList[m_WeaponSwitching.GetCurrentWeaponID()].SetBool("isWalking", false);
        }
    }
}