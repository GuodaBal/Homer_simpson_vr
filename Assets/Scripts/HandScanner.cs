using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HandScanner : MonoBehaviour
{
    [Header("Settings")]
    public float scanTime = 2f; // Kiek sekundžių reikia laikyti ranką
    public Renderer scannerLight; // (nebūtina) spalvos pasikeitimui

    [SerializeField] public UnityEvent OnHandScanned;

    private bool isScanning = false;
    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        // Patikrinam, ar įėjo ranka (pagal tag ar komponentą)
        if (other.CompareTag("PlayerHand") && !isScanning)
        {
            isScanning = true;
            StartCoroutine(ScanProgress());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            // Ranka išėjo – nutraukiam skanavimą
            isScanning = false;
            timer = 0f;
            if (scannerLight != null)
                scannerLight.material.color = Color.red;
        }
    }

    private IEnumerator ScanProgress()
    {
        timer = 0f;
        if (scannerLight != null)
            scannerLight.material.color = Color.yellow;

        while (isScanning && timer < scanTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (isScanning)
        {
            OnScanComplete();
        }
    }

    private void OnScanComplete()
    {
        isScanning = false;
        if (scannerLight != null)
            scannerLight.material.color = Color.green;

        Debug.Log("Rankos skanavimas baigtas!");
        // čia gali paleisti durų atidarymą, animaciją, garsą ir t.t.
        OnHandScanned?.Invoke();
    }
}
