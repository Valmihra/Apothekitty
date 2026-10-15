using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [Header("Music")] 
    public AudioClip musicMainMenu;
    public AudioClip musicPauseMenu;
    public AudioClip musicPatientWindow;
    public AudioClip musicDesk;
    public AudioClip musicHerbWall;
    public AudioClip musicResultsScreen;

    
    [Header("SFX - Menus")] 
    public AudioClip menuSave;
    public AudioClip menuSelect;
    public AudioClip menuReturn;
    public AudioClip menuNavigation; // #1?
    
    
    [Header("SFX - General")]
    public AudioClip sfxNextDialogue;
    public AudioClip sfxSceneNavigation;
    // public AudioClip sfxQuestLogOpen;
    // public AudioClip sfxQuestLogClose;
    
    
    [Header("SFX - Patient Window")]
    public AudioClip sfxFootsteps;
    // public AudioClip sfxDoorChime;
    // public AudioClip sfxCurtainOpen;
    
    
    [Header("SFX - Desk")]
    public AudioClip sfxTutorialPageGrab;
    public AudioClip sfxGrimoirePageTurn;
    public AudioClip sfxWritingOnPage;
    public AudioClip sfxStampingPage;
    // public AudioClip sfxDraggingGrimoire;
    // public AudioClip sfxDraggingForms;
    // public AudioClip sfxLiftingStamp;
    
    
    [Header("SFX - Herb Wall")]
    public AudioClip sfxDrawerOpen;
    public AudioClip sfxDrawerClose;
    
    [Space]
    
    public AudioClip sfxDirt01;
    public AudioClip sfxDirt02;
    public AudioClip sfxDirt03;
    public AudioClip sfxDirt04;
    public AudioClip sfxDirt05;
    public AudioClip sfxDirt06;
    public AudioClip sfxDirt07;
    
    // [Space]
    
    // public AudioClip sfxCompendiumPanelOpen;
    // public AudioClip sfxCompendiumPanelClose;
    
    [Header("SFX - Chatter")]
    public AudioClip chatterJimothy;
    public AudioClip chatterBarry;
    public AudioClip chatterArabella;
    public AudioClip chatterLawrence;
    
    /*[Header("SFX - Results Screen")]
     public AudioClip sfxTreatmentSuccess;
     public AudioClip sfxTreatmentFailure;
     */
    
    
    [HideInInspector] public List<AudioClip> dirtSounds;
    
    private float musicVolume = 0.45f;
    private float sfxVolume = 1.0f;
    
    
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        // sfxAudioSource = GetComponent<AudioSource>();
        
        SetVolume(musicAudioSource, musicVolume);
        SetVolume(sfxAudioSource, sfxVolume);
        musicAudioSource.loop = true;

        GenerateSFXLists();
    }

    void GenerateSFXLists()
    {
        dirtSounds = new List<AudioClip>();
        dirtSounds.Add(sfxDirt01);
        dirtSounds.Add(sfxDirt02);
        dirtSounds.Add(sfxDirt03);
        dirtSounds.Add(sfxDirt04);
        dirtSounds.Add(sfxDirt05);
        dirtSounds.Add(sfxDirt06);
        dirtSounds.Add(sfxDirt07);
    }

    void SetVolume(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
    }
    
    public void PlayAudio(AudioClip audioToPlay)
    {
        if (audioToPlay == musicMainMenu)
        {
            musicAudioSource.clip = audioToPlay;
            musicAudioSource.Play();//OneShot(audioToPlay);
            //SetVolume(musicVolume);
        }
        else
        {
            sfxAudioSource.PlayOneShot(audioToPlay);
            //SetVolume(sfxVolume);
        }
        
        
    }

    public void GetResultsScreen()
    {
        if (musicAudioSource.clip != musicResultsScreen)
        {
            SwitchInstrumental(musicResultsScreen);
        }
    }
    public void GetAudioToReturnTo()
    {
        AudioClip audioToUse = SceneManager.Instance.currentCanvasGroup == SceneManager.Instance.mainCanvasGroupDesk ? musicDesk : SceneManager.Instance.currentCanvasGroup == SceneManager.Instance.mainCanvasGroupHerbWall ? musicHerbWall : musicPatientWindow;
        SwitchInstrumental(audioToUse);
        Debug.Log("Currently playing: " + audioToUse);
    }

    public void SwitchInstrumental(AudioClip audioToPlay)
    // public void AddInstrumental(AudioClip audioToPlay)
    {
        float currentTime = musicAudioSource.time;
        
        AudioClip currentAudio = musicAudioSource.clip;
        
        // musicAudioSource.clip = audioToPlay;
        
        //musicAudioSource.time = currentTime;
        musicAudioSource.time = currentTime;
        //Mathf.Clamp(currentTime, 0f, currentAudio.length);
        musicAudioSource.clip = audioToPlay;
        musicAudioSource.Play();
        


        // PlayAudio(audioToPlay);
        // PlayAudio(currentAudio);


        //
    }

    public void GetRandomSFX(List<AudioClip> audioList)
    {
        int randomisedNumber = Random.Range(0, audioList.Count);
        PlayAudio(audioList[randomisedNumber]);
    }

    /*void SetAudioSourceTimestamp(float startingTime)
    {
        musicAudioSource.time
    }*/

    /*void PlayFromTime(float time)
    {
        //
    }*/
}
