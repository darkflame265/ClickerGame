using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void move_to_scene_1()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
