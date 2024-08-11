using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;

    public AudioSource audioSource;

    private int musicIndex = 0 ;

    public AudioMixerGroup mixerGroup;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scene ");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    // Update is called once per frame
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

    public AudioSource PlayClipAt (AudioClip clip, Vector3 position) 
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
}
