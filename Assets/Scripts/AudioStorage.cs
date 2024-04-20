using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AudioID
{
    Intro,
    Main,
    Correct,
}

[CreateAssetMenu]
public class AudioStorage : ScriptableObject
{
    [SerializeField] AudioSrc[] audioSrcs;

    Dictionary<AudioID, AudioClip> audioDictionary = new Dictionary<AudioID, AudioClip>();

    void GenerateDictionary()
    {
        for (int i = 0; i < audioSrcs.Length; i++)
        {
            audioDictionary.Add(audioSrcs[i].audioId, audioSrcs[i].audioFile);
        }
    }

    public AudioClip GetAudio(AudioID id)
    {
        Debug.Assert(audioSrcs.Length > 0, "No soundData!!");

        if (audioDictionary.Count.Equals(0))
        {
            GenerateDictionary();
        }

        if (audioDictionary.ContainsKey(id))
        {
            return audioDictionary[id];
        }
        else
        {
            return null;
        }
    }
}

[Serializable]
public struct AudioSrc
{
    [SerializeField] AudioClip _audioFile;
    [SerializeField] AudioID _audioId;

    public AudioClip audioFile { get { return _audioFile; } }
    public AudioID audioId { get { return _audioId; } }
}
