using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float thrustPower;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private AudioSource thrustAudio;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
            if (!thrustAudio.isPlaying)
            {
                thrustAudio.Play();
            }
        }
        else
        {
            thrustAudio.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can rotate freely
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so physics system can take over
    }
}
