using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void BeginTraining()
    {
        SceneManager.LoadScene("TimerScene");
    }

    public void Return()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
