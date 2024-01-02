using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction prepareCookInput;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    Camera mainCamera;
    Coroutine coroutine;
    Vector3 targetPosition;

    CharacterController characterController;

    int floorLayer;
    int kitchenApplianceLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        floorLayer = LayerMask.NameToLayer("Floor");
        kitchenApplianceLayer = LayerMask.NameToLayer("KitchenAppliance");
    }

    private void OnEnable()
    {
        // 입력 함수 활성화
        moveInput.Enable();
        prepareCookInput.Enable();
        // 핸들러 연결
        moveInput.performed += Move;
        prepareCookInput.performed += PrepareCook;
    }

    private void OnDisable()
    {
        // 핸들러 해제
        moveInput.performed -= Move;
        prepareCookInput.performed -= PrepareCook;
        // 입력 함수 비활성화
        moveInput.Disable();
        prepareCookInput.Disable();
    }

    /// <summary>
    /// 플레이어의 이동 함수입니다.
    /// </summary>
    private void Move(InputAction.CallbackContext context)
    {
        // 카메라의 마우스 포인트 위치 할당
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // 바닥을 클릭했을 때
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
            // 캐릭터 컨트롤
            Vector3 direction = target - transform.position;
            Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
            characterController.Move(movement);

            // 캐릭터 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    /// <summary>
    /// 플레이어의 이동 함수입니다.
    /// </summary>
    private void PrepareCook(InputAction.CallbackContext context)
    {
        // 카메라의 마우스 포인트 위치 할당
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // 주방가전을 클릭했을 때
        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer == kitchenApplianceLayer)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(LookKitchenAppliance(hit.point));
            targetPosition = hit.point;
        }
    }

    private IEnumerator LookKitchenAppliance(Vector3 target)
    {
        float playerDistanceToFloor = transform.position.y - target.y;

        target.y += playerDistanceToFloor;

        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            // 캐릭터 회전
            Vector3 direction = target - transform.position;
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
