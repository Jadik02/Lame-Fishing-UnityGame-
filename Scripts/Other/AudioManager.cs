using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager: MonoBehaviour
{
    [Header("--------------Audio Source--------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("--------------Audio Clip----------------")]
    public AudioClip background;
    public AudioClip WonFish;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.pitch = (Random.Range(0.5f, 1.5f));
        SFXSource.PlayOneShot(clip);
    }
}

