using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> rngAudioSources;

    private void Start()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("RdmAudio"))
        {
            rngAudioSources.Add(o.GetComponent<AudioSource>());
        }
    }

    /// <summary>
    /// Plays a random audio clip from the list of audio sources
    /// </summary>
    protected void PlayRandomSound()
    {
        var randomIndex = Random.Range(3, rngAudioSources.Count);
        rngAudioSources[randomIndex].Play();
    }
    
    /// <summary>
    /// Play Swoochhh sound
    /// </summary>
    protected void PlaySwoochSound()
    {
        GameObject.Find("EvilSuprSound").GetComponent<AudioSource>().Play();
    }

    public static void GameOverSound()
    {
        GameObject.Find("LooseSound").GetComponent<AudioSource>().Play();
    }
    
    public static void WinSound()
    {
        GameObject.Find("WinSound").GetComponent<AudioSource>().Play();
    }
    
    
    public static void OptionsTheme()
    {
        GameObject.Find("OptionsThemes").GetComponent<AudioSource>().Play();
    }
    
    public static void GameTheme()
    {
        GameObject.Find("GamesThemes").GetComponent<AudioSource>().Play();
    }
    
}
