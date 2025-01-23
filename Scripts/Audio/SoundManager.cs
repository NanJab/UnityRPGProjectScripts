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
    public AudioSource bgmSource; // ������� audiosource
    public AudioSource sfxSource; // ȿ���� audiosource

    [Header("Audio Clip")]
    public List<AudioClip> clipList; // ���� Ŭ�� ����Ʈ
    private Dictionary<string, AudioClip> clipDictionary; // �̸����� Ŭ�� ã��

    [Header("UI Sliders")]
    public Slider bgmSlider; // ������� �����̴�
    public Slider sfxSlider; // ȿ���� �����̴�

    private void Start()
    {
        //// �����̴� �ʱⰪ ����
        //bgmSlider.value = bgmSource.volume; // AudioSource�� ���� ���� ���� ������
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
            sfxSource.PlayOneShot(clip); // ��ø ��� ����
        }
    }

    public void SetVolume(string name, float volume)
    {
        audioMixer.SetFloat(name, Mathf.Log10(volume) * 20);
    }
}
