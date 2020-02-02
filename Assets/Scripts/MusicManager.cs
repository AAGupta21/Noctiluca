using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip UnderWater_BGMusic = null;
    [SerializeField] AudioClip SurfaceWater_BGMusic = null;
    [SerializeField] AudioClip MainMenu_BGMusic = null;

    [SerializeField] private AudioClip BubbleSound = null;
    [SerializeField] private AudioClip BeepSound = null;
    [SerializeField] private AudioClip ClickSound = null;
    [SerializeField] private AudioClip CollectingSound = null;
    
    [SerializeField] private AudioSource MainMusic = null;
    [SerializeField] private AudioSource SfxMusic = null;
    
    public void PlayUnderWaterMusic()
    {
        MainMusic.clip = UnderWater_BGMusic;
        MainMusic.Play();
    }

    public void PlaySurfaceWaterMusic()
    {
        MainMusic.clip = SurfaceWater_BGMusic;
        MainMusic.Play();
    }

    public void PlayMainMenuMusic()
    {
        MainMusic.clip = MainMenu_BGMusic;
        MainMusic.Play();
    }

    public void PlayBubbleSound()
    {
        SfxMusic.clip = BubbleSound;
        SfxMusic.Play();
    }

    public void PlayBeepSound()
    {
        SfxMusic.clip = BeepSound;
        SfxMusic.Play();
    }

    public void PlayClickSound()
    {
        SfxMusic.clip = ClickSound;
        SfxMusic.Play();
    }

    public void PlayCollectingSound()
    {
        SfxMusic.clip = CollectingSound;
        SfxMusic.Play();
    }
}