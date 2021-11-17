using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSoundsPlayer : MonoBehaviour
{
    public AudioClip[] regularTypingSounds;
    public AudioClip backspaceTypingSound;
    public AudioClip returnTypingSound;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRegularKeySound()
    {
        AudioClip clip = GetRandomRegularClip();
        audioSource.PlayOneShot(clip);
    }

    public void PlayBackspaceKeySound()
    {
        AudioClip clip = backspaceTypingSound;
        audioSource.PlayOneShot(clip);
    }

    public void PlayReturnKeySound()
    {
        AudioClip clip = returnTypingSound;
        audioSource.PlayOneShot(clip);  
    }

    AudioClip GetRandomRegularClip()
    {
        int regularIndex = Random.Range(0, regularTypingSounds.Length);
        return regularTypingSounds[regularIndex];
    }
}
