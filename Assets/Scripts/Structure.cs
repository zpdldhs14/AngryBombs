using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public int maxHealth = 200;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
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