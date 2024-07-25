using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hello");
                break;
            case "Hostile":
                StartCrashSequence();
                break;
            case "Fuel":
                Debug.Log("Refuel");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                break;
        }
    }

    private void StartCrashSequence()
    {
        // to do add sfx
        // to do add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    private void ReloadLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void StartFinishSequence()
    {
        // to do add sfx
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", loadDelay);
    }

    private void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int totalScene = SceneManager.sceneCountInBuildSettings;
        if (nextSceneIndex == totalScene)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
