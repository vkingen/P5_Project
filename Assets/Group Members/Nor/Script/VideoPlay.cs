using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class VideoPlay : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    public VideoClip[] videoClips;

    public TMP_Text currentMinutes;
    public TMP_Text currentSeconds;
    public TMP_Text totalMinutes;
    public TMP_Text totalSeconds;

    public PlayHeadMover playHeadmover;



    private VideoPlayer videoPlayer;
    private int videoClipIndex;

    private void Awake()
    {
        videoClipIndex = 0;
        videoPlayer = GetComponent<VideoPlayer>();
    }


    public void Start()
    {
        videoPlayer.targetTexture.Release();
    }
    public void Update()
    {
        if (videoPlayer.isPlaying)
        {
            SetCurrentTimeUI();
            playHeadmover.MovePlayHead(CalculatePlayedFraction());
        }
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
        TotalTimeUI();
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
        TotalTimeUI();
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
            TotalTimeUI();
            //screenRenderer.material = pauseButtonMaterial;
        }


    }


    void SetCurrentTimeUI()
    {
        string minutes = Mathf.Floor((int)videoPlayer.time / 60).ToString("00");
        string seconds = ((int)videoPlayer.time % 60).ToString("00");

        currentMinutes.text = minutes;
        currentSeconds.text = seconds;
    }


    void TotalTimeUI()
    {
        string minutes = Mathf.Floor((int)videoPlayer.clip.length / 60).ToString("00");
        string seconds = ((int)videoPlayer.clip.length % 60).ToString("00");

        totalMinutes.text = minutes;
        totalSeconds.text = seconds;
    }

    double CalculatePlayedFraction()
    {
        double fraction = (double)videoPlayer.frame / (double)videoPlayer.clip.frameCount;
        return fraction;
    }



}
