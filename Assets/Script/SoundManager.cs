using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm;
    public AudioClip coin;
    public AudioClip getItemSound0;
    public AudioClip getItemSound1;
    public AudioClip getItemSound2;
    public AudioClip getChallengeReward;

    public GameObject bgm_controller;

    public GameObject bgm_btn;
    public GameObject effect_btn;


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
        if(PlayerPrefs.GetInt("bgmState") == 1)
        {
            bgm_controller.GetComponent<AudioSource>().volume = 1f;
            bgm_btn.GetComponent<Image>().color = new Color(1f, 1f, 1f);

        } else {
            bgm_controller.GetComponent<AudioSource>().volume = 0f;
            bgm_btn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        } 

        if(PlayerPrefs.GetInt("effectState") == 1)
        {
           this.GetComponent<AudioSource>().mute = false;
            effect_btn.GetComponent<Image>().color = new Color(1f, 1f, 1f);

        } else {
            this.GetComponent<AudioSource>().mute = true;
            effect_btn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        } 
    }

    public void on_off_bgm()
    {
        if(PlayerPrefs.GetInt("bgmState") == 1)
        {
            bgm_controller.GetComponent<AudioSource>().volume = 0f;
            bgm_btn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            PlayerPrefs.SetInt("bgmState", 0);
        } else {
            bgm_controller.GetComponent<AudioSource>().volume = 1f;
            bgm_btn.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            PlayerPrefs.SetInt("bgmState", 1);
        } 
    }

    public void on_off_effect_sound()
    {
        if(PlayerPrefs.GetInt("effectState") == 1)
        {
            this.GetComponent<AudioSource>().mute = true;
            effect_btn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            PlayerPrefs.SetInt("effectState", 0);
        } else {
            this.GetComponent<AudioSource>().mute = false;
            effect_btn.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            PlayerPrefs.SetInt("effectState", 1);
        } 
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

    public void get_challenge_reward()
    {
        audioSource.clip = getChallengeReward;
        audioSource.volume = 0.369f;
        audioSource.Play();
    }

}
