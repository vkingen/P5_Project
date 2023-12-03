using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumStickThatWorks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public AudioSource[] drumAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drum"))
        {
            int drumIndex = System.Array.IndexOf(drumAudio, other.GetComponent<AudioSource>()); // Finding the index of the drum in the drum set

            if (drumIndex >= 0 && drumIndex < drumAudio.Length) // Checking if the drum index is valid

            {


                drumAudio[drumIndex].Play();  // Triggers the drum sound
            }
        }

    }

}