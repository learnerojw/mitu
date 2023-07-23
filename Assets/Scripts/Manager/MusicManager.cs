using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private GameObject BKGameObject=null;
    private AudioSource bkMusic;
    private float bkValue = 1;


    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();
    private float soundValue = 0.5f;


    private void Awake()
    {
        
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }

            
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        CheckMusicPlay();
    }


    void CheckMusicPlay()
    {
        for (int i = soundList.Count - 1; i >= 0; i--)
        {
            if (soundList[i].isPlaying == false)
            {
                Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    public void PlayBKMusic(string name)
    {
        if(BKGameObject==null)
        {
            BKGameObject = new GameObject();
            BKGameObject.name = "BKMusic";
            bkMusic = BKGameObject.AddComponent<AudioSource>();
        }
        ResManager.instance.LoadAsync<AudioClip>("Music/BK/" + name, (clip) =>
           {
               bkMusic.clip = clip;
               bkMusic.volume = bkValue;
               bkMusic.loop = true;
               bkMusic.Play();
           });
    }

    public void PlaySound(string name,bool isLoop,Action<AudioSource> callback=null)
    {
        if(soundObj==null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }

        ResManager.instance.LoadAsync<AudioClip>("Music/Sound/" + name, (clip) =>
           {
               AudioSource audioSource = soundObj.AddComponent<AudioSource>();
               audioSource.clip = clip;
               audioSource.name = name;
               audioSource.loop = isLoop;
               audioSource.volume = soundValue;
               audioSource.Play();
               
               soundList.Add(audioSource);
               
               if (callback!=null)
               {
                   callback(audioSource);
               }

           });
    }




}
