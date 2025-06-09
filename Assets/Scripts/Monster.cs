using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private MonsterManager monsterManager;

    public event Action<Monster> OnMonsterDied;

    private void Start()
    {
        Initialize();
    }

    public void Initialize(MonsterManager manager = null)
    {
        currentHealth = maxHealth;
        monsterManager = manager;
        if (monsterManager != null)
        {
            monsterManager.RegisterMonster(this);
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // MonsterManager에 알림
        if (monsterManager != null)
        {
            monsterManager.UnregisterMonster(this);
        }

        // 이벤트 발생
        OnMonsterDied?.Invoke(this);
        
        // 몬스터 제거
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        OnMonsterDied = null;
    }
}
