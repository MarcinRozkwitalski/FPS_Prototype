using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput m_PlayerInput;
    
    private InputAction m_EquipWeapon01;
    private InputAction m_EquipWeapon02;
    private InputAction m_EquipWeapon03;

    [SerializeField]
    private WeaponSwitching m_WeaponSwitching;

    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_EquipWeapon01 = m_PlayerInput.actions["EquipWeapon01"];
        m_EquipWeapon02 = m_PlayerInput.actions["EquipWeapon02"];
        m_EquipWeapon03 = m_PlayerInput.actions["EquipWeapon03"];

        PlayerInputActions playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();

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

    private void Start()
    {
        // isEquippingWeapon = 
    }
}