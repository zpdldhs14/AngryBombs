using UnityEngine;

public class UIManager : MonoBehaviour,IManager
{
    [SerializeField] private GameObject clearUI;

    private int currentScore = 0;
    
    public void Initialize()
    {
        //초기 ui상태 설정
        HideAllPanel();
        UpdateScore(0);
        
    }

    public void Cleanup()
    {
        
    }

    private void HideAllPanel()
    {
        if(clearUI) clearUI.SetActive(false);
    }

    public void ShowClearUI()
    {
        if(clearUI) clearUI.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        currentScore = score;
    }

    public void AddScore(int score)
    {
        UpdateScore(score);
    }
    
    public void Clean()
    {
        
    }
}