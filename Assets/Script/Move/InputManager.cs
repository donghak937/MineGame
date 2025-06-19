using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // --- 싱글톤 패턴 ---
    public static InputManager Instance { get; private set; }

    // "오른쪽 마우스가 클릭되었다"는 사실을 방송할 채널(이벤트)
    // Vector3는 클릭된 월드 좌표를 전달하기 위함
    public event Action<Vector3> OnRightMouseClick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // 필요하다면 씬이 바뀌어도 살아남게 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 만약 마우스 오른쪽 버튼을 눌렀다면
        if (Input.GetMouseButtonDown(1)) // 0: 왼쪽, 1: 오른쪽, 2: 가운데
        {
            // 마우스의 스크린 좌표를 월드 좌표로 변환
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Z축 값은 2D에서 의미 없으므로 0으로 고정
            mouseWorldPosition.z = 0f; 

            // OnRightMouseClick 이벤트를 구독한 모든 대상에게
            // 클릭된 월드 좌표(mouseWorldPosition)를 담아서 방송!
            OnRightMouseClick?.Invoke(mouseWorldPosition);
        }
    }
}