using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource1;
    [SerializeField] private AudioSource _audioSource2;

    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayMusic() {
        if (_audioSource1.isPlaying && _audioSource2.isPlaying) return;
        _audioSource1.Play();
        _audioSource2.Play();
    }

    public void StopMusic() {
        _audioSource1.Stop();
        _audioSource2.Stop();
    }
}
