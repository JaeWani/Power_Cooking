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
        // �Է� �Լ� Ȱ��ȭ
        moveInput.Enable();
        prepareCookInput.Enable();
        // �ڵ鷯 ����
        moveInput.performed += Move;
        prepareCookInput.performed += PrepareCook;
    }

    private void OnDisable()
    {
        // �ڵ鷯 ����
        moveInput.performed -= Move;
        prepareCookInput.performed -= PrepareCook;
        // �Է� �Լ� ��Ȱ��ȭ
        moveInput.Disable();
        prepareCookInput.Disable();
    }

    /// <summary>
    /// �÷��̾��� �̵� �Լ��Դϴ�.
    /// </summary>
    private void Move(InputAction.CallbackContext context)
    {
        // ī�޶��� ���콺 ����Ʈ ��ġ �Ҵ�
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // �ٴ��� Ŭ������ ��
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
            // ĳ���� ��Ʈ��
            Vector3 direction = target - transform.position;
            Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
            characterController.Move(movement);

            // ĳ���� ȸ��
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    /// <summary>
    /// �÷��̾��� �̵� �Լ��Դϴ�.
    /// </summary>
    private void PrepareCook(InputAction.CallbackContext context)
    {
        // ī�޶��� ���콺 ����Ʈ ��ġ �Ҵ�
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // �ֹ氡���� Ŭ������ ��
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
            // ĳ���� ȸ��
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
