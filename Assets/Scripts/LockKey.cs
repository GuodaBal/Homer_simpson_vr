using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LockKey : MonoBehaviour
{
    [Header("References")]
    public XRSocketInteractor socket;
    public Rigidbody lockBody;
    public XRGrabInteractable keyGrab;
    public Transform keyAttachPoint;
    public Animator KeyRotation;
    public float delay = 1f;
    private int index = 0;

    [SerializeField] public UnityEvent OnKeyTurned;

    [SerializeField]
    AudioClip audio;
    
    public void Update()
    {
        if (keyGrab.isSelected)
        {
            socket.enabled = true;
            KeyRotation.enabled = false;
            Console.WriteLine("paimtas");
        }
    }
    public void OnKeyInserted(SelectEnterEventArgs args)
    {
        //Wait();        
        StartCoroutine(PlayAnimationNextFrame());
    }

    public void OnKeyRemoved(SelectExitEventArgs args)
    {
        KeyRotation.SetBool("IsInserted", false);
        KeyRotation.enabled = false;
        Rigidbody rb = keyGrab.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        index = 0;
        
    }

    private IEnumerator PlayAnimationNextFrame()
    {
        index++;
        if (index == 1)
        {
            Rigidbody rb = keyGrab.GetComponent<Rigidbody>();

            // Palaukiame vieną frame, kol XR snap baigs pozicionuoti objektą
            yield return new WaitForSeconds(delay);
            AudioManager.instance.PlaySoundEffect(audio, keyAttachPoint, 1f, 1f);
            socket.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            KeyRotation.enabled = true;
            // Dabar saugiai paleidžiame animaciją
            KeyRotation.SetTrigger("IsInserted");
            yield return new WaitForSeconds( 1.5f);

            rb.constraints = RigidbodyConstraints.None;
            socket.enabled = true;
            index = -1;
            OnKeyTurned?.Invoke();
        }

    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.05f);
    }

}
