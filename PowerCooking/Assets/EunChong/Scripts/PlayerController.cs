using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction mouseClickAction;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    Camera mainCamera;
    Coroutine coroutine;
    Vector3 targetPosition;

    CharacterController characterController;

    int floorLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        floorLayer = LayerMask.NameToLayer("Floor");
    }

    private void OnEnable()
    {
        // 입력 함수 활성화
        mouseClickAction.Enable();
        // 핸들러 연결
        mouseClickAction.performed += Move;
    }

    private void OnDisable()
    {
        // 핸들러 해제
        mouseClickAction.performed -= Move;
        // 입력 함수 비활성화
        mouseClickAction.Disable();
    }

    /// <summary>
    /// 플레이어의 이동 함수입니다.
    /// </summary>
    private void Move(InputAction.CallbackContext context)
    {
        // 카메라의 마우스 포인트 위치 할당
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // 
        if(Physics.Raycast(ray:ray, hitInfo:out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer == floorLayer)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(PlayerMoveTowards(hit.point));
            targetPosition = hit.point;
        }
    }

    private IEnumerator PlayerMoveTowards(Vector3 target)
    {
        float playerDistanceToFloor = transform.position.y - target.y;

        target.y += playerDistanceToFloor;

        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            // 충돌 무시
            Vector3 destination = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            // 캐릭터 컨트롤
            Vector3 direction = target - transform.position;
            Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
            characterController.Move(movement);

            // 캐릭터 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 1);
    }
}
