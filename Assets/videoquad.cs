using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class video : MonoBehaviour
{
    public VideoPlayer _video;

    public void mainkan()
    {
        _video.Play();
    }

    public void berhenti()
    {
        _video.Stop();
    }

    public void delay()
    {
        _video.Pause();
    }
}
