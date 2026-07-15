using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLevel : MonoBehaviour
{
    public GameObject winText;

    private bool finished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (finished)
            return;

        if (!other.CompareTag("Player"))
            return;

        finished = true;

        StartCoroutine(FinishGame());
    }

    IEnumerator FinishGame()
    {
        if (winText != null)
            winText.SetActive(true);

        yield return new WaitForSeconds(2f);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int lastScene = SceneManager.sceneCountInBuildSettings - 1;

        if (currentScene < lastScene)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            Debug.Log("Fim do jogo!");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}