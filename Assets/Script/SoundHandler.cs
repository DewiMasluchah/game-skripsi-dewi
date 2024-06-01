using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] sounds;

    int counter = 0;

    public void ChangeAudio()
    {
        if (counter == sounds.Length - 1)
            counter = 0;
        else
            counter++;


    }

    public void PlaySound()
    {
        if (source.isPlaying)
            source.Stop(); 

        source.PlayOneShot(source.clip = sounds[counter]);
    }

}
