using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlay : MonoBehaviour
{
    /*public Material pauseButtonMaterial;
    public Material playButtonMaterial;
    public Renderer screenRenderer;
    */

    [SerializeField] Slider volumeSlider;

    public VideoClip[] videoClips;


    private VideoPlayer videoPlayer;
    private int videoClipIndex;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void SetNextClip()
    {
        videoClipIndex++;

        if(videoClipIndex>= videoClips.Length)
        {
            videoClipIndex = videoClipIndex % videoClips.Length;
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
            videoPlayer.Play();
            //screenRenderer.material = pauseButtonMaterial;
        }

    }


}
