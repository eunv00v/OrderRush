using UnityEngine;
using UnityEngine.InputSystem;
using Services.UpdateService;
using UnityEngine.EventSystems;
using VContainer;
using UnityEngine.AI;

public class PlayerInputHandler : MonoBehaviour, IUpdatable
{
    [SerializeField] NavMeshMover _mover;
    [SerializeField] LayerMask _groundLayer;

    ICharacterStateMachine _stateMachine;
    MoveState _moveState;
    WorkState _workState;
    IdleState _idleState;

    Camera _mainCamera;
    IUpdateSubscriptionService _updateService;

    [Inject]
    public void Construct(
        IUpdateSubscriptionService updateService,
        ICharacterStateMachine stateMachine,
        MoveState moveState,
        WorkState workState,
        IdleState idleState)
    {
        _updateService = updateService;
        _stateMachine = stateMachine;
        _moveState = moveState;
        _workState = workState;
        _idleState = idleState;
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Start()
    {
        _moveState.SetMover(_mover);
    }

    void OnEnable()
    {
        _updateService?.RegisterUpdatable(this);
    }

    void OnDisable()
    {
        _updateService?.UnregisterUpdatable(this);
    }

    public void ManagedUpdate()
    {
#if UNITY_EDITOR
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    void HandleMouseInput()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;
        if (!mouse.leftButton.wasPressedThisFrame) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        var ray = _mainCamera.ScreenPointToRay(mouse.position.ReadValue());

        // IInteractable 먼저 체크
        if (Physics.Raycast(ray, out var hit, 100f))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                InteractWith(interactable);
                return;
            }
        }

        // 바닥 이동
        if (Physics.Raycast(ray, out var groundHit, 100f, _groundLayer))
        {
            if (NavMesh.SamplePosition(groundHit.point, out var navHit, 1f, NavMesh.AllAreas))
                MoveToPosition(navHit.position);
        }
    }

    void HandleTouchInput()
    {
        var touchscreen = Touchscreen.current;
        if (touchscreen == null) return;

        var touch = touchscreen.primaryTouch;
        if (!touch.press.wasPressedThisFrame) return;

        var ray = _mainCamera.ScreenPointToRay(touch.position.ReadValue());

        if (Physics.Raycast(ray, out var hit, 100f))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                InteractWith(interactable);
                return;
            }
        }

        if (Physics.Raycast(ray, out var groundHit, 100f, _groundLayer))
        {
            if (NavMesh.SamplePosition(groundHit.point, out var navHit, 1f, NavMesh.AllAreas))
                MoveToPosition(navHit.position);
        }
    }

    void InteractWith(IInteractable interactable)
    {
        var interactPoint = interactable.InteractPoint;

        if (!NavMesh.SamplePosition(interactPoint.position, out var navHit, 0.5f, NavMesh.AllAreas))
        {
            Debug.Log("이동 불가");
            return;
        }

        _workState.SetTarget(interactable);
        _moveState.SetDestination(navHit.position, _workState);
        _stateMachine.ChangeState(_moveState);
    }

    void MoveToPosition(Vector3 position)
    {
        _moveState.SetDestination(position);
        _stateMachine.ChangeState(_moveState);
    }
}