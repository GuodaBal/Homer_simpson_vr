using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private bool restart = false;
    [SerializeField]
    private AudioSource audioSource;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void PlaySoundEffect(AudioClip audioClip, Transform transform, float volume, float pitch)
    {
        AudioSource AS = Instantiate(audioSource, transform.position, Quaternion.identity);

        AS.clip = audioClip;

        AS.pitch = pitch;
        AS.volume = volume;
        AS.spatialBlend = 1f;
        AS.Play();

        float clipLength = AS.clip.length;

        Destroy(AS.gameObject, clipLength);

    }

    public AudioSource PlaySoundEffectWithReturn(AudioClip audioClip, Transform transform, float volume, float pitch)
    {
        AudioSource AS = Instantiate(audioSource, transform.position, Quaternion.identity);

        AS.clip = audioClip;

        AS.pitch = pitch;
        AS.volume = volume;
        AS.spatialBlend = 1f;
        AS.Play();

        float clipLength = AS.clip.length;

        Destroy(AS.gameObject, clipLength);

        return AS;

    }

}
