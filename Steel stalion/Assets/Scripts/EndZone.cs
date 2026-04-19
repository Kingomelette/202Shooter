using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public GameObject completionScreen;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            completionScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}