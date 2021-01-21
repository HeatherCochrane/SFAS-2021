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
    AudioClip jump;

    [SerializeField]
    AudioClip drop;

    [SerializeField]
    AudioClip eat;

    [SerializeField]
    AudioClip pickup;

    [SerializeField]
    AudioClip bossDeath;

    [SerializeField]
    AudioClip buttonHighlight;

    [SerializeField]
    AudioClip woodsAmbience;

    [SerializeField]
    AudioClip jungleAmbience;

    [SerializeField]
    AudioClip playerDeath;

    [SerializeField]
    AudioClip snowAmbience;

    [SerializeField]
    List<AudioClip> enemyDeath = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> coins = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> grassFootsteps = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> snowFootsteps = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> woodFootsteps = new List<AudioClip>();


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

    public void Ambience(AudioSource s, AudioClip c)
    {
        s.clip = c;
        s.volume = ambienceVolume;
        s.loop = true;
        s.Play();

        s.transform.SetParent(this.transform);
    }

    public void EffectNormal(AudioSource s, AudioClip c)
    {
        s.clip = c;
        s.volume = effectsVolume;
        s.Play();
    }

    public void EffectPitched(AudioSource s, AudioClip c)
    {
        s.clip = c;
        s.volume = effectsVolume;
        s.pitch = Random.Range(0.8f, 1.2f);
        s.Play();
    }

    public void spawnWoodsAmbience()
    {
        if(ambienceSource != null)
        {
            if (ambienceSource.source.clip != woodsAmbience)
            {
                Destroy(ambienceSource.gameObject);

                ambienceSource = Instantiate(audioSource);

                Ambience(ambienceSource.source, woodsAmbience);
            }
        }
        else
        {
            ambienceSource = Instantiate(audioSource);
            Ambience(ambienceSource.source, woodsAmbience);
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
                Ambience(ambienceSource.source, jungleAmbience);
            }
        }
        else
        {
            ambienceSource = Instantiate(audioSource);
            Ambience(ambienceSource.source, jungleAmbience);

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
                Ambience(ambienceSource.source, snowAmbience);
            }
        }
        else
        {
            ambienceSource = Instantiate(audioSource);
            Ambience(ambienceSource.source, snowAmbience);
        }
    }

    public void playGrassStep()
    {
        newAudioSource = Instantiate(audioSource);
        EffectPitched(newAudioSource.source, grassFootsteps[Random.Range(0, grassFootsteps.Count)]);
    }

    public void playSnowStep()
    {
        newAudioSource = Instantiate(audioSource);
        EffectPitched(newAudioSource.source, snowFootsteps[Random.Range(0, snowFootsteps.Count)]);
    }

    public void playWoodStep()
    {
        newAudioSource = Instantiate(audioSource);
        EffectPitched(newAudioSource.source, woodFootsteps[Random.Range(0, woodFootsteps.Count)]);
    }

    public void playEnemyHit()
    {
        newAudioSource = Instantiate(audioSource);

        EffectNormal(newAudioSource.source, enemyDamage);
    }

    public void playPlayerHit()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, playerDamage);
    }

    public void playJump()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, jump);
    }

    public void playCoins()
    {
        newAudioSource = Instantiate(audioSource);
        EffectPitched(newAudioSource.source, coins[Random.Range(0, coins.Count)]);
    }

    public void playDrop()
    {
        newAudioSource = Instantiate(audioSource);
        EffectPitched(newAudioSource.source, drop);
    }

    public void playEat()
    {
        newAudioSource = Instantiate(audioSource);
        EffectPitched(newAudioSource.source, eat);
    }

    public void playPickup()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, pickup);
    }

    public void playEnemyDeath()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, enemyDeath[Random.Range(0, enemyDeath.Count)]);
    }

    public void playBossDeath()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, bossDeath);
    }

    public void playHighlightButton()
    {
        newAudioSource = Instantiate(audioSource);
        EffectPitched(newAudioSource.source, buttonHighlight);
    }

    public void playPlayerDeath()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, playerDeath);
    }

    public void playInventory(bool open)
    {
        newAudioSource = Instantiate(audioSource);

        if (open)
        {
            EffectNormal(newAudioSource.source, inventoryOpen);
        }
        else
        {
            EffectNormal(newAudioSource.source, inventoryClose);
        }
    }
    public void playDialogue(bool open)
    {
        newAudioSource = Instantiate(audioSource);

        if (open)
        {
            EffectNormal(newAudioSource.source, dialogueOpen);
        }
        else
        {
            EffectNormal(newAudioSource.source, dialogueClose);
        }
    }

    public void playMap(bool open)
    {
        newAudioSource = Instantiate(audioSource);

        if (open)
        {
            EffectNormal(newAudioSource.source, mapOpen);
        }
        else
        {
            EffectNormal(newAudioSource.source, mapClose);
        }

    }

    public void playButtonTap()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, buttonTap);
    }

    public void playMelee()
    {
        newAudioSource = Instantiate(audioSource);
        EffectPitched(newAudioSource.source, melee);
    }

    public void playRanged()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, ranged);
    }

    public void playDash()
    {
        newAudioSource = Instantiate(audioSource);
        EffectNormal(newAudioSource.source, dash);
    }
}
