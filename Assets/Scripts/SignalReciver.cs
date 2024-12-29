using UnityEngine;
using UnityEngine.Playables;

public class SignalReciver : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayerMovement playerMovement;
    public CameraFollow cameraFollow; 

    void Start()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineStopped;
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            playerMovement.EnablePlayerContorls();
            cameraFollow.enabled = true;
        }
    }

    private void OnDestroy()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnTimelineStopped;
        }
    }
}
