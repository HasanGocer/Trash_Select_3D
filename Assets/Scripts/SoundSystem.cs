using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] private AudioSource _mainSource;
    [SerializeField] private AudioClip _mainMusic, _bloomEffect, _goldEffect;

    public void MainMusicPlay()
    {
        _mainSource.clip = _mainMusic;
        _mainSource.Play();
    }

    public void MainMusicStop()
    {
        _mainSource.Stop();
    }

    public void EffectCall()
    {
        _mainSource.PlayOneShot(_bloomEffect);
    }
    public void EffectGoldCall()
    {
        _mainSource.PlayOneShot(_goldEffect);

    }
}
