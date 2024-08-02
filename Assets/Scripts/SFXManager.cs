using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    public void PlayAudio(string clipName)
    {
        audioSource.clip = audioClips.FirstOrDefault(c => c.name == clipName);
        audioSource.Play();
    }
}
