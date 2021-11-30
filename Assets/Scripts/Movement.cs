using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketbody;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    // Start is called before the first frame update
    void Start()
    {
        rocketbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
            StartThrusting();

        else
            StopThrusting();
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    private void StartThrusting()
    {
        rocketbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!mainBooster.isPlaying) mainBooster.Play();

        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rocketbody.freezeRotation = true; // freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketbody.freezeRotation = false;
    }
}
