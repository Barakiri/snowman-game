using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioSource> tracks;

    // Start is called before the first frame update
    void Start()
    {
        SetMusicLevel(0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTrackVolume(int track, float volume)
    {
        tracks[track].volume = volume;
    }

    public void SetMusicLevel(float t)
    {
        Mathf.Clamp01(t);

        int currentTrack = (int)(t * (float)tracks.Count);

        for (int i = 0; i < tracks.Count; i++)
        {
            if (i < currentTrack)
            {
                SetTrackVolume(i, 1f);
            }
            if (i == currentTrack)
            {
                SetTrackVolume(i, (t * (float)tracks.Count) - (int)(t * (float)tracks.Count)); 
            }
            if (i > currentTrack)
            {
                SetTrackVolume(i, 0f);
            }
        }
    }
}