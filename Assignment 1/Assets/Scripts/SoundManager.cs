using System.Xml.Serialization;
using UnityEngine;
using System;

//tutorial: https://www.youtube.com/watch?v=g5WT91Sn3hg
public enum SoundType
{
    PICKUP,
    ROLLING
}
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode] //makes sure we always have a aduio sourcwew
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance; //reference so we can call it from anywhere
    [SerializeField] private AudioSource rollingAudioSource;
    [SerializeField] private AudioSource oneShotAudioSource;

    private void Awake()
    {
        instance = this;   
    }
    private void Start()
    {
    }
    public static void PlaySound(SoundType sound, float volume = 1) //Soundtype, volume preset 1
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.oneShotAudioSource.PlayOneShot(randomClip, volume); //gets settings of audiosource and plays a one shot clip
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }

#endif
    public static void StartRollingSound(float volume = 0.5f)
    {
        if (instance.rollingAudioSource.isPlaying)
        {
            return; // Exit if a clip is already playing
        }
        int rollingIndex = (int)SoundType.ROLLING;
        AudioClip[] rollingClips = instance.soundList[rollingIndex].Sounds;

        if (rollingClips.Length == 0) return; // Safety check

        AudioClip randomClip = rollingClips[UnityEngine.Random.Range(0, rollingClips.Length)];

        instance.rollingAudioSource.clip = randomClip;
        instance.rollingAudioSource.volume = volume;
        instance.rollingAudioSource.loop = false; // Ensure it's not looping!
        instance.rollingAudioSource.Play();
    }
    public static void StopRollingSound()
    {
        if (instance.rollingAudioSource.isPlaying)
        {
            instance.rollingAudioSource.Stop();
        }
    }
}
[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds;} //getter
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}