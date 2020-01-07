using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    [Header("暫停介面")]
    public Image imagePause;
    public Sprite spritePause, spritePlay;
    [Header("暫停")]
    public bool pause;

    private void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            print("暫停~");
            pause = !pause;
            imagePause.sprite = pause ? spritePlay : spritePause;
            Time.timeScale = pause ? 0 : 1;
        }
    }
    private void Update()
    {
        Pause();
    }


}
