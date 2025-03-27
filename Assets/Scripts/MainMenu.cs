using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnButtonStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnButtonExitClick() 
    {
        Application.Quit();
        Debug.Log("Игра закрыта");
    }
}
