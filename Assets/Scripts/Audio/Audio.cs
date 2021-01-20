using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Audio : MonoBehaviour
{
    [SerializeField]
    AudioSourceObject audioSource;

    AudioSourceObject newAudioSource;

    AudioSourceObject ambienceSource;

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

    [SerializeField]
    AudioClip melee;

    [SerializeField]
    AudioClip ranged;

    [SerializeField]
    AudioClip dash;

    [SerializeField]
    AudioClip enemyDamage;

    [SerializeField]
    AudioClip playerDamage;

    [SerializeField]
    AudioClip woodsAmbience;

    [SerializeField]
    AudioClip jungleAmbience;

    [SerializeField]
    AudioClip snowAmbience;




    float effectsVolume = 0.5f;
    float ambienceVolume = 0.5f;

    [SerializeField]
    TextMeshProUGUI effectsText;

    [SerializeField]
    TextMeshProUGUI ambienceText;

    public void adjustEffectsVolume(float i)
    {
        effectsVolume += i;

        if(effectsVolume > 1)
        {
            effectsVolume = 1;
        }
        else if(effectsVolume < 0)
        {
            effectsVolume = 0;
        }

        float visualVolume = effectsVolume * 10;
        effectsText.text = Mathf.RoundToInt(visualVolume).ToString();
    }

    public void adjustAmbienceVolume(float i)
    {
        ambienceVolume += i;

        if (ambienceVolume > 1)
        {
            ambienceVolume = 1;
        }
        else if (ambienceVolume < 0)
        {
            ambienceVolume = 0;
        }

        float visualVolume = ambienceVolume * 10;
        ambienceText.text = Mathf.RoundToInt(visualVolume).ToString();
    }


    public void spawnWoodsAmbience()
    {
        if(ambienceSource != null)
        {
            if (ambienceSource.source.clip != woodsAmbience)
            {
                Destroy(ambienceSource.gameObject);

                ambienceSource = Instantiate(audioSource);
                ambienceSource.source.clip = woodsAmbience;
                ambienceSource.source.volume = ambienceVolume;
                ambienceSource.source.loop = true;
                ambienceSource.source.Play();


                ambienceSource.transform.SetParent(this.transform);
            }
        }
        else
        {
            ambienceSource = Instantiate(audioSource);
            ambienceSource.source.clip = woodsAmbience;
            ambienceSource.source.volume = ambienceVolume;
            ambienceSource.source.loop = true;
            ambienceSource.source.Play();


            ambienceSource.transform.SetParent(this.transform);
        }

       
    }

    public void spawnJungleAmbience()
    {
        if (ambienceSource != null )
        {
            if (ambienceSource.source.clip != jungleAmbience)
            {
                Destroy(ambienceSource.gameObject);

                ambienceSource = Instantiate(audioSource);
                ambienceSource.source.clip = jungleAmbience;
                ambienceSource.source.volume = ambienceVolume;
                ambienceSource.source.loop = true;
                ambienceSource.source.Play();

                ambienceSource.transform.SetParent(this.transform);
            }
        }
        else
        {
            ambienceSource = Instantiate(audioSource);
            ambienceSource.source.clip = jungleAmbience;
            ambienceSource.source.volume = ambienceVolume;
            ambienceSource.source.loop = true;
            ambienceSource.source.Play();

            ambienceSource.transform.SetParent(this.transform);
        }

    }

    public void spawnSnowAmbience()
    {
        if (ambienceSource != null)
        {
            if (ambienceSource.source.clip != snowAmbience)
            {
                Destroy(ambienceSource.gameObject);

                ambienceSource = Instantiate(audioSource);
                ambienceSource.source.clip = snowAmbience;
                ambienceSource.source.volume = ambienceVolume;
                ambienceSource.source.loop = true;
                ambienceSource.source.Play();


                ambienceSource.transform.SetParent(this.transform);
            }
        }
        else
        {
            ambienceSource = Instantiate(audioSource);
            ambienceSource.source.clip = snowAmbience;
            ambienceSource.source.volume = ambienceVolume;
            ambienceSource.source.loop = true;
            ambienceSource.source.Play();


            ambienceSource.transform.SetParent(this.transform);
        }
    }


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

        newAudioSource.source.volume = effectsVolume;
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
        newAudioSource.source.volume = effectsVolume;
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
        newAudioSource.source.volume = effectsVolume;
        newAudioSource.source.Play();

    }

    public void playButtonTap()
    {
        newAudioSource = Instantiate(audioSource);
        newAudioSource.source.clip = buttonTap;
        newAudioSource.source.volume = effectsVolume;
        newAudioSource.source.Play();
    }

    public void playMelee()
    {
        newAudioSource = Instantiate(audioSource);
        newAudioSource.source.clip = melee;
        newAudioSource.source.volume = effectsVolume;
        newAudioSource.source.Play();
    }

    public void playRanged()
    {
        newAudioSource = Instantiate(audioSource);
        newAudioSource.source.clip = ranged;
        newAudioSource.source.volume = effectsVolume;
        newAudioSource.source.Play();
    }

    public void playDash()
    {
        newAudioSource = Instantiate(audioSource);
        newAudioSource.source.clip = dash;
        newAudioSource.source.volume = effectsVolume;
        newAudioSource.source.Play();
    }
}
