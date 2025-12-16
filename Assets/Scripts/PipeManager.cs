using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PipeManager : MonoBehaviour
{
    [SerializeField]
    public GameObject pipe_socket_1;
    public GameObject pipe_socket_2;



    float drop_duration = 0.0f;
    int pipes_placed = 2;
    public bool are_pipes_dropped = false;

    void Start()
    {
        pipe_socket_1.SetActive(true);
        pipe_socket_2.SetActive(true);

        XRSocketInteractor socket_interactor_1 = pipe_socket_1.GetComponent<XRSocketInteractor>();
        XRSocketInteractor socket_interactor_2 = pipe_socket_2.GetComponent<XRSocketInteractor>();
        
        socket_interactor_1.selectEntered.AddListener(PipeGotPlaced);
        socket_interactor_2.selectEntered.AddListener(PipeGotPlaced);

        socket_interactor_1.selectExited.AddListener(PipeGotDropped);
        socket_interactor_2.selectExited.AddListener(PipeGotDropped);
    }

    void Update()
    {
        if (drop_duration > 0.0f)
        {
            drop_duration -= Time.deltaTime;
            if (drop_duration <= 0.0f)
            {
                pipe_socket_1.SetActive(true);
                pipe_socket_2.SetActive(true);
            }
        }
    }

    public void DropPipes()
    {

        pipe_socket_1.SetActive(false);
        pipe_socket_2.SetActive(false);
        drop_duration = 0.5f;
        are_pipes_dropped = true;
        pipes_placed = 0;
    }

    public void PipeGotPlaced(SelectEnterEventArgs args)
    {
        pipes_placed += 1;
        if (pipes_placed >= 2)
        {
            are_pipes_dropped = false;
        }
    }

    public void PipeGotDropped(SelectExitEventArgs args)
    {
        pipes_placed -= 1;
        if (pipes_placed < 2)
        {
            are_pipes_dropped = true;
        }
    }
}
