using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public string targetTag = "Drumstick"; // Specify the tag you want to detect collisions with
    public AudioClip collisionSound; // Assign the sound clip in the Unity Editor
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Make sure the AudioSource and AudioClip are set
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
        }
        else if (collisionSound == null)
        {
            Debug.LogError("Collision sound is not assigned!");
        }
        else
        {
            // Set the AudioClip for the AudioSource
            audioSource.clip = collisionSound;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        // Check if the collided GameObject has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Play the collision sound
            audioSource.Play();
        }
    }
}
