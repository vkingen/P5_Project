using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlay : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    public VideoClip[] videoClips;


    private VideoPlayer videoPlayer;
    private int videoClipIndex;

    private void Awake()
    {
        videoClipIndex = 0;
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void SetNextClip()
    {
        videoClipIndex++;

        if(videoClipIndex>= videoClips.Length)
        {
            //videoClipIndex = videoClipIndex % videoClips.Length;
            videoClipIndex = 0;
        }

        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
    }

    public void SetPreviousClip()
    {
        videoClipIndex--;

        if (videoClipIndex < 0)
        {
            //videoClipIndex = videoClipIndex % videoClips.Length;
            videoClipIndex = 0;
        } 

        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
    }

    public void PlayPause()
    {
        if(videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            //screenRenderer.material = playButtonMaterial;
        }
        else
        {
            videoPlayer.clip = videoClips[videoClipIndex];
            videoPlayer.Play();
            //screenRenderer.material = pauseButtonMaterial;
        }

    }


}
