// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""PlayerGrounded"",
            ""id"": ""d2001e69-bb4f-4fc1-99df-5a78bd5cfefa"",
            ""actions"": [
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Value"",
                    ""id"": ""927d4de5-6498-47c0-95dc-30617ef99557"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Value"",
                    ""id"": ""05ab3830-5ed8-4ea1-8262-5c4fb31cd38f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpaceToggle"",
                    ""type"": ""Button"",
                    ""id"": ""8f99510c-9a69-4209-992d-2f056371cc10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Evade"",
                    ""type"": ""Button"",
                    ""id"": ""736e9071-5358-4274-ba0b-91315110731e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ShotGrappling"",
                    ""type"": ""Button"",
                    ""id"": ""80b118b0-3fd7-495b-810a-9115faa7fae6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""StopGrappling"",
                    ""type"": ""Button"",
                    ""id"": ""c56c5192-3d46-404e-a101-eb5d407825d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""08c21490-9f0c-4ba9-88ad-a06eb25536ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""5231bcc5-4b54-47ef-b40c-84919315cb58"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Right"",
                    ""id"": ""4bcafa20-fe8d-4c73-a8f8-a57367a7b900"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b942110a-5f6a-4632-9ad0-e7e537a9b67b"",
                    ""path"": ""<Keyboard>/#(Q)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_K/M"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""11c60e24-cf7f-4c7a-b9d8-af009d6a8825"",
                    ""path"": ""<Keyboard>/#(D)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_K/M"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Forward"",
                    ""id"": ""459eeb60-d505-4045-beb5-3fc97dc40574"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2932e816-dc5d-4959-a570-a102138bef21"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e6f35694-4815-4938-98a0-02b33c1d98ec"",
                    ""path"": ""<Keyboard>/#(Z)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_K/M"",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""48c02ba0-dfd8-4378-acd2-10f0eca4ce40"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_K/M"",
                    ""action"": ""SpaceToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""248d8cde-1fe2-433d-97d9-f11bf0e5cdc1"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_K/M"",
                    ""action"": ""Evade"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aab9ca4a-5bac-4a2b-a3ec-b93988002c13"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_K/M"",
                    ""action"": ""ShotGrappling"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae93c2a4-88e2-4fbb-a2d2-67fd57c5de0f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Player_K/M"",
                    ""action"": ""StopGrappling"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e01549f-fc8b-4531-abc6-a02d1064d92b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_K/M"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d884234e-c3b3-41eb-8825-912d3dd4dea2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player_K/M"",
            ""bindingGroup"": ""Player_K/M"",
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
        },
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerGrounded
        m_PlayerGrounded = asset.FindActionMap("PlayerGrounded", throwIfNotFound: true);
        m_PlayerGrounded_MoveRight = m_PlayerGrounded.FindAction("MoveRight", throwIfNotFound: true);
        m_PlayerGrounded_MoveForward = m_PlayerGrounded.FindAction("MoveForward", throwIfNotFound: true);
        m_PlayerGrounded_SpaceToggle = m_PlayerGrounded.FindAction("SpaceToggle", throwIfNotFound: true);
        m_PlayerGrounded_Evade = m_PlayerGrounded.FindAction("Evade", throwIfNotFound: true);
        m_PlayerGrounded_ShotGrappling = m_PlayerGrounded.FindAction("ShotGrappling", throwIfNotFound: true);
        m_PlayerGrounded_StopGrappling = m_PlayerGrounded.FindAction("StopGrappling", throwIfNotFound: true);
        m_PlayerGrounded_Pause = m_PlayerGrounded.FindAction("Pause", throwIfNotFound: true);
        m_PlayerGrounded_Attack = m_PlayerGrounded.FindAction("Attack", throwIfNotFound: true);
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

    // PlayerGrounded
    private readonly InputActionMap m_PlayerGrounded;
    private IPlayerGroundedActions m_PlayerGroundedActionsCallbackInterface;
    private readonly InputAction m_PlayerGrounded_MoveRight;
    private readonly InputAction m_PlayerGrounded_MoveForward;
    private readonly InputAction m_PlayerGrounded_SpaceToggle;
    private readonly InputAction m_PlayerGrounded_Evade;
    private readonly InputAction m_PlayerGrounded_ShotGrappling;
    private readonly InputAction m_PlayerGrounded_StopGrappling;
    private readonly InputAction m_PlayerGrounded_Pause;
    private readonly InputAction m_PlayerGrounded_Attack;
    public struct PlayerGroundedActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerGroundedActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveRight => m_Wrapper.m_PlayerGrounded_MoveRight;
        public InputAction @MoveForward => m_Wrapper.m_PlayerGrounded_MoveForward;
        public InputAction @SpaceToggle => m_Wrapper.m_PlayerGrounded_SpaceToggle;
        public InputAction @Evade => m_Wrapper.m_PlayerGrounded_Evade;
        public InputAction @ShotGrappling => m_Wrapper.m_PlayerGrounded_ShotGrappling;
        public InputAction @StopGrappling => m_Wrapper.m_PlayerGrounded_StopGrappling;
        public InputAction @Pause => m_Wrapper.m_PlayerGrounded_Pause;
        public InputAction @Attack => m_Wrapper.m_PlayerGrounded_Attack;
        public InputActionMap Get() { return m_Wrapper.m_PlayerGrounded; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerGroundedActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerGroundedActions instance)
        {
            if (m_Wrapper.m_PlayerGroundedActionsCallbackInterface != null)
            {
                @MoveRight.started -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnMoveRight;
                @MoveForward.started -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnMoveForward;
                @MoveForward.performed -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnMoveForward;
                @MoveForward.canceled -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnMoveForward;
                @SpaceToggle.started -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnSpaceToggle;
                @SpaceToggle.performed -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnSpaceToggle;
                @SpaceToggle.canceled -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnSpaceToggle;
                @Evade.started -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnEvade;
                @Evade.performed -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnEvade;
                @Evade.canceled -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnEvade;
                @ShotGrappling.started -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnShotGrappling;
                @ShotGrappling.performed -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnShotGrappling;
                @ShotGrappling.canceled -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnShotGrappling;
                @StopGrappling.started -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnStopGrappling;
                @StopGrappling.performed -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnStopGrappling;
                @StopGrappling.canceled -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnStopGrappling;
                @Pause.started -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnPause;
                @Attack.started -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerGroundedActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_PlayerGroundedActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveForward.started += instance.OnMoveForward;
                @MoveForward.performed += instance.OnMoveForward;
                @MoveForward.canceled += instance.OnMoveForward;
                @SpaceToggle.started += instance.OnSpaceToggle;
                @SpaceToggle.performed += instance.OnSpaceToggle;
                @SpaceToggle.canceled += instance.OnSpaceToggle;
                @Evade.started += instance.OnEvade;
                @Evade.performed += instance.OnEvade;
                @Evade.canceled += instance.OnEvade;
                @ShotGrappling.started += instance.OnShotGrappling;
                @ShotGrappling.performed += instance.OnShotGrappling;
                @ShotGrappling.canceled += instance.OnShotGrappling;
                @StopGrappling.started += instance.OnStopGrappling;
                @StopGrappling.performed += instance.OnStopGrappling;
                @StopGrappling.canceled += instance.OnStopGrappling;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public PlayerGroundedActions @PlayerGrounded => new PlayerGroundedActions(this);
    private int m_Player_KMSchemeIndex = -1;
    public InputControlScheme Player_KMScheme
    {
        get
        {
            if (m_Player_KMSchemeIndex == -1) m_Player_KMSchemeIndex = asset.FindControlSchemeIndex("Player_K/M");
            return asset.controlSchemes[m_Player_KMSchemeIndex];
        }
    }
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IPlayerGroundedActions
    {
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveForward(InputAction.CallbackContext context);
        void OnSpaceToggle(InputAction.CallbackContext context);
        void OnEvade(InputAction.CallbackContext context);
        void OnShotGrappling(InputAction.CallbackContext context);
        void OnStopGrappling(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
}
