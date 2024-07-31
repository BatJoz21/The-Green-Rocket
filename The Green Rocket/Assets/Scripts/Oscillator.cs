using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2f;

    private float movementFactor;
    private Vector3 startingPosition;
    private Vector3 movingPosition;

    void Start()
    {
        startingPosition = transform.position;
        movingPosition = startingPosition;
    }

    void Update()
    {
        Oscillation();
    }

    private void Oscillation()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //continually growing over time
        const float tau = Mathf.PI * 2; //constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; //recalculate from 0 to 1 so its cleaner
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
