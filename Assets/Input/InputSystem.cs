// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5d27c32c-d0e1-4b9f-9b13-54c811310db6"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""1491de5d-9dae-49e3-9c45-f1cd90c0f2c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""368231fd-0b22-443c-b01f-c0c538ae89f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LookDown"",
                    ""type"": ""Button"",
                    ""id"": ""f4b01114-86a3-4743-9aa1-2c519504daa2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""LookUp"",
                    ""type"": ""Button"",
                    ""id"": ""c8c71750-31d0-42ab-8693-e48cf79c15e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""MeleeAttack"",
                    ""type"": ""Value"",
                    ""id"": ""4fb77cd3-ec33-43f1-8209-a9f33debd968"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RangeAttack"",
                    ""type"": ""Value"",
                    ""id"": ""17b983fc-3047-4e52-ab70-f16afc696a2b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Value"",
                    ""id"": ""0e6cae1d-6059-48fc-a44c-f173ff22d5bd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Quest"",
                    ""type"": ""Value"",
                    ""id"": ""f50a7b09-7e12-4678-84ac-27b55f9c4925"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Value"",
                    ""id"": ""93925e8b-8ae1-4b93-a22c-1dc0c0d987bb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Value"",
                    ""id"": ""db150168-4889-42d0-b5fc-225fd92bfbb3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""78e02ab5-0169-4cf8-983e-730107527d6c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonSelect"",
                    ""type"": ""Value"",
                    ""id"": ""fca3de86-837f-4670-96b6-e36ae9b1f405"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveX"",
                    ""type"": ""Value"",
                    ""id"": ""cc667fd8-4fe4-4da7-a4db-357d6f75d507"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MoveY"",
                    ""type"": ""Value"",
                    ""id"": ""cb97d66d-dbe7-47c5-8b2b-a0e9ed5af474"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Settings"",
                    ""type"": ""Button"",
                    ""id"": ""83a7f75b-1aaa-4f32-8264-f3bc030b6512"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c0ee5b55-c560-4536-9faf-8ac097cb66d4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f4b4ba6-934b-4413-b0f8-fc8a8208ea32"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75f99e3e-d782-47b0-aa0f-4444f5a7815a"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""256ce835-f04c-4574-9a8f-f0c2f23dad65"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""640f7347-4f43-48bb-990c-88499e72867e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""LookDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55bd2af9-1ec1-4391-a098-2ca51647735f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""LookDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dced614-1f29-405e-b370-d4c1492b9dae"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""LookDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""274cbe52-75c3-41b7-bc9f-efd0131245f3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""LookUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8a1875c-b28f-4cef-b504-d53010ecb7b2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""LookUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42d70db3-0744-4d51-8b41-883478c97e09"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""LookUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9acaf5ee-4346-4930-9ea7-d04f33727e9f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90fdaf89-62ff-4644-b379-71f0ab53640b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""120d1fed-e6ae-43cc-a858-cb6e7a3e3e06"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""RangeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46498cd1-e67c-4f8e-bd57-5d7078cbd386"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""RangeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffa6d3fb-0880-4bc4-9ceb-927434f25371"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""258c7674-089c-4f9b-bc50-5432d632b3cb"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65091796-0c6d-4f76-8dc2-0307a0c24f9c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Quest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98dc692f-7e6c-4001-a803-d381951600f9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Quest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5e1a33a-75cd-4834-8d68-d0b73aa92f91"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e50c0045-b0ac-468d-854d-c0b0a80751cb"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3259789e-bc3e-45bf-8bac-f94d21db843f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""724652fb-9b79-464f-8872-2741a58ec3d8"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edbdf5d3-1b9c-4c31-a390-b4a7e20b3a59"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e2291d22-de91-402a-8daa-94aa7b89f555"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c7116411-6754-4b79-838b-eda0ee4b98d2"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""91251366-074f-41fe-b29a-c93a16cc4ecf"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""69c25304-a92b-42eb-9f57-3bad190dd8d9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""10465d18-08a9-4e13-af12-a64f72bdd6ef"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f9e27fa3-dab9-456c-aa04-c0f7558e7d51"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bda0e124-e0c5-4638-8572-20ccac632658"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7c3319bf-8425-461b-a8ef-461da87e12ce"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cc43f94b-a7fb-4ce7-a260-a5217c01c5f1"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bb6a5715-a7f7-4c65-b143-8a60c1ad2e96"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""288a5b8b-670f-4ba8-9bd9-955b2a29a690"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""ButtonSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b878c841-dab0-4be4-9a14-c6909e15e527"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""ButtonSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""607bb46d-58bb-4bd4-b1c0-7d877a892006"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""ButtonSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""c70fab61-c570-4f17-a8b8-0da3e91b85b0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveY"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b5135bc2-51f7-4be8-a3c9-6d68ef6f92ef"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""MoveY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c5c8c03f-ae1d-4799-b2fa-2e72ea6616b6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""MoveY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""efeb8e48-2118-4bb3-9a2c-370fb593a64b"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""MoveY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""1124fc6f-7720-4e2a-b214-c09769f597b1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveX"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0cdcb6f9-3307-4392-a9a5-079fe84d3d4d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""MoveX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4e758807-8d1a-4f6f-b226-4dc4f3a56a69"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""MoveX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""555e1b9d-20d8-4adc-bc71-086008e172d4"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""MoveX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc481125-cb1a-4631-a856-d5354d449eb8"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Settings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eef9653-e8fc-448a-89fb-e70289e8f3e5"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Settings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02fd4527-a440-46ce-ad69-7e5e4bd45e2f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Settings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""All"",
            ""bindingGroup"": ""All"",
            ""devices"": []
        },
        {
            ""name"": ""All1"",
            ""bindingGroup"": ""All"",
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
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_LookDown = m_Player.FindAction("LookDown", throwIfNotFound: true);
        m_Player_LookUp = m_Player.FindAction("LookUp", throwIfNotFound: true);
        m_Player_MeleeAttack = m_Player.FindAction("MeleeAttack", throwIfNotFound: true);
        m_Player_RangeAttack = m_Player.FindAction("RangeAttack", throwIfNotFound: true);
        m_Player_Map = m_Player.FindAction("Map", throwIfNotFound: true);
        m_Player_Quest = m_Player.FindAction("Quest", throwIfNotFound: true);
        m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_ButtonSelect = m_Player.FindAction("ButtonSelect", throwIfNotFound: true);
        m_Player_MoveX = m_Player.FindAction("MoveX", throwIfNotFound: true);
        m_Player_MoveY = m_Player.FindAction("MoveY", throwIfNotFound: true);
        m_Player_Settings = m_Player.FindAction("Settings", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_LookDown;
    private readonly InputAction m_Player_LookUp;
    private readonly InputAction m_Player_MeleeAttack;
    private readonly InputAction m_Player_RangeAttack;
    private readonly InputAction m_Player_Map;
    private readonly InputAction m_Player_Quest;
    private readonly InputAction m_Player_Inventory;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_ButtonSelect;
    private readonly InputAction m_Player_MoveX;
    private readonly InputAction m_Player_MoveY;
    private readonly InputAction m_Player_Settings;
    public struct PlayerActions
    {
        private @InputSystem m_Wrapper;
        public PlayerActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @LookDown => m_Wrapper.m_Player_LookDown;
        public InputAction @LookUp => m_Wrapper.m_Player_LookUp;
        public InputAction @MeleeAttack => m_Wrapper.m_Player_MeleeAttack;
        public InputAction @RangeAttack => m_Wrapper.m_Player_RangeAttack;
        public InputAction @Map => m_Wrapper.m_Player_Map;
        public InputAction @Quest => m_Wrapper.m_Player_Quest;
        public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @ButtonSelect => m_Wrapper.m_Player_ButtonSelect;
        public InputAction @MoveX => m_Wrapper.m_Player_MoveX;
        public InputAction @MoveY => m_Wrapper.m_Player_MoveY;
        public InputAction @Settings => m_Wrapper.m_Player_Settings;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @LookDown.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookDown;
                @LookDown.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookDown;
                @LookDown.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookDown;
                @LookUp.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookUp;
                @LookUp.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookUp;
                @LookUp.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookUp;
                @MeleeAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMeleeAttack;
                @RangeAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRangeAttack;
                @RangeAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRangeAttack;
                @RangeAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRangeAttack;
                @Map.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                @Quest.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuest;
                @Quest.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuest;
                @Quest.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuest;
                @Inventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @ButtonSelect.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonSelect;
                @ButtonSelect.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonSelect;
                @ButtonSelect.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonSelect;
                @MoveX.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveX;
                @MoveX.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveX;
                @MoveX.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveX;
                @MoveY.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveY;
                @MoveY.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveY;
                @MoveY.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveY;
                @Settings.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
                @Settings.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
                @Settings.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @LookDown.started += instance.OnLookDown;
                @LookDown.performed += instance.OnLookDown;
                @LookDown.canceled += instance.OnLookDown;
                @LookUp.started += instance.OnLookUp;
                @LookUp.performed += instance.OnLookUp;
                @LookUp.canceled += instance.OnLookUp;
                @MeleeAttack.started += instance.OnMeleeAttack;
                @MeleeAttack.performed += instance.OnMeleeAttack;
                @MeleeAttack.canceled += instance.OnMeleeAttack;
                @RangeAttack.started += instance.OnRangeAttack;
                @RangeAttack.performed += instance.OnRangeAttack;
                @RangeAttack.canceled += instance.OnRangeAttack;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
                @Quest.started += instance.OnQuest;
                @Quest.performed += instance.OnQuest;
                @Quest.canceled += instance.OnQuest;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @ButtonSelect.started += instance.OnButtonSelect;
                @ButtonSelect.performed += instance.OnButtonSelect;
                @ButtonSelect.canceled += instance.OnButtonSelect;
                @MoveX.started += instance.OnMoveX;
                @MoveX.performed += instance.OnMoveX;
                @MoveX.canceled += instance.OnMoveX;
                @MoveY.started += instance.OnMoveY;
                @MoveY.performed += instance.OnMoveY;
                @MoveY.canceled += instance.OnMoveY;
                @Settings.started += instance.OnSettings;
                @Settings.performed += instance.OnSettings;
                @Settings.canceled += instance.OnSettings;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_AllSchemeIndex = -1;
    public InputControlScheme AllScheme
    {
        get
        {
            if (m_AllSchemeIndex == -1) m_AllSchemeIndex = asset.FindControlSchemeIndex("All");
            return asset.controlSchemes[m_AllSchemeIndex];
        }
    }
    private int m_All1SchemeIndex = -1;
    public InputControlScheme All1Scheme
    {
        get
        {
            if (m_All1SchemeIndex == -1) m_All1SchemeIndex = asset.FindControlSchemeIndex("All1");
            return asset.controlSchemes[m_All1SchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnLookDown(InputAction.CallbackContext context);
        void OnLookUp(InputAction.CallbackContext context);
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnRangeAttack(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnQuest(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnButtonSelect(InputAction.CallbackContext context);
        void OnMoveX(InputAction.CallbackContext context);
        void OnMoveY(InputAction.CallbackContext context);
        void OnSettings(InputAction.CallbackContext context);
    }
}
