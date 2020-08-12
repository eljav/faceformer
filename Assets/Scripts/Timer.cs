using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI instruccionTest;
    public TextMeshProUGUI cronometroText;
    public TextMeshProUGUI countdownTimerText;
    public Button startButton;
    public Sprite playSprite;
    public Sprite pauseSprite;
    public float maximumFill;
    public float currentFill;
    public Image fill;
    public string activeText;
    public string restText;
    public int countdownTime;
    public int maxReps;
    public float pulseTime = 0f;
    public float pulseTimeMax = 1f;
    public float pulseIncrease = 1f;
    private float timeStart;
    bool timerActive = false;
    bool apretar = false;
    bool primeraVez = true;
    bool pulsar = false;
    int counter = 0;

    static int progress = 0;

    void Start()
    {
        progressText.text = counter + " / " + maxReps;
        if (countdownTime == 0)
        {
            countdownTimerText.gameObject.SetActive(false);
            cronometroText.gameObject.SetActive(true);
        }
        cronometroText.text = timeStart.ToString("f0");
    }

    void Update()
    {
        GetCurrentFill();
        if (timerActive && counter < 20)
        {
            timeStart += Time.deltaTime;
            SetInstruccionTest(apretar);
            SetBackgroundColor(apretar);
            cronometroText.text = timeStart.ToString("f0");
            if (pulsar)
            {
                pulseTime += Time.deltaTime;
                if (pulseTime < pulseTimeMax * .5f)
                {
                    progressText.transform.localScale += Vector3.one * pulseIncrease * Time.deltaTime;
                }
                else if (pulseTime < pulseTimeMax)
                {
                    progressText.transform.localScale -= Vector3.one * pulseIncrease * Time.deltaTime;
                }
                else
                {
                    pulseTime = 0f;
                    pulsar = false;
                }
            }

            if (timeStart > 6)
            {
                timeStart = 0;
                if (apretar)
                {
                    counter++;
                    pulsar = true;
                }
                apretar = !apretar;
                progressText.text = counter + " / " + maxReps;
            }
        }
        if (counter == 20)
        {
            if (progress < 3)
            {
                progress++;
                PlayerPrefs.SetInt(DateTime.Today.ToString(), progress);
                counter = 21;
            }
        }
    }

    public void timerButton()
    {
        apretar = !apretar;
        if (timerActive)
        {
            timerActive = !timerActive;
            startButton.image.sprite = playSprite;
        }
        else
        {
            startButton.image.sprite = pauseSprite;
            if (primeraVez)
                StartCoroutine(CountdownToStart());
            else
                timerActive = !timerActive;
                
        }
    }

    IEnumerator CountdownToStart()
    {
        primeraVez = false;
        while (countdownTime > 0)
        {
            countdownTimerText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownTimerText.gameObject.SetActive(false);
        cronometroText.gameObject.SetActive(true);
        timerActive = true;
        apretar = true;
    }

    void GetCurrentFill()
    {
        float fillAmount = timeStart / (float)maximumFill;
        fill.fillAmount = fillAmount;
    }

    void SetInstruccionTest(bool apretar)
    {
        instruccionTest.text = apretar ? activeText : restText;
    }

    void SetBackgroundColor(bool apretar)
    {
        canvas.GetComponent<Image>().color = apretar ? new Color32(55, 38, 188, 255) : new Color32(62, 153, 154, 255);
    }

    void PulsateText(TextMeshProUGUI text)
    {
        text.transform.localScale += Vector3.one * 1f * Time.deltaTime;
    }
    public void Return()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
