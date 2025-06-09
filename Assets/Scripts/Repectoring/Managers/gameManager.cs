using System;
using UnityEngine;
using System.Collections.Generic;

public class gameManager : MonoBehaviour
{
    [SerializeField] private MonsterManager _monsterManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CameraManager _cameraManager;
    
    [SerializeField] private Animator characterAnimator;
    private string victoryTrigger = "Victory";

    private void Start()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        Cleanup();
    }

    private void Initialize()
    {
        if (_monsterManager != null)
        {
            _monsterManager.OnAllMonstersDestroyed += HandleAllMonsterDestory;
        }
    }

    private void Cleanup()
    {
        if (_monsterManager != null)
        {
            _monsterManager.OnAllMonstersDestroyed -= HandleAllMonsterDestory;
        }
    }

    private void HandleAllMonsterDestory()
    {
        if (_uiManager != null)
        {
            _uiManager.ShowClearUI();
        }

        if (_cameraManager != null)
        {
            _cameraManager.SetVictoryCamera();
        }

        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger(victoryTrigger);
        }
    }
}