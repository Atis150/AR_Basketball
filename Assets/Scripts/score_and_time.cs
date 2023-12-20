using UnityEngine;
using UnityEngine.UI;

public class score_and_time : MonoBehaviour
{
    public int score = 0;
    public float timeLimit = 30f;
    
    public Text scoreText;
    public Text timeText;
    public Button startAgainButton; // Add reference to the button in the inspector

    private float currentTime;

    public AudioClip scoreUpSound; 
    public AudioClip startAgain;
    public AudioClip gameStart;
    private AudioSource audioSource;
    private AudioSource audioSource2;
    private AudioSource audioSource3;

    private void Start()
    {
        currentTime = timeLimit;

        // Find Text components by tag
        scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
        timeText = GameObject.FindGameObjectWithTag("timeLeft").GetComponent<Text>();
        startAgainButton = GameObject.FindGameObjectWithTag("StartAgain").GetComponent<Button>();

        // Set up button click listener
        startAgainButton.onClick.AddListener(StartAgain);
        
        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.clip = scoreUpSound; 

        audioSource2 = gameObject.AddComponent<AudioSource>(); 
        audioSource2.clip = startAgain; 

        audioSource3 = gameObject.AddComponent<AudioSource>(); 
        audioSource3.clip = gameStart; 
        audioSource3.Play();

        // Update the UI with initial values
        UpdateUI();

        startAgainButton.interactable = false; // Deactivate the "StartAgain" button
    }

    private void Update()
    {
        // Update time
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            score = 0;
            PlayStartAgain();

            // Activate the "StartAgain" button
            startAgainButton.interactable = true;

            currentTime = 0f;
        }

        // Update UI every frame
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
            // Increase the score
            score++;

            // Optionally, you can perform other actions when scoring, e.g., play a sound, show a particle effect, etc.
            PlayScoreUpSound();

            // Update UI after scoring
            UpdateUI();
    }



    private void UpdateUI()
    {
        // Update the UI Text components with current values
        scoreText.text = score.ToString();
        timeText.text = currentTime.ToString("F0"); // Display time with two decimal places
    }

    private void StartAgain()
    {
        // Reset the game state
        score = 0;
        currentTime = timeLimit;
        startAgainButton.interactable = false; // Deactivate the "StartAgain" button
        UpdateUI();
        PlayStartAgain();
    }

    void PlayScoreUpSound() {
        if (scoreUpSound != null) {
            audioSource.Play(); // Play the score up sound
        } else {
            Debug.LogWarning("Score Up Sound is not assigned.");
        }
    }

    void PlayStartAgain() {
        if (scoreUpSound != null) {
            audioSource2.Play(); // Play the score up sound
        } else {
            Debug.LogWarning("Score Up Sound is not assigned.");
        }
    }
}
