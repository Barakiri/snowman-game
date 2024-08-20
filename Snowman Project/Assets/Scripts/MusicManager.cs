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
            if (i > currentTrack )
            {
                SetTrackVolume(i, 0f);
            }
        }

        //Behemoth of a Debug.Log statement for debugging music, keeping so I don't have to retype it later
        //Debug.Log($"T: {t}\nCurrent Track: {currentTrack}\nCurrent Float: {t * (float)tracks.Count}\nTrack 1: {tracks[0].volume}\nTrack 2: {tracks[1].volume}\nTrack 3: {tracks[2].volume}\nTrack 4: {tracks[3].volume}\nTrack 5: {tracks[4].volume}\nTrack 6: {tracks[5].volume}\nTrack 7: {tracks[6].volume}\n");

    }
}