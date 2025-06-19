using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap 관련 클래스를 사용하기 위해 필수!

public class CameraController : MonoBehaviour
{
    // 인스펙터에서 설정할 변수들
    public Transform target;          // 카메라가 따라갈 대상 (플레이어)
    public Tilemap mapTilemap;        // 경계의 기준이 될 Tilemap

    // 내부 계산에 사용될 변수들
    private Camera mainCamera;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private float minX, maxX, minY, maxY; // 최종적으로 계산된 맵의 경계

    void Start()
    {
        // 기본 컴포넌트 초기화
        mainCamera = GetComponent<Camera>();

        // Tilemap의 경계를 직접 계산합니다.
        // mapTilemap.localBounds는 Tilemap의 로컬 좌표 기준 경계입니다.
        // mapTilemap.transform.position을 더해 월드 좌표 기준 경계를 구합니다.
        Vector3 mapMin = mapTilemap.localBounds.min + mapTilemap.transform.position;
        Vector3 mapMax = mapTilemap.localBounds.max + mapTilemap.transform.position;

        minX = mapMin.x;
        maxX = mapMax.x;
        minY = mapMin.y;
        maxY = mapMax.y;

        // 카메라 뷰의 절반 크기를 미리 계산해 둡니다.
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect; // aspect = 너비/높이 비율
    }

    void LateUpdate()
    {
        // target이나 mapTilemap이 설정되지 않았다면 오류 방지를 위해 실행하지 않습니다.
        if (target == null || mapTilemap == null)
        {
            return;
        }

        // 카메라가 따라갈 목표 위치는 플레이어의 현재 위치입니다.
        Vector3 targetPosition = target.position;

        // Mathf.Clamp 함수로 카메라의 X, Y 좌표가 맵 경계를 벗어나지 않도록 제한합니다.
        float clampedX = Mathf.Clamp(targetPosition.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, minY + cameraHalfHeight, maxY - cameraHalfHeight);

        // 계산된 최종 위치로 카메라를 이동시킵니다. (카메라의 Z축 위치는 그대로 유지)
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}