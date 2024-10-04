using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioClip bouncingAroundInPixelTown; // Nouveau clip audio pour "Bouncing Around in Pixel Town"

    public AudioSource audioSource;

    private int musicIndex = 0;

    public AudioMixerGroup mixerGroup;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène ");
            return;
        }
        instance = this;
    }

    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            playNexSong();
        }
    }

    public void playNexSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 position)
    {
        GameObject tempGo = new GameObject("TempAudio");
        tempGo.transform.position = position;
        AudioSource audioSource = tempGo.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.Play();
        Destroy(tempGo, clip.length);
        return audioSource;
    }

    // Nouvelle méthode pour jouer la musique "Bouncing Around in Pixel Town"
    public void PlayBouncingAroundInPixelTown()
    {
        audioSource.clip = bouncingAroundInPixelTown;
        audioSource.Play();
    }
}
