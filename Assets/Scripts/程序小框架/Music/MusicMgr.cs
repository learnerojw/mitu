using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : SingletonMono<MusicMgr>
{
    private GameObject BKGameObject = null;
    private AudioSource bkMusic;
    private float bkValue = 1;

    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();
    private float soundValue = 1;


    private void Update()
    {
        CheckMusicPlay();
    }

    void CheckMusicPlay()
    {
        for (int i = soundList.Count - 1; i >= 0; --i)
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
            BKGameObject= new GameObject();
            BKGameObject.name = "BKMusic";
            bkMusic = BKGameObject.AddComponent<AudioSource>();
        }
        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/BK/" + name, (clip) =>
           {
               bkMusic.clip = clip;
               bkMusic.volume = bkValue;
               bkMusic.loop = true;
               bkMusic.Play();
           }
        );
    }

    public void ChangeBKMusic(float v)
    {
        bkValue = v;
        if (bkMusic == null)
            return;
        bkMusic.volume = bkValue;
    }
    public void PauseBKMusic()
    {
        if(bkMusic==null)
        {
            return;
        }
        bkMusic.Pause();
    }


    public void StopBKMusic()
    {
        if(bkMusic==null)
        {
            return;
        }
        bkMusic.Stop();
    }

    public void PlayeSound(string name,bool isLoop,UnityAction<AudioSource> callback=null)
    {
        if(soundObj==null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }
        

        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/Forest/" + name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = soundValue;
            source.loop = isLoop;
            source.Play();
            source.name = name;
            soundList.Add(source);
            if(callback!=null)
            {
                callback(source);
            }
        }
        );
        
    }

    public void ChangsoundMusic(float v)
    {
        soundValue = v;
        if (soundObj == null)
            return;
        foreach (AudioSource item in soundList)
        {
            item.volume = v;
        }
    }


    public void StopSound(string name)
    {
        ///Debug.Log(soundList.Count);
        
        foreach (AudioSource item in soundList)
        {
            if(item.name==name)
            {
                Destroy(item);
                soundList.Remove(item);
                item.Stop();
                StopSound(name);//递归删除name的音源组件
                return;
            }
        }
    }

}
