using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GamePlayTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // 전환할 Virtual Camera
    public int newPriority = 20; // 충돌 시 Virtual Camera의 우선순위 설정
    private int defaultPriority = 10; // 기본 Virtual Camera 우선순위

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌했는지 확인 (Player 태그로 확인)
        if (other.CompareTag("Player"))
        {
            // Virtual Camera의 우선순위를 높여 활성화
            virtualCamera.Priority = newPriority;
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        // 플레이어가 충돌 영역을 벗어나면 다시 기본 카메라로 전환
        if (other.CompareTag("Player"))
        {
            // Virtual Camera의 우선순위를 기본값으로 되돌림
            virtualCamera.Priority = defaultPriority;
        }
    }*/
}

