using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;
    [SerializeField]
    Image progressBar;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
      AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);//�񵿱� ������� ���� �ҷ���.
      op.allowSceneActivation = false;//���� �ҷ������� Ȱ��ȭ�� ���� ����.

      float timer = 0.0f;
      while(!op.isDone)
      {
        yield return null;
        if(op.progress < 0.9f)
        {
          progressBar.fillAmount = op.progress;
        }
        else
        {
          timer += Time.unscaledDeltaTime;
          progressBar.fillAmount = Mathf.Lerp(0.9f, 1.0f, timer);
          if(progressBar.fillAmount >= 1.0f)
          {
            op.allowSceneActivation = true;
            yield break;
          }
        }
      }
    }
}
