using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hello");
                break;
            case "Hostile":
                Debug.Log("Explode");
                break;
            case "Fuel":
                Debug.Log("Refuel");
                break;
            case "Finish":
                Debug.Log("Next level");
                break;
            default:
                break;
        }
    }
}
