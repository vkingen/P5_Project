using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//source: https://www.youtube.com/watch?v=EHKrMWGEZPU

public class RadioController : MonoBehaviour
{
    private AudioSource radioAudioSource;

    [SerializeField] Slider volumeSlider;


    public AudioClip[] audioClips;

    private int audioClipIndex;


    // Start is called before the first frame update

    private void Awake()
    {
        audioClipIndex = 0;
    }
    void Start()
    {
        radioAudioSource = GetComponent<AudioSource>();
    }


    public void SetNextClip()
    {
        audioClipIndex++;

        if (audioClipIndex >= audioClips.Length)
        {
            //videoClipIndex = videoClipIndex % videoClips.Length;
            audioClipIndex = 0;
        }

        radioAudioSource.clip = audioClips[audioClipIndex];
        radioAudioSource.Play();

    }


    public void SetPreviousClip()
    {
        audioClipIndex--;

        if (audioClipIndex < 0)
        {
            //videoClipIndex = videoClipIndex % videoClips.Length;
            audioClipIndex = 0;
        }

        radioAudioSource.clip = audioClips[audioClipIndex];
        radioAudioSource.Play();

    }

    public void PlayPause()
    {
        if (radioAudioSource.isPlaying)
        {
            radioAudioSource.Pause();
            //screenRenderer.material = playButtonMaterial;
        }
        else
        {
            radioAudioSource.clip = audioClips[audioClipIndex];
            radioAudioSource.Play();
            //screenRenderer.material = pauseButtonMaterial;
        }
    }
}
