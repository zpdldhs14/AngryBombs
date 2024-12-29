using UnityEngine;
using System.Collections.Generic;

public class BombGameManager : MonoBehaviour
{
    // 몬스터를 관리할 리스트
    public List<GameObject> monsters = new List<GameObject>();

    public Cinemachine.CinemachineVirtualCamera victoryCamera;
    public string victoryTrigger = "Victory"; 
    public Animator characterAnimator;
    // 클리어 UI를 참조
    public GameObject clearUI;

    private void Start()
    {
        // 클리어 UI 비활성화
        if (clearUI != null)
            clearUI.SetActive(false);

        // 씬 내 모든 몬스터를 리스트에 저장
        FindAllMonsters();
    }

    // 씬 내 몬스터를 모두 찾아서 리스트에 추가
    private void FindAllMonsters()
    {
        Monster[] foundMonsters = FindObjectsOfType<Monster>();
        foreach (var monster in foundMonsters)
        {
            monsters.Add(monster.gameObject);
        }
    }

    // 몬스터가 파괴되었을 때 호출
    public void OnMonsterDestroyed(GameObject monster)
    {
        if (monsters.Contains(monster))
        {
            monsters.Remove(monster); // 리스트에서 제거
        }

        // 남은 몬스터가 없으면 클리어 UI 표시
        if (monsters.Count == 0)
        {
            ShowClearUI();
        }
    }

    // 클리어 UI를 활성화
    private void ShowClearUI()
    {
        if (clearUI != null)
        {
            clearUI.SetActive(true);
            Debug.Log("Game Cleared!");
        }
        // Victory Camera 활성화
        if (victoryCamera != null)
        {
            victoryCamera.Priority = 20; // 다른 카메라보다 Priority 높게 설정
        }

        // Victory 애니메이션 재생
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger(victoryTrigger);
        }
    }
}