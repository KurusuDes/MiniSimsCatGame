using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> audios;

    public void TriggerSound(int audioNumber, float volume)
    {
        GameObject Sound = new GameObject("audio number " + audioNumber+". ");
        //Instantiate(Sound);
        Sound.AddComponent<AudioSource>();
        Sound.GetComponent<AudioSource>().clip = audios[audioNumber];

        Sound.GetComponent<AudioSource>().Play();
        Sound.GetComponent<AudioSource>().playOnAwake = true;
        Sound.GetComponent<AudioSource>().loop = false;
        Sound.GetComponent<AudioSource>().volume = 1;
        Destroy(Sound, audios[audioNumber].length + 0.3f);


    }
}
