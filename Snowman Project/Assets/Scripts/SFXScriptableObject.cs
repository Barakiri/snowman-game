using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXScriptableObject", menuName = "ScriptableObjects/SFXScriptableObject", order = 1)]
public class SFXScriptableObject : ScriptableObject
{
    public string sfxName;
    public AudioClip sfxSource;
    public float fadeInTime;
}