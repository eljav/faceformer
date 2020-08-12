using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour
{
    public Image relleno;

    public void BeginTraining()
    {
        SceneManager.LoadScene("TimerScene");
    }

    private void Start()
    {
        int progress = PlayerPrefs.GetInt(DateTime.Today.ToString());
        float fill = 0.0f;
        if (progress == 1)
            fill = 0.333f;
        else if (progress == 2)
            fill = 0.666f;
        else if (progress == 3)
            fill = 1.0f;
        relleno.fillAmount = fill;
        print(progress);
    }
}
