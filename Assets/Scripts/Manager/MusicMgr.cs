using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : BaseManager<MusicMgr>
{
    private AudioSource bkMusic;

    private GameObject soundObj;

    private List<AudioSource> soundList = new List<AudioSource>();

    public void PlayerBkMusic(string name)
    {
        if (bkMusic == null)
        {
            GameObject obj = new GameObject();
            obj.name = "BkMusic";
            bkMusic = obj.AddComponent<AudioSource>();
        }

        ResMgr.Instance.LoadAsync<AudioClip>("Music/BK/" + name, (clip) =>
        {
            bkMusic.clip = clip;
            bkMusic.loop = true;
            if ( GameDataMgr.Instance.MusicData.isPlayerMusic)
            {
                bkMusic.volume = GameDataMgr.Instance.MusicData.MusicVolume;
            }
            else
            {
                bkMusic.volume = 0;
            }
            bkMusic.Play();
        });
    }

    public void PauseBkMusic()
    {
        bkMusic?.Pause();
    }

    public void StopBkMusic()
    {
        bkMusic?.Stop();
    }

    public void ChangeBkValue(float value)
    {
        if (bkMusic != null)
            bkMusic.volume = value;
    }

    public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callBack)
    {
        ResMgr.Instance.LoadAsync<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            AudioSource source = GetIdleSource();
            source.clip = clip;
            source.loop = isLoop;
            if (GameDataMgr.Instance.MusicData.isPlayerSound)
            {
                source.volume = GameDataMgr.Instance.MusicData.SoundVolume;
            }
            else
            {
                source.volume = 0;
            }
            source.Play();
            soundList.Add(source);
            callBack?.Invoke(source);
        });
    }

    public AudioSource GetIdleSource()
    {
        for (int i = soundList.Count - 1; i >= 0; i --)
        {
            if (soundList[i] == null)
                soundList.RemoveAt(i);
        }

        for (int i = 0; i < soundList.Count; i++)
        {
            if (soundList[i] != null && !soundList[i].isPlaying)
            {
                return soundList[i];
            }
        }

        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }
        AudioSource source = soundObj.AddComponent<AudioSource>();
        return source;
    }

    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            source.Stop();
        }
    }

    public void ChangeSoundValue(float value)
    {
        for (int i = 0; i < soundList.Count; i++)
        {
            if (soundList[i] != null)
            {
                soundList[i].volume = value;
            }
        }
    }
}
