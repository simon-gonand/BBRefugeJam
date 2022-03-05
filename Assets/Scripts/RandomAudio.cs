using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();
    [SerializeField] float pitchRandom = 0;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayAudio();
    }

    public void PlayAudio()
    {
        source.clip = clips[Random.Range(0, clips.Count)];
        source.pitch += Random.Range(-pitchRandom, pitchRandom);
        source.Play();
    }
}
