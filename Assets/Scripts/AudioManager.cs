using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => instance;
    private static AudioManager instance;
    [SerializeField] AudioStorage _audioStorage;
    [SerializeField] AudioSource _bgm;
    [SerializeField] AudioSource _sfx;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (_bgm.clip != instance._bgm.clip)
            {
                instance._bgm.clip = _bgm.clip;  
                instance._bgm.Play(); 
            }
            Destroy(gameObject); 
        }
        
    }
    
    public void PlayBGM()
    {
        _bgm.Play();
    }

    public void PlaySFX(AudioID id)
    {
        if (_sfx.isPlaying)
        {
            _sfx.Stop();
        }

        _sfx.PlayOneShot(_audioStorage.GetAudio(id));
    }

   
}
