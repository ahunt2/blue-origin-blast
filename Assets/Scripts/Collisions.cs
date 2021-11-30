using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning;
    bool collisions;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
        collisions = true;
    }

    void Update() 
    {
        RespondToDebugKeys();
    }


    void OnCollisionEnter(Collision other) {
        if (isTransitioning)
            return;

        switch (other.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Finish":
                StartNextLevel();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        if (!collisions)
            return;

        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash, 0.5f);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void StartNextLevel()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success, 0.5f);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
        
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollisions();
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void ToggleCollisions()
    {
        collisions = !collisions;
    }
}
