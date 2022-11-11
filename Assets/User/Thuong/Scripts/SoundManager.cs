using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Linq;

// 音量管理クラス
[Serializable]
public class Sound
{
    public string soundName;
    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0.1f, 3f)]
    public float pitch;
    public AudioClip clip;
    public bool loop;
    //
    [HideInInspector]
    public AudioSource source;
    //
    public void Init()
    {
        
    }
}
public class SoundManager : MonoBehaviour
{ 
    public Sound[] Sounds;

    void Awake()
    {
        //if(_instance = null)
        //{
        //    _instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            if(s.clip == null)
            {
                Debug.LogWarning("don't have any sound in list sounds ! ");
                return;
            }
            else
            {
                s.source.clip = s.clip;
            }
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlayAnySound("BGM");
    }

    private void Update()
    {
        
    }
    public void PlayAnySound(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " Not found!");
            return;
        }
           
        s.source.Play();
    }
    public void StopBGM()
    {

    }
}