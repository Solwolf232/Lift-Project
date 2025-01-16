using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundEffectDict
{
    public string npcName;
    public AudioClip audioClip;
}


public class SoundManagerScript : MonoBehaviour
{
    [Header("Sounds Effects Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource Background_sfxSource;


    [Header("Npcs")]
    [SerializeField] public List<AudioClip> soundEffects;

    [Header("Background Music")]
    private bool Test;

    [Header("Objects Sound Effects")]
    private bool Test2;

    [Header("Instance")]
    public static SoundManagerScript instance;

    #region On Start Methods
    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }
    #endregion


    #region Npcs Sound
    public void PlayNpcSpeak(AudioClip npcSound)
    {
        if (sfxSource != null) // if the sound Effect source isnt null
        {
            sfxSource.PlayOneShot(npcSound);
        }
        else
        {
            Debug.LogWarning($"Sound {sfxSource} not found!");
        }
    }


    #endregion

    #region BackgroundSounds
    public void PlayJetSound()
    {
        if (soundEffects[0] != null)
        {
            Background_sfxSource.clip = soundEffects[0];
            Background_sfxSource.loop = true;           
            Background_sfxSource.Play();
            Background_sfxSource.volume = 0.5f;

        }
    }


    #endregion

}

