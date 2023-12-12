using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//source: https://www.youtube.com/watch?v=EHKrMWGEZPU

public class RadioController : MonoBehaviour
{
    private AudioSource radioAudioSource;

    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        radioAudioSource = GetComponent<AudioSource>(); 
    }

    public void PlayAudio()
    {
        radioAudioSource.Play();
    }

    public void PauseAudio()
    {
        radioAudioSource.Pause();
    }
}
