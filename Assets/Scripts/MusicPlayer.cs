using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PersistentSingleton))]
public class MusicPlayer : MonoBehaviour
{

    [SerializeField] List<AudioClip> tracks = new List<AudioClip>();
    AudioSource audioSource;

    int trackIndex = 0;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    void Update() {
        if (!audioSource.isPlaying) {
            PlayNext();
        }
    }

    public void Restart() {
        trackIndex = 0;
        audioSource.clip = tracks[trackIndex];
        audioSource.Play();       
    }

    public void Play(AudioClip track) {
        audioSource.clip = track;
        audioSource.Play();
    }

    public void Play(int index) {
        if (0 <= index && tracks.Count > index) {
            audioSource.clip = tracks[index];
            audioSource.Play();
        }
    }

    public void PlayNext() {
        IncrementTrackIndex();
        audioSource.clip = tracks[trackIndex];
        audioSource.Play();
    }

    void IncrementTrackIndex() {
        trackIndex += 1;
        if (trackIndex >= tracks.Count) {
            trackIndex = 0;
        }
    }

    void DecrementTrackIndex() {
        trackIndex -= 1;
        if (trackIndex < 0) {
            trackIndex = tracks.Count - 1;
        }
    }

}
