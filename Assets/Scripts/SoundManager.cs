using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    public enum Sound
    {
        PlayerJump,
        PlayerDie,
        Start,
        Score,
        Shield,
        Shoot,
        Explosion,
        Bird,
        UFO,
        Egg,
        Hint,
        Roar,
        Breath,
        Prepare,
        Death,
        Pop
    }

    [System.Serializable] 
    public class SoundAudioClip {
        public Sound sound;
        public AudioClip audioClip;
    }

    public SoundAudioClip[] sounds;

    private AudioSource sfxSource;   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(Sound sound)
    {
        foreach (SoundAudioClip sac in sounds)
        {
            if (sac.sound == sound)
            {
                sfxSource.PlayOneShot(sac.audioClip);
                return;
            }
        }

        Debug.LogWarning("Sound not found: " + sound);
    }



}
