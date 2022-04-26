using UnityEngine;
using UnityEngine.UI;

//Interacting with objects and doors
namespace Suntail
{
    public class PlayerInteractions : MonoBehaviour
    {
        [Header("Interaction variables")]
        [Tooltip("Layer mask for interactive objects")]
        [SerializeField] private LayerMask interactionLayer;
        [Tooltip("Maximum distance from player to object of interaction")]
        [SerializeField] private float interactionDistance = 3f;
        [Tooltip("Tag for door object")]
        [SerializeField] private string doorTag = "Door";
        [Tooltip("Tag for pickable object")]
        [SerializeField] private string itemTag = "Item";
        [Tooltip("열고 닫을수 있는 오브젝트")]
        [SerializeField] private string drawerTag = "Drawer";
        [Tooltip("패스워드 퍼즐 오브젝트")]
        [SerializeField] private string passwordPuzzleTag = "PasswordPuzzle";
        [Tooltip("스케일 퍼즐 오브젝트")]
        [SerializeField] private string scalePuzzleTag = "ScalePuzzle";
        [Tooltip("컵보드 오브젝트")]
        [SerializeField] private string cupBoardTag = "CupBoard";
        [Tooltip("주울수 있는 아이템 오브젝트")]
        [SerializeField] private string pickUpItemTag = "InventoryItem";
        [Tooltip("The player's main camera")]
        [SerializeField] private Camera mainCamera;
        [Tooltip("Parent object where the object to be lifted becomes")]
        [SerializeField] private Transform pickupParent;

        [Header("Keybinds")]
        [Tooltip("Interaction key")]
        [SerializeField] private KeyCode interactionKey = KeyCode.E;

        [Header("Object Following")]
        [Tooltip("Minimum speed of the lifted object")]
        [SerializeField] private float minSpeed = 0;
        [Tooltip("Maximum speed of the lifted object")]
        [SerializeField] private float maxSpeed = 3000f;

        [Header("UI")]
        [Tooltip("Background object for text")]
        [SerializeField] private Image uiPanel;
        [Tooltip("Text holder")]
        [SerializeField] private Text panelText;
        [Tooltip("Text when an object can be lifted")]
        [SerializeField] private string itemPickUpText;
        [Tooltip("Text when an object can be drop")]
        [SerializeField] private string itemDropText;
        [Tooltip("Text when the door can be opened")]
        [SerializeField] private string doorOpenText;
        [Tooltip("Text when the door can be closed")]
        [SerializeField] private string doorCloseText;
        [Tooltip("서랍장을 열때 나올 텍스트입니다.")]
        [SerializeField] private string drawerOpenText;
        [Tooltip("서랍장을 닫을때 나올 텍스트입니다.")]
        [SerializeField] private string drawerCloseText;

        //Private variables.
        private PhysicsObject _physicsObject;
        private PhysicsObject _currentlyPickedUpObject;
        private PhysicsObject _lookObject;
        private Quaternion _lookRotation;
        private Vector3 _raycastPosition;
        private Rigidbody _pickupRigidBody;
        private Door _lookDoor;
        private DrawerOpen _drawerObj;
        private PasswordOpen _passwordObj;
        private Cupboard _cupBoardObj;
        private ScalePuzzleScript _scalePuzzleObj;
        private Item _pickUpObj;
        private float _currentSpeed = 0f;
        private float _currentDistance = 0f;
        private CharacterController _characterController;


        private void Start()
        {
            mainCamera = Camera.main;
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Interactions();
            LegCheck();
        }

        //Determine which object we are now looking at, depending on the tag and component
        private void Interactions()
        {
            _raycastPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit interactionHit;
            if (Physics.Raycast(_raycastPosition, mainCamera.transform.forward, 
                out interactionHit, interactionDistance, interactionLayer))
            {
                ObjScript.instance.isCheck = true;
                if (interactionHit.collider.gameObject.GetComponent<Outline>() == null)
                {
                    interactionHit.collider.gameObject.AddComponent<Outline>();
                    interactionHit.collider.gameObject.AddComponent<ObjScript>();
                    Outline interoutline = interactionHit.collider.gameObject.GetComponent<Outline>();
                    interoutline.OutlineMode = Outline.Mode.OutlineHidden;
                    interoutline.OutlineWidth = 2.7f;
                    //interoutline.enabled = true;
                    //ObjScript interScript = interactionHit.collider.gameObject.GetComponent<ObjScript>();

                }
                if (interactionHit.collider.CompareTag(itemTag))
                {
                    _lookObject = interactionHit.collider.GetComponentInChildren<PhysicsObject>();
                    ShowItemUI();
                }
                else if (interactionHit.collider.CompareTag(doorTag))
                {
                    _lookDoor = interactionHit.collider.gameObject.GetComponentInChildren<Door>();
                    ShowDoorUI();
                    if (Input.GetKeyDown(interactionKey))
                    {
                        _lookDoor.PlayDoorAnimation();
                    }
                }
                else if (interactionHit.collider.CompareTag(drawerTag))
                {
                    _drawerObj = interactionHit.collider.gameObject.GetComponent<DrawerOpen>();
                    _drawerObj.isCheck = true;
                    ShowDrawUI();
                }
                else if (interactionHit.collider.CompareTag(passwordPuzzleTag))
                {
                    _passwordObj = interactionHit.collider.gameObject.GetComponent<PasswordOpen>();
                    PasswordPuzzleUI();
                }
                else if(interactionHit.collider.CompareTag(scalePuzzleTag))
                {
                    _scalePuzzleObj = interactionHit.collider.gameObject.GetComponent<ScalePuzzleScript>();
                    ScalePuzzleUI();
                }
                else if(interactionHit.collider.CompareTag(cupBoardTag))
                {
                    _cupBoardObj = interactionHit.collider.gameObject.GetComponent<Cupboard>();
                    _cupBoardObj.isCheck = true;
                    CupBoardUI();
                }
                else if(interactionHit.collider.CompareTag(pickUpItemTag))
                {
                    _pickUpObj = interactionHit.collider.gameObject.GetComponent<MyData>().myData;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Inventory.instance.AddItem(_pickUpObj);
                        Destroy(interactionHit.collider.gameObject);
                    }
                    PickUpUI();
                }
            }
            else
            {
                _lookDoor = null;
                _lookObject = null;
                uiPanel.gameObject.SetActive(false);
                ObjScript.instance.isCheck = false;
            }

