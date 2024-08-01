using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        QuitPlaying();
    }

    private void QuitPlaying()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("I QUIT!");
            Application.Quit();
        }
    }
}
