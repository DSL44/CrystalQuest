using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        tutorialUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        tutorialUI.SetActive(false);
    }
}
