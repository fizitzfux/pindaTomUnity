using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class AudioHandler : NetworkBehaviour
{
    private AudioSource Audio;
    public string Object;
    public AudioClip[] AudioClips;
    public GameObject SoundObjectPrefab;

    private bool donePlaying = true;
    private int step = 0;


    void Start()
    {
        Audio = GetComponent<AudioSource>();

        if (Object == "scene") Object = SceneManager.GetActiveScene().name;
    }


    void Update()
    {
        if (Object == "MainMenu" && step < 2)
        {
            if (step == 0)
            {
                donePlaying = false;

                Audio.clip = AudioClips[0];
                Audio.GetComponent<AudioSource>().Play();

                StartCoroutine(waitAudio(Audio.clip));
                step = 1;
            }
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

    private IEnumerator waitAudio(AudioClip Audio, GameObject Object = null)
    {
        yield return new WaitForSeconds(Audio.length);
        donePlaying = true;
        if (Object != null) Destroy(Object);
    }

    [ServerRpc(RequireOwnership = false)]
    public void AudioServerRpc(int track, Vector3 pos)
    {
        AudioClientRpc(track, pos);
    }

    [ClientRpc]
    public void AudioClientRpc(int track, Vector3 pos)
    {
        GameObject AudioPlayerObject = Instantiate(SoundObjectPrefab, pos, Quaternion.identity);
        AudioSource AudioObject = AudioPlayerObject.GetComponent<AudioSource>();

        AudioObject.clip = AudioClips[track];
        AudioObject.GetComponent<AudioSource>().Play();

        StartCoroutine(waitAudio(AudioObject.clip, AudioPlayerObject));
    }
    
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

    public void SetApplicationVolume(float value)
    {
        AudioListener.volume = value;
    }
}

