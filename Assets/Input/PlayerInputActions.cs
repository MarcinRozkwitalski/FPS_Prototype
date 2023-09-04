//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3830f5bc-1911-4325-aca5-532b9f062216"",
            ""actions"": [
                {
                    ""name"": ""EquipWeapon01"",
                    ""type"": ""Button"",
                    ""id"": ""9a141f77-462b-4767-9249-8e132bba3796"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipWeapon02"",
                    ""type"": ""Button"",
                    ""id"": ""fca580e8-56ec-4844-9b0c-24609d6e089e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipWeapon03"",
                    ""type"": ""Button"",
                    ""id"": ""94a210fb-1d60-49f4-9907-f63fe0958872"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c1cab25a-94d9-40b1-a596-c8bdf47e231e"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""EquipWeapon01"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf8b6b49-dcdb-4bb9-8cdd-d0b590251312"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""EquipWeapon03"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6cba518-531e-4e37-8cb6-5d652390dc12"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""EquipWeapon02"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_EquipWeapon01 = m_Player.FindAction("EquipWeapon01", throwIfNotFound: true);
        m_Player_EquipWeapon02 = m_Player.FindAction("EquipWeapon02", throwIfNotFound: true);
        m_Player_EquipWeapon03 = m_Player.FindAction("EquipWeapon03", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_EquipWeapon01;
    private readonly InputAction m_Player_EquipWeapon02;
    private readonly InputAction m_Player_EquipWeapon03;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @EquipWeapon01 => m_Wrapper.m_Player_EquipWeapon01;
        public InputAction @EquipWeapon02 => m_Wrapper.m_Player_EquipWeapon02;
        public InputAction @EquipWeapon03 => m_Wrapper.m_Player_EquipWeapon03;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @EquipWeapon01.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon01;
                @EquipWeapon01.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon01;
                @EquipWeapon01.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon01;
                @EquipWeapon02.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon02;
                @EquipWeapon02.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon02;
                @EquipWeapon02.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon02;
                @EquipWeapon03.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon03;
                @EquipWeapon03.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon03;
                @EquipWeapon03.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon03;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EquipWeapon01.started += instance.OnEquipWeapon01;
                @EquipWeapon01.performed += instance.OnEquipWeapon01;
                @EquipWeapon01.canceled += instance.OnEquipWeapon01;
                @EquipWeapon02.started += instance.OnEquipWeapon02;
                @EquipWeapon02.performed += instance.OnEquipWeapon02;
                @EquipWeapon02.canceled += instance.OnEquipWeapon02;
                @EquipWeapon03.started += instance.OnEquipWeapon03;
                @EquipWeapon03.performed += instance.OnEquipWeapon03;
                @EquipWeapon03.canceled += instance.OnEquipWeapon03;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnEquipWeapon01(InputAction.CallbackContext context);
        void OnEquipWeapon02(InputAction.CallbackContext context);
        void OnEquipWeapon03(InputAction.CallbackContext context);
    }
}
