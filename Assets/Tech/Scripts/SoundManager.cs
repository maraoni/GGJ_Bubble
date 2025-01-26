
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public enum Songs
    {
        MainMenu,
        CustommersEnter,
        Playing,
        Outro,
        Disco,
        Lost
    }

    [SerializeField] AudioSource MainTrack;

    [Serializable]
    public class SongPair
    {
        public Songs mySong;
        public AudioClip mySource;
    }

    [SerializeField] List<SongPair> songs = new();

    [SerializeField] Slider myVolumeSlider;

    private void Start()
    {
        myVolumeSlider.onValueChanged.AddListener(delegate { AdjustVolume(); });
    }

    public void PlaySong(Songs aSong)
    {
        foreach (SongPair s in songs)
        {
            if (s.mySong == aSong)
            {
                MainTrack.Stop();
                MainTrack.clip = s.mySource;
                MainTrack.Play();
                return;
            }
        }
    }

    public void AdjustVolume()
    {
        AudioListener.volume = myVolumeSlider.value;
    }

    public void StopSong()
    {
        MainTrack.Stop();
    }
}
