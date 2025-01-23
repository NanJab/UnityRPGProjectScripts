using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Audio Source")]
    public AudioSource bgmSource; // 배경음악 audiosource
    public AudioSource sfxSource; // 효과음 audiosource

    [Header("Audio Clip")]
    public List<AudioClip> clipList; // 사운드 클립 리스트
    private Dictionary<string, AudioClip> clipDictionary; // 이름으로 클립 찾기

    [Header("UI Sliders")]
    public Slider bgmSlider; // 배경음악 슬라이더
    public Slider sfxSlider; // 효과음 슬라이더

    private void Start()
    {
        //// 슬라이더 초기값 설정
        //bgmSlider.value = bgmSource.volume; // AudioSource의 현재 볼륨 값을 가져옴
        //sfxSlider.value = sfxSource.volume;

        clipDictionary = new Dictionary<string, AudioClip>();
        foreach(var clip in clipList)
        {
            clipDictionary[clip.name] = clip;
        }

        PlayBgm("Jungle1");
    }

    public void PlayBgm(string clipName, bool Loop = true)
    {
        if(clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            bgmSource.clip = clip;
            bgmSource.loop = Loop;
            bgmSource.Play();
        }        
    }

    public void PlaySfx(string clipName)
    {
        if(clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip); // 중첩 재생 가능
        }
    }

    public void SetVolume(string name, float volume)
    {
        audioMixer.SetFloat(name, Mathf.Log10(volume) * 20);
    }
}
