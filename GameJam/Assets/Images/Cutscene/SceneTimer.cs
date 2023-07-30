using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    public string nextSceneName; // Name of the scene to load after the timer elapses
    public float delayInSeconds = 4.0f; // 4 seconds delay before moving to the next scene

    private bool isTimerStarted;
    private float timer;

    private void Start()
    {
        // Start the timer once the scene is fully loaded
        isTimerStarted = true;
        timer = 0f;
    }

    private void Update()
    {
        if (isTimerStarted)
        {
            timer += Time.deltaTime;

            // Check if the timer has reached the desired delay
            if (timer >= delayInSeconds)
            {
                isTimerStarted = false;
                TryLoadNextScene();
            }
        }

        // Display the timer in the Unity console
        Debug.Log("Timer: " + Mathf.Max(0f, delayInSeconds - timer).ToString("F2") + " seconds");
    }

    private void TryLoadNextScene()
    {
        // Check if the next scene exists in the build settings
        if (SceneUtility.GetBuildIndexByScenePath(nextSceneName) != -1)
        {
            // Load the next scene by its name
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("Scene change: Successful. Loading " + nextSceneName + ".");
        }
        else
        {
            Debug.LogError("Scene change: Unable to load " + nextSceneName + ". Scene name not found in build settings.");
        }
    }
}
