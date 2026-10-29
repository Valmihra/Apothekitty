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
        
        musicAudioSource.volume = musicVolume;
        sfxAudioSource.volume = sfxVolume;
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

    /*void SetVolume(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
    }*/
    
    public void PlayAudio(AudioClip audioToPlay)
    {
        // Determines whether to play a sound effect or a music track, and chooses the correct audio source
        if (audioToPlay == musicMainMenu)
        {
            musicAudioSource.clip = audioToPlay;
            musicAudioSource.Play();
        }
        else
        {
            sfxAudioSource.PlayOneShot(audioToPlay);
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
        // Used when returning to the game from the pause screen
        // Uses the current active canvas in SceneManager to determine which audio to play
        AudioClip audioToUse = SceneManager.Instance.currentCanvasGroup == SceneManager.Instance.mainCanvasGroupDesk ? musicDesk : SceneManager.Instance.currentCanvasGroup == SceneManager.Instance.mainCanvasGroupHerbWall ? musicHerbWall : musicPatientWindow;
        SwitchInstrumental(audioToUse);
    }

    public void SwitchInstrumental(AudioClip audioToPlay)
    {
        // Used to switch between different tracks depending on the current active screen in-game 
        float currentTime = musicAudioSource.time;
        
        AudioClip currentAudio = musicAudioSource.clip;
        musicAudioSource.time = currentTime;
        
        musicAudioSource.clip = audioToPlay;
        musicAudioSource.Play();
        
    }

    public void GetRandomSFX(List<AudioClip> audioList)
    {
        int randomisedNumber = Random.Range(0, audioList.Count);
        PlayAudio(audioList[randomisedNumber]);
    }
}
