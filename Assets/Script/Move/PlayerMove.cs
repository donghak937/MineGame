using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 캐릭터 이동 속도

    private Vector3 targetPosition; // 이동해야 할 목표 지점
    private bool isMoving = false; // 현재 이동 중인지 여부

    // 스크립트가 활성화될 때 InputManager의 방송을 구독
    private void OnEnable()
    {
        InputManager.Instance.OnRightMouseClick += MoveToPosition;
    }

    // 스크립트가 비활성화될 때 구독 취소 (메모리 누수 방지)
    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnRightMouseClick -= MoveToPosition;
        }
    }

    // InputManager가 방송을 보내면 호출될 함수
    private void MoveToPosition(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
        Debug.Log("이동 명령 받음! 목표: " + targetPosition);
    }

    void Update()
    {
        // 이동 중일 때만 실행
        if (isMoving)
        {
            // 현재 위치에서 목표 위치까지 프레임마다 조금씩 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 현재 위치와 목표 위치가 거의 같아졌다면 이동을 멈춘다
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                Debug.Log("목표 도착! 이동 중지.");
            }
        }
    }
}