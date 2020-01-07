using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioMixer 音樂管理器;
    public Text loadingText;
    public Slider loading;

    public void SetVBGM(float value)
    {
        音樂管理器.SetFloat("VBGM", value);
    }

    public void SetVSFX(float value)
    {
        音樂管理器.SetFloat("VSFX", value);
    }

    public void Play()
    {
        SceneManager.LoadScene("場景");
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        //print("測試 1");
        //yield return new WaitForSeconds(1);
        //print("測試 2");

        AsyncOperation ao = SceneManager.LoadSceneAsync("遊戲場景");
        ao.allowSceneActivation = false;

        while (ao.isDone == false)
        {
            loadingText.text = ((ao.progress / 0.9f) * 100).ToString();
            loading.value = ao.progress / 0.9f;
            yield return new WaitForSeconds(0.0001f);

            if (ao.progress == 0.9f && Input.anyKey)
            {
                ao.allowSceneActivation = true;
            }
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("選單");
    }

    public void Quit()
    {
        Application.Quit();
    }
}