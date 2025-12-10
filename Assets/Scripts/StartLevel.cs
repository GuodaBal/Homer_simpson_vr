using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public GameObject Manager;
    int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= 1)
        {
            Manager.SetActive(true);
        }
    }
    public void AddToIndex()
    {
        index++;
    }
}
