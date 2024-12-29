using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    // 씬을 다시 로드
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // 메인 메뉴로 이동 (메뉴 씬 이름 예시: "MainMenu")
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Lobby");
    }
}
