using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_hit_hoop : MonoBehaviour
{
    public AudioClip hitBackboardSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If there's no AudioSource component, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.clip = hitBackboardSound;
    }

    private void OnCollisionEnter(Collision collision)
    {
            // Play the hit backboard sound
            audioSource.Play();
    }
}
