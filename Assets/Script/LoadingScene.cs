using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Slider progressbar;
    public Text loadText;
    AsyncOperation operation;

    private void Start()
    {
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        operation = SceneManager.LoadSceneAsync("SampleScene");
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            //Debug.Log("progressbar.value: " + progressbar.value);
            yield return null;
            if(progressbar.value < 1f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
                loadText.text = "" + progressbar.value + "%";
            }
            else {
                loadText.text = "100%...";
            }
             if(progressbar.value >= 1f && progressbar.value > 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            
        }
    }

    public void pressButton()
    {
        // if(progressbar.value >= 1f && progressbar.value > 0.9f)
        // {
        //     operation.allowSceneActivation = true;
        // } else {
        //     Debug.Log("progressbar.value: " + progressbar.value);
        // }
        
    }
}
