using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 몬스터와 충돌했는지 확인
        if (collision.gameObject.TryGetComponent<Monster>(out Monster monster))
        {
            // 즉시 파괴
            DestroyStructure();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Structure took {damage} damage, current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            DestroyStructure();
        }
    }

    private void DestroyStructure()
    {
        Debug.Log("Structure destroyed!");
        Destroy(gameObject);
    }
}