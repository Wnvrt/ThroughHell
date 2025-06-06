using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMasterVol(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level)*20f);
    }
    public void SetSFXVol(float level)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(level) * 20f);
    }
    public void SetMusicVol(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }
}
