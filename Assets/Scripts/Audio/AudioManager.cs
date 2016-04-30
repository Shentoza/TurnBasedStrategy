using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // All necessary Audio Files
    private static AudioSource endTurnSound;
    private static AudioSource grenadeSound;
    private static AudioSource launcherSound;
    private static AudioSource mainClickSound;
    private static AudioSource secondClickSound;
    private static AudioSource medikitSound;
    private static AudioSource missedSound;
    private static AudioSource molotovSound;
    private static AudioSource reloadSound;
    private static AudioSource soldierCrouch1Sound;
    private static AudioSource soldierCrouch2Sound;
    private static AudioSource soldierWalk1Sound;
    private static AudioSource soldierWalk2Sound;

    private static float pitch;

    private static System.Collections.Generic.List<AudioSource> clips;

    // Use this for initialization
    void Start ()
    {
        clips = new System.Collections.Generic.List<AudioSource>();

        endTurnSound = gameObject.AddComponent<AudioSource>();
        endTurnSound.clip = Resources.Load("Audio/endTurn1") as AudioClip;
        clips.Add(endTurnSound);

        grenadeSound = gameObject.AddComponent<AudioSource>();
        grenadeSound.clip = Resources.Load("Audio/granate") as AudioClip;
        clips.Add(grenadeSound);

        launcherSound = gameObject.AddComponent<AudioSource>();
        launcherSound.clip = Resources.Load("Audio/launcher") as AudioClip;
        clips.Add(launcherSound);

        mainClickSound = gameObject.AddComponent<AudioSource>();
        mainClickSound.clip = Resources.Load("Audio/main_click") as AudioClip;
        clips.Add(mainClickSound);


        secondClickSound = gameObject.AddComponent<AudioSource>();
        secondClickSound.clip = Resources.Load("Audio/second_click") as AudioClip;
        clips.Add(secondClickSound);

        medikitSound = gameObject.AddComponent<AudioSource>();
        medikitSound.clip = Resources.Load("Audio/medikit") as AudioClip;
        clips.Add(medikitSound);

        missedSound = gameObject.AddComponent<AudioSource>();
        missedSound.clip = Resources.Load("Audio/missed") as AudioClip;
        clips.Add(missedSound);

        molotovSound = gameObject.AddComponent<AudioSource>();
        molotovSound.clip = Resources.Load("Audio/molotov") as AudioClip;
        clips.Add(molotovSound);

        reloadSound = gameObject.AddComponent<AudioSource>();
        reloadSound.clip = Resources.Load("Audio/reload") as AudioClip;
        clips.Add(reloadSound);

        soldierCrouch1Sound = gameObject.AddComponent<AudioSource>();
        soldierCrouch1Sound.clip = Resources.Load("Audio/soldierCrouch1") as AudioClip;
        clips.Add(soldierCrouch1Sound);

        soldierCrouch2Sound = gameObject.AddComponent<AudioSource>();
        soldierCrouch2Sound.clip = Resources.Load("Audio/soldierCrouch2") as AudioClip;
        clips.Add(soldierCrouch2Sound);

        soldierWalk1Sound = gameObject.AddComponent<AudioSource>();
        soldierWalk1Sound.clip = Resources.Load("Audio/soldierWalk1") as AudioClip;
        clips.Add(soldierWalk1Sound);

        soldierWalk2Sound = gameObject.AddComponent<AudioSource>();
        soldierWalk2Sound.clip = Resources.Load("Audio/soldierWalk2") as AudioClip;
        clips.Add(soldierWalk2Sound);
    }
	
	// Update is called once per frame
	void Update ()
    {
        pitch = UnityEngine.Random.Range(0.75f, 1.25f);
    }

    public static void playEndTurn()
    {
        endTurnSound.Play();
    }

    public static void playMainClick()
    {
        mainClickSound.pitch = pitch;
        mainClickSound.Play();
    }

    public static void playSecondClick()
    {
        secondClickSound.pitch = pitch;
        secondClickSound.Play();
    }

    public static void playShootingSound(WeaponComponent weapon)
    {
        if(weapon.shootingSound != null)
        {
            weapon.shootingSound.Play();
        }
        else
        {
            Debug.Log("Diese Waffe hat noch keinen Sound, bitte verständigen Ihren nächstbesten Sounddesigner");
        }
    }

    public static void playMissed()
    {
        missedSound.Play();
    }

    public static void playMedikit()
    {
        medikitSound.Play();
    }

    public static void playReload()
    {
        reloadSound.Play();
    }

    public static void playRandomWalkingSound()
    {
        int random = UnityEngine.Random.Range(0, 4);
        switch (random)
        {
            case 0: soldierWalk1Sound.Play(); break;
            case 1: soldierWalk2Sound.Play(); break;
            case 2: soldierCrouch1Sound.Play(); break;
            case 3: soldierCrouch2Sound.Play(); break;
            default: soldierWalk1Sound.Play(); break;
        }
    }

    public static void playGrenade()
    {
        grenadeSound.Play();
    }

    public static void playMolotov()
    {
        molotovSound.Play();
    }

    public static void playSmoke()
    {

    }

    public static void playTeargasLauncher()
    {
        launcherSound.Play();
    }

    public static void changeVolume(float f)
    {
        for(int i=0; i < clips.Count; i++)
        {
            clips[i].volume = f;
        }
    }

}
