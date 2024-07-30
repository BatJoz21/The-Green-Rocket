using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float thrustPower;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ParticleSystem mainThrustParticle;
    [SerializeField] private ParticleSystem leftThrustParticle;
    [SerializeField] private ParticleSystem rightThrustParticle;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mainEngine;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        CheckingInstance();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void CheckingInstance()
    {
        if (rb == null || audioSource == null)
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustParticle.isPlaying)
        {
            mainThrustParticle.Play();
        }
    }

    private void StopThrusting()
    {
        mainThrustParticle.Stop();
        audioSource.Stop();
    }

    private void RotateLeft()
    {
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
        ApplyRotation(rotateSpeed);
    }

    private void RotateRight()
    {
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
        ApplyRotation(-rotateSpeed);
    }

    private void StopRotating()
    {
        leftThrustParticle.Stop();
        rightThrustParticle.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can rotate freely
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so physics system can take over
    }
}
