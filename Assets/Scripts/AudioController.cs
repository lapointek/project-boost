using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Parameters
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
  

    // Cache
    AudioSource audioSource;
   

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SuccessAudio()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
    }

    public void CrashAudio()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
    }

    // public void EngineAudio()
    // {
    //     _audioSource.PlayOneShot(_mainEngine);
    // }

    // public void StopAudio()
    // {
    //     _audioSource.Stop();
    // }
}
