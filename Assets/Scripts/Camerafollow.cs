using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (플레이어)
    public Vector3 offset;   // 플레이어와의 상대적인 거리
    public float smoothSpeed = 0.125f; // 카메라 움직임의 부드러움

    private void LateUpdate()
    {
        // 목표 위치 계산 (플레이어 위치 + 오프셋)
        Vector3 desiredPosition = target.position + offset;

        // 기존 위치에서 원하는 위치까지 부드럽게 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 새로운 카메라 위치 적용
        transform.position = smoothedPosition;

        // 플레이어를 항상 카메라가 바라보도록 설정 (필요 시)
        transform.LookAt(target);
    }
}
