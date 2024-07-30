using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 2f;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private ParticleSystem succsessEffect;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip finishSound;

    private bool isTransitioning = false;
    private bool isCantColliding = false;
    private CapsuleCollider capCol;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        capCol = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        CheckingInstance();
    }

    void Update()
    {
        Cheats();
    }

    private void CheckingInstance()
    {
        if (audioSource == null || capCol == null)
        {
            audioSource = GetComponent<AudioSource>();
            capCol = GetComponent<CapsuleCollider>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || isCantColliding) { return; }

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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        explosionEffect.Play();
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
        succsessEffect.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
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

    private void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Collider Cheats active!");
            isCantColliding = !isCantColliding;
        }
    }
}
