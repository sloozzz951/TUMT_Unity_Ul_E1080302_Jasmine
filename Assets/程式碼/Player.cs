using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("血量與血條")]
    public int hp = 100;
    public Slider hpSlider;
    [Header("勳章數量")]
    public Text textMedal;
    public int MedalCount;
    public int MedalTotal;
    [Header("時間區域")]
    public Text textTime;
    public float gameTime;

    [Header("結束畫布")]
    public GameObject final;
    public Text textBest;
    public Text textCurrent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "陷阱")
        {
            int d = other.GetComponent<Trap>().damage;
            hp -= d;
            hpSlider.value = hp;
            if (hp <= 0) Dead();
        }

        if (other.tag == "勳章")
        {
            MedalCount++;
            textMedal.text = "Medal : " + MedalCount + " / " + MedalTotal;
            Destroy(other.gameObject);
        }

        if(other.name == "終點" && MedalCount == MedalTotal)
        {            
            GameOver();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "陷阱")
        {
            int d = other.GetComponent<Trap>().damage;
            hp -= d;
            hpSlider.value = hp;
            if (hp <= 0) Dead();
        }
    }

    private void Start()
    {
        if(PlayerPrefs.GetFloat("最佳紀錄") == 0)
        {
            PlayerPrefs.SetFloat("最佳紀錄", 99999);
        }
        MedalTotal = GameObject.FindGameObjectsWithTag("勳章").Length;
        textMedal.text = "Medal : 0 /" +  MedalTotal;
    }

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        gameTime += Time.deltaTime;
        textTime.text = gameTime.ToString("F2");
    }

    private void Dead()
    {
        final.SetActive(true);
        textCurrent.text = "TIME : " + gameTime.ToString("F2");
        textBest.text = "BEST : " + PlayerPrefs.GetFloat("最佳紀錄").ToString("F2");
        Cursor.lockState = CursorLockMode.None;
            
    }

    private void GameOver()
    {
        final.SetActive(true);
        textCurrent.text = "TIME : " + gameTime.ToString("F2");

        if(gameTime < PlayerPrefs.GetFloat("最佳紀錄"))
        {
            PlayerPrefs.SetFloat("最佳紀錄", gameTime);
        }

        textBest.text = "BEST : " + PlayerPrefs.GetFloat("最佳紀錄").ToString("F2");

        Cursor.lockState = CursorLockMode.None;
    }
}
