using UnityEngine;
using UnityEngine.SceneManagement;

public class castel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        int nextSceneIndex = 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
