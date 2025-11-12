using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Button_Poke : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 movementAxis;
    public float minDistance = 0f;
    public float maxDistance = 0.02f;

    public float resetSpeed = 5;
    public bool resetPosition = true;
    public bool freeze = true;

    [SerializeField] public UnityEvent OnButtonPressed;

    private Vector3 initialPosition;

    private Vector3 offset;
    private Transform pokeAttachTransform;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private bool isFollowing = false;

    void Start()
    {
        initialPosition = visualTarget.position;
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(StopFollowing);
    }

    void Update()
    {
        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedPosition = Vector3.Project(localTargetPosition, movementAxis);
            float distance = Vector3.Dot(constrainedPosition, movementAxis.normalized);
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            Vector3 clampedLocal = movementAxis.normalized * distance;
            visualTarget.position = visualTarget.TransformPoint(clampedLocal);
        }
        else if (resetPosition)
        {
            visualTarget.position = Vector3.Lerp(visualTarget.position, initialPosition, resetSpeed * Time.deltaTime);
        }

    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor)
        {
            UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor interactor = (UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor)hover.interactorObject;
            isFollowing = true;
            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;
        }
    }

    public void StopFollowing(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor)
        {
            isFollowing = false;
            if (visualTarget.position.y - initialPosition.y <= 0)
            {
                OnButtonPressed?.Invoke();
            }
                
        }   
    }
}
