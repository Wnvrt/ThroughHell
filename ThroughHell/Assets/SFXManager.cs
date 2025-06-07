using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioSource sfxObject;
    public AudioSource sfxLoop;

    private AudioSource currentLoop; // <-- Add this

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioClip.length);
    }

    public void LoopSFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Only start loop if nothing is currently playing
        if (currentLoop == null)
        {
            currentLoop = Instantiate(sfxLoop, spawnTransform.position, Quaternion.identity);
            currentLoop.clip = audioClip;
            currentLoop.volume = volume;
            currentLoop.loop = true;
            currentLoop.Play();
        }
    }

    public void StopLoop()
    {
        if (currentLoop != null)
        {
            currentLoop.Stop();
            Destroy(currentLoop.gameObject);
            currentLoop = null;
        }
    }
}