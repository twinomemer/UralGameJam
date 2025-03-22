using UnityEngine;

public class RandomMiniGame : MonoBehaviour
{

    int randomCanvase;
    public GameObject MiniGameColorCanvas;
    public GameObject QTECanvas;

    public void OnClickSkill()
    {
        randomCanvase = Random.Range(0, 2);
        if(randomCanvase == 0)
        {
            MiniGameColorCanvas.SetActive(true);
        }
        else
        {
            QTECanvas.SetActive(true);
        }
    }
}
