using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField]
    AudioSourceObject audioSource;

    AudioSourceObject newAudioSource;

    [SerializeField]
    AudioClip inventoryOpen;

    [SerializeField]
    AudioClip inventoryClose;

    [SerializeField]
    AudioClip dialogueOpen;

    [SerializeField]
    AudioClip dialogueClose;

    [SerializeField]
    AudioClip mapOpen;

    [SerializeField]
    AudioClip mapClose;

    [SerializeField]
    AudioClip buttonTap;

    public void playInventory(bool open)
    {
        newAudioSource = Instantiate(audioSource);

        if (open)
        {
            newAudioSource.source.clip = inventoryOpen;
        }
        else
        {
            newAudioSource.source.clip = inventoryClose;
        }

        newAudioSource.source.Play();

    }
    public void playDialogue(bool open)
    {
        newAudioSource = Instantiate(audioSource);

        if (open)
        {
            newAudioSource.source.clip = dialogueOpen;
        }
        else
        {
            newAudioSource.source.clip = dialogueClose;
        }

        newAudioSource.source.Play();

    }

    public void playMap(bool open)
    {
        newAudioSource = Instantiate(audioSource);

        if (open)
        {
            newAudioSource.source.clip = mapOpen;
        }
        else
        {
            newAudioSource.source.clip = mapClose;
        }

        newAudioSource.source.Play();

    }

    public void playButtonTap()
    {
        newAudioSource = Instantiate(audioSource);
        newAudioSource.source.clip = buttonTap;
        newAudioSource.source.Play();
    }


}
