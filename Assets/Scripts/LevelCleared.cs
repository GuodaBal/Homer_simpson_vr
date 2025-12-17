using UnityEngine;

public class LevelCleared : MonoBehaviour
{
    public GameObject nextLevelButton;

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void TriggerGameOver()
    {
        gameObject.SetActive(true);
        nextLevelButton.SetActive(false);
    }
    public void TriggerWin()
    {
        gameObject.SetActive(true);
        nextLevelButton.SetActive(true);
    }
}
