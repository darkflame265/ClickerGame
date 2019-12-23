using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        
        //audioSource.clip = bgm;
        audioSource.clip = bgm_pack[PlayerPrefs.GetInt("bgm_num")];
        bgm_name_text.text = bgm_name[PlayerPrefs.GetInt("bgm_num")];

        audioSource.volume = 0.123f;
        audioSource.loop = true;
        audioSource.mute = false;

        audioSource.Play();
        //audioSource.Stop();

        audioSource.playOnAwake = true;

        audioSource.priority = 0; //오디오소스중 현재 오디오 소스의 우선순위를 최우선
    }

    public AudioClip[] bgm_pack = new AudioClip[0];
    public Text bgm_name_text;

    string[] bgm_name = {
        "Ikson - We Are Free",
        "Dark Waltz - 김재성",
        "fantasy piano - 김태현",
    };

    public void set_background_left_btn()
    {
        int i = PlayerPrefs.GetInt("bgm_num");
        i--;
        if(i == -1)
        {
            PlayerPrefs.SetInt("bgm_num", bgm_pack.Length-1);
        } else {
            PlayerPrefs.SetInt("bgm_num", i);
        }
        audioSource.clip = bgm_pack[PlayerPrefs.GetInt("bgm_num")];
        bgm_name_text.text = bgm_name[PlayerPrefs.GetInt("bgm_num")];
        audioSource.Play();
    }

    public void set_background_right_btn()
    {
        int i = PlayerPrefs.GetInt("bgm_num");
        i++;
        if(i > bgm_pack.Length-1)
        {
            PlayerPrefs.SetInt("bgm_num", 0);
        } else {
            PlayerPrefs.SetInt("bgm_num", i);
        }
        audioSource.clip = bgm_pack[PlayerPrefs.GetInt("bgm_num")];
        bgm_name_text.text = bgm_name[PlayerPrefs.GetInt("bgm_num")];
        audioSource.Play();
    }

}
