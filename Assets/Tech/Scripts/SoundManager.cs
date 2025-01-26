
using System;
using System.Collections.Generic;
using UnityEngine;

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
        Outro
    }

    [SerializeField] AudioSource MainTrack;

    [Serializable]
    public class SongPair
    {
        public Songs mySong;
        public AudioSource mySource;
    }

    [SerializeField] List<SongPair> songs = new();

    public void PlaySong(Songs aSong)
    {
        foreach(SongPair s in songs)
        {
            if(s.mySong == aSong)
            {
                MainTrack.Stop();
                MainTrack = s.mySource;
                MainTrack.Play();
                return;
            }
        }
    }
}
