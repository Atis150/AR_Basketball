using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreController : MonoBehaviour {

    public int Score;
    public Text ScoreText;
    public AudioClip scoreUpSound; // Add this variable to hold the score up sound
    private AudioSource audioSource; // Add this variable to hold the AudioSource component

    // Start is called before the first frame update
    void Start() {
        audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component to the GameObject
        audioSource.clip = scoreUpSound; // Set the AudioClip for the AudioSource
    }

    // Update is called once per frame
    void Update() {
        // You can add any additional update logic here if needed
    }

    private void OnTriggerEnter(Collider other) {
        AddScore();
        PlayScoreUpSound(); // Play the sound when a collision occurs
    }

    void AddScore() {
        Score++;
        ScoreText.text = Score.ToString();
    }

    void PlayScoreUpSound() {
        if (scoreUpSound != null) {
            audioSource.Play(); // Play the score up sound
        } else {
            Debug.LogWarning("Score Up Sound is not assigned.");
        }
    }
}