            if (Input.GetKeyDown(interactionKey))
            {
                if (_currentlyPickedUpObject == null)
                {
                    if (_lookObject != null)
                    {
                        PickUpObject();
                    }
                }
                else
                {
                    BreakConnection();
                }
            }
        }

        //Disconnects from the object when the player attempts to step on the object, prevents flight on the object
        private void LegCheck()
        {
            Vector3 spherePosition = _characterController.center + transform.position;
            RaycastHit legCheck;
            if (Physics.SphereCast(spherePosition, 0.3f, Vector3.down, out legCheck, 2.0f))
            {
                if (legCheck.collider.CompareTag(itemTag))
                {
                    BreakConnection();
                }
            }
        }

        //Velocity movement toward pickup parent
        private void FixedUpdate()
        {
            if (_currentlyPickedUpObject != null)
            {
                _currentDistance = Vector3.Distance(pickupParent.position, _pickupRigidBody.position);
                _currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, _currentDistance / interactionDistance);
                _currentSpeed *= Time.fixedDeltaTime;
                Vector3 direction = pickupParent.position - _pickupRigidBody.position;
                _pickupRigidBody.velocity = direction.normalized * _currentSpeed;
            }
        }

        //Picking up an looking object
        public void PickUpObject()
        {
            _physicsObject = _lookObject.GetComponentInChildren<PhysicsObject>();
            _currentlyPickedUpObject = _lookObject;
            _lookRotation = _currentlyPickedUpObject.transform.rotation;
            _pickupRigidBody = _currentlyPickedUpObject.GetComponent<Rigidbody>();
            _pickupRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            _pickupRigidBody.transform.rotation = _lookRotation;
            _physicsObject.playerInteraction = this;
            StartCoroutine(_physicsObject.PickUp());
        }

        //Release the object
        public void BreakConnection()
        {
            if (_currentlyPickedUpObject)
            {
                _pickupRigidBody.constraints = RigidbodyConstraints.None;
                _currentlyPickedUpObject = null;
                _physicsObject.pickedUp = false;
                _currentDistance = 0;
            }
        }

        //Show interface elements when hovering over an object
        private void ShowDoorUI()
        {
            uiPanel.gameObject.SetActive(true);

            if (_lookDoor.doorOpen)
            {
                panelText.text = doorCloseText;
            }
            else
            {
                panelText.text = doorOpenText;
            }
        }

        private void ShowItemUI()
        {
            uiPanel.gameObject.SetActive(true);

            if (_currentlyPickedUpObject == null)
            {
                panelText.text = itemPickUpText;
            }
            else if (_currentlyPickedUpObject != null)
            {
                panelText.text = itemDropText;
            }

        }

        private void ShowDrawUI()
        {
            uiPanel.gameObject.SetActive(true);

            if(_drawerObj.isEnd == true)
            {
                panelText.text = drawerCloseText;
            }
            else if(_drawerObj.isEnd == false)
            {
                panelText.text = drawerOpenText;
            }
        }

        private void PasswordPuzzleUI()
        {
            uiPanel.gameObject.SetActive(true);

            if(_passwordObj.GetComponent<PasswordOpen>().puzzleClear == false)
            {
                panelText.text = "퍼즐 조사하기";
            }
            else if (_passwordObj.GetComponent<PasswordOpen>().puzzleClear == true)
            {
                panelText.text = "클리어한 퍼즐";
            }
            
        }

        private void ScalePuzzleUI()
        {
            uiPanel.gameObject.SetActive(true);

            if(_scalePuzzleObj.GetComponent<ScalePuzzleScript>().bookCount == 0)
            {
                panelText.text = "올려놓기" + "\n0/3";
            }
            if (_scalePuzzleObj.GetComponent<ScalePuzzleScript>().bookCount == 1)
            {
                panelText.text = "올려놓기" + "\n1/3";
            }
            if (_scalePuzzleObj.GetComponent<ScalePuzzleScript>().bookCount == 2)
            {
                panelText.text = "올려놓기" + "\n2/3";
            }
            else if(_scalePuzzleObj.GetComponent<ScalePuzzleScript>().bookCount == 3)
            {
                panelText.text = "클리어한 퍼즐" + "\n2/3";
            }
        }

        private void CupBoardUI()
        {
            uiPanel.gameObject.SetActive(true);

            if(_cupBoardObj.isEnd != true)
            {
                panelText.text = "열기";
            }
            else if (_cupBoardObj.isEnd)
            {
                panelText.text = "닫기";
            }
        }
        
        private void PickUpUI()
        {
            uiPanel.gameObject.SetActive(true);

            panelText.text = "줍기";
        }
    }
}