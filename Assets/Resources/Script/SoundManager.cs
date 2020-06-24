using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static AudioSource AudioSrc;

    void Start()
    {
        AudioSrc = GetComponent<AudioSource>();
    }
    public static void playSoundMark()
    {
        AudioSrc.clip = Resources.Load<AudioClip>("Sounds/Mark");
        AudioSrc.Play();
    }
    public static void playSoundTarget(int n)
    {
        switch (n)
        {
            case 7:
                AudioSrc.clip = Resources.Load<AudioClip>("Sounds/ChotAn");
                AudioSrc.Play();
                break;
            case 3:
                AudioSrc.clip = Resources.Load<AudioClip>("Sounds/PhaoAn");
                AudioSrc.Play();
                break;
            case 2:
                AudioSrc.clip = Resources.Load<AudioClip>("Sounds/XeAn");
                AudioSrc.Play();
                break;
            case 4:
                AudioSrc.clip = Resources.Load<AudioClip>("Sounds/MaAn");
                AudioSrc.Play();
                break;
            case 5:
                AudioSrc.clip = Resources.Load<AudioClip>("Sounds/TinhAn");
                AudioSrc.Play();
                break;
            case 6:
                AudioSrc.clip = Resources.Load<AudioClip>("Sounds/SyAn");
                AudioSrc.Play();
                break;
            case 1:
                AudioSrc.clip = Resources.Load<AudioClip>("Sounds/TuongAn");
                AudioSrc.Play();
                break;
        }
    }
    public static void playSoundReady()
    {
        AudioSrc.clip = Resources.Load<AudioClip>("Sounds/Ready");
        AudioSrc.Play();
    }
    public static void playSoundChieu()
    {
        AudioSrc.clip = Resources.Load<AudioClip>("Sounds/Chieu");
        AudioSrc.Play();
    }
}
