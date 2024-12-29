using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int health = 100;
    private int currenthealth;
    
    void Start()
    {
        currenthealth = health;
    }

    public void Damage(int damage)
    {
        currenthealth -= damage;
        if(currenthealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        // GameManager에 파괴 사실 알림
        BombGameManager gameManager = FindObjectOfType<BombGameManager>();
        if (gameManager != null)
        {
            gameManager.OnMonsterDestroyed(gameObject);
        }

        // 몬스터 제거
        Destroy(gameObject);
    }
}
