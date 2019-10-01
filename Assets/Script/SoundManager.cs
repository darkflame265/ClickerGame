using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm;
    public AudioClip coin;
    public AudioClip getItemSound0;
    public AudioClip getItemSound1;
    public AudioClip getItemSound2;

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
         /* 
        audioSource = GetComponent<AudioSource>();
        
        audioSource.clip = bgm;

        audioSource.volume = 1.0f;
        audioSource.loop = true;
        audioSource.mute = false;

        audioSource.Play();
        //audioSource.Stop();

        audioSource.playOnAwake = true;

        audioSource.priority = 0; //오디오소스중 현재 오디오 소스의 우선순위를 최우선
        */
    }

    public void click_sound()
    {
        audioSource.clip = coin;
        audioSource.volume = 0.369f;
        audioSource.Play();
    }

    public void click_get_item_sound()
    {
        audioSource.clip = getItemSound0;
        audioSource.Play();
    }

    public void click_button_sound()
    {
        audioSource.clip = getItemSound1;
        audioSource.volume = 0.123f;
        audioSource.Play();
    }

    public void upgrade_button_sound()
    {
        audioSource.clip = getItemSound2;
        audioSource.volume = 0.369f;
        audioSource.Play();
    }
}
