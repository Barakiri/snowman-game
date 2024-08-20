using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX { GROW, SHRINK }

public class SFXManager : MonoBehaviour
{
    public List<SFXProperty> props;

    // Start is called before the first frame update
    void Start()
    {
        if (props.Count == 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var prop in props) 
        {
            //Debug.Log($"t: {prop.currentT}, start: {prop.start}, goal: {prop.goal}, volume: {prop.source.volume}");
            if (prop.source.volume != prop.goal)
            {
                if (prop.currentT > 1f) prop.currentT = 1f;

                prop.source.volume = Mathf.Lerp(prop.start, prop.goal, prop.currentT);
                prop.currentT += prop.fadeSpeed * Time.deltaTime;
            }
        }
    }

    SFXProperty FindEffect(SFX sfxType)
    {
        for (int i = 0; i < props.Count; i++)
        {
            if (props[i].sfxType == sfxType)
            {
                return props[i];
            }
        }
        return null;
    }

    public void SetVolumeGoal(SFX sfx, float volume)
    {
        FindEffect(sfx).start = FindEffect(sfx).source.volume;
        FindEffect(sfx).goal = volume;
        FindEffect(sfx).currentT = 0f;
    }
}

[Serializable]
public class SFXProperty
{
    public string name;
    public SFX sfxType;
    public AudioSource source;
    public float fadeSpeed;
    [HideInInspector] public float goal = 0f;
    [HideInInspector] public float start = 0f;
    [HideInInspector] public float currentT = 0f;

    public void SetVolume(float volume)
    {
        source.volume = volume;
    }
}