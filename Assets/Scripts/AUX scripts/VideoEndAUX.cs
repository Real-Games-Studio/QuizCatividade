using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class VideoEndAUX : MonoBehaviour
{
    private MediaPlayer mediaPlayer;
    [SerializeField] private GameObject mediaPlayerDisplay;

    void Start()
    {
        mediaPlayer = GetComponent<MediaPlayer>();
        mediaPlayer.Events.AddListener(OnVideoFinished);
        mediaPlayer.gameObject.SetActive(false);
    }

    private void OnVideoFinished(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        if (et == MediaPlayerEvent.EventType.FinishedPlaying)
        {
            mediaPlayerDisplay.SetActive(false);
            Managers.Instance.GameManager.CanStartTimer = true;
            Managers.Instance.InputManager.CanPress = true;
        }
    }


}
