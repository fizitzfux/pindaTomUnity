using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class AudioHandler : NetworkBehaviour
{
    //Variables
    private AudioSource Audio;
    private bool donePlaying = true;
    private int step = 0;
    public string Object;
    //References
    public AudioClip[] AudioClips;
    public GameObject SoundObjectPrefab;


    void Start()
    {
        Audio = GetComponent<AudioSource>();
        // If audio for scene, get scenename
        if (Object == "scene") Object = SceneManager.GetActiveScene().name;
    }


    void Update()
    {
        if (Object == "MainMenu" && step < 2)
        {
            if (step == 0)
            {
                donePlaying = false;
                // Start audio
                Audio.clip = AudioClips[0];
                Audio.GetComponent<AudioSource>().Play();

                StartCoroutine(waitAudio(Audio.clip));
                step = 1;
            }
            // When done with audio 1, play audio 2
            if (step == 1 && donePlaying)
            {
                donePlaying = false;
                Audio.clip = AudioClips[1];
                Audio.loop = true;
                Audio.GetComponent<AudioSource>().Play();

                step = 2;
            }
        }
    }

    // Waits for Audio.length amount of time
    private IEnumerator waitAudio(AudioClip Audio, GameObject Object = null)
    {
        yield return new WaitForSeconds(Audio.length);
        // After track destroy Object
        donePlaying = true;
        if (Object != null) Destroy(Object);
    }

    // Call to server to play track at pos to all players
    [ServerRpc(RequireOwnership = false)]
    private void AudioServerRpc(int track, Vector3 pos)
    {
        AudioClientRpc(track, pos);
    }

    // Call to all clients from server to play track at pos
    [ClientRpc]
    private void AudioClientRpc(int track, Vector3 pos)
    {
        GameObject AudioPlayerObject = Instantiate(SoundObjectPrefab, pos, Quaternion.identity);
        AudioSource AudioObject = AudioPlayerObject.GetComponent<AudioSource>();

        AudioObject.clip = AudioClips[track];
        AudioObject.GetComponent<AudioSource>().Play();

        StartCoroutine(waitAudio(AudioObject.clip, AudioPlayerObject));
    }
    
    // Public function for playermovement soundeffects
    public void PlayerSound(string sound)
    {
        if (donePlaying)
        {
            donePlaying = false;

            if (sound == "walk")
            {
                AudioServerRpc(0, this.transform.position);
            }
            else if (sound == "run")
            {
                AudioServerRpc(1, this.transform.position);
            }
        }
    }

    // Public function for setting global application volume
    public void SetApplicationVolume(float value)
    {
        AudioListener.volume = value;
    }
}

