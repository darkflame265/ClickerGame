using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm;


    private static SoundManager instance;

    public static SoundManager Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if(instance == null)
                {
                    GameObject container = new GameObject("SoundManager");

                    instance = container.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }


    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.clip = bgm;

        audioSource.volume = 0.123f;
        audioSource.loop = true;
        audioSource.mute = false;

        audioSource.Play();
        //audioSource.Stop();

        audioSource.playOnAwake = true;

        audioSource.priority = 0; //오디오소스중 현재 오디오 소스의 우선순위를 최우선
        
    }

}
