using UnityEngine;
using System.Collections.Generic;
using System;

public class MonsterManager : MonoBehaviour, IManager
{
    [Header("Monsters")]
    [SerializeField] private List<Monster> activeMonsters = new List<Monster>();

    // 몬스터 관련 이벤트
    public event Action<Monster> OnMonsterRegistered;
    public event Action<Monster> OnMonsterUnregistered;
    public event Action OnAllMonstersDestroyed;

    public void Initialize()
    {
        Debug.Log("MonsterManager: Initializing...");
        // 초기화 시 필요한 작업
        FindAllMonsters();
    }

    public void Clean()
    {
        
    }

    public void Cleanup()
    {
        Debug.Log("MonsterManager: Cleaning up...");
        // 모든 몬스터의 이벤트 구독 해제
        foreach (var monster in activeMonsters)
        {
            if (monster != null)
            {
                UnregisterMonster(monster);
            }
        }
        activeMonsters.Clear();
    }

    private void FindAllMonsters()
    {
        Monster[] foundMonsters = FindObjectsOfType<Monster>();
        Debug.Log($"MonsterManager: Found {foundMonsters.Length} monsters");
        foreach (var monster in foundMonsters)
        {
            if (monster != null)
            {
                RegisterMonster(monster);
            }
        }
    }

    public void RegisterMonster(Monster monster)
    {
        if (monster == null || activeMonsters.Contains(monster)) return;

        activeMonsters.Add(monster);
        monster.OnMonsterDied += HandleMonsterDied;
        OnMonsterRegistered?.Invoke(monster);
        Debug.Log($"MonsterManager: Registered Monster. Total count: {activeMonsters.Count}");
    }

    public void UnregisterMonster(Monster monster)
    {
        if (monster == null || !activeMonsters.Contains(monster)) return;

        activeMonsters.Remove(monster);
        monster.OnMonsterDied -= HandleMonsterDied;
        OnMonsterUnregistered?.Invoke(monster);
        Debug.Log($"MonsterManager: Unregistered Monster. Remaining count: {activeMonsters.Count}");

        CheckAllMonstersDestroyed();
    }

    private void HandleMonsterDied(Monster monster)
    {
        Debug.Log($"MonsterManager: Monster died");
        UnregisterMonster(monster);
    }

    private void CheckAllMonstersDestroyed()
    {
        if (activeMonsters.Count == 0)
        {
            Debug.Log("MonsterManager: All monsters destroyed! Triggering victory event.");
            OnAllMonstersDestroyed?.Invoke();
        }
    }

    // 디버깅용 메서드
    public int GetActiveMonsterCount()
    {
        return activeMonsters.Count;
    }

    public bool HasActiveMonsters()
    {
        return activeMonsters.Count > 0;
    }
} 