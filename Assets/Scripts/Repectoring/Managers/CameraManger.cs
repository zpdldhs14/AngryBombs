using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour, IManager
{
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera gameplayCamera;
    [SerializeField] private CinemachineVirtualCamera victoryCamera;

    [Header("Camera Settings")]
    [SerializeField] private float transitionSpeed = 1f;
    [SerializeField] private int gameplayPriority = 20;
    [SerializeField] private int victoryPriority = 20;
    [SerializeField] private int gameOverPriority = 20;

    private CinemachineVirtualCamera currentCamera;

    public void Initialize()
    {
        // 초기 카메라 설정
        SetGameplayCamera();
    }

    public void Clean()
    {
        
    }

    public void Cleanup()
    {
        // 필요한 정리 작업
    }

    public void SetGameplayCamera()
    {
        if (gameplayCamera)
        {
            SetCameraPriority(gameplayCamera, gameplayPriority);
            currentCamera = gameplayCamera;
        }
    }

    public void SetVictoryCamera()
    {
        if (victoryCamera)
        {
            SetCameraPriority(victoryCamera, victoryPriority);
            currentCamera = victoryCamera;
        }
    }

    private void SetCameraPriority(CinemachineVirtualCamera camera, int priority)
    {
        if (camera != null)
        {
            // 다른 카메라들의 우선순위를 낮춤
            if (gameplayCamera) gameplayCamera.Priority = 10;
            if (victoryCamera) victoryCamera.Priority = 10;

            // 선택된 카메라의 우선순위를 높임
            camera.Priority = priority;
        }
    }

    public void ShakeCamera(float intensity, float duration)
    {
        if (currentCamera != null)
        {
            var noise = currentCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise != null)
            {
                StartCoroutine(ShakeCoroutine(noise, intensity, duration));
            }
        }
    }

    private System.Collections.IEnumerator ShakeCoroutine(CinemachineBasicMultiChannelPerlin noise, float intensity, float duration)
    {
        float elapsed = 0f;
        float originalIntensity = noise.m_AmplitudeGain;

        while (elapsed < duration)
        {
            noise.m_AmplitudeGain = Mathf.Lerp(intensity, 0f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        noise.m_AmplitudeGain = originalIntensity;
    }
}