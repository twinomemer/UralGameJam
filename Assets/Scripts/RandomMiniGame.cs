using UnityEngine;

public class RandomMiniGame : MonoBehaviour
{

    int randomCanvase;
    public GameObject MiniGameColorCanvas;
    public GameObject QTECanvas;

    private QTESystem qte;
    private MiniGameColor mgc;

    public void OnClickSkill()
    {
        randomCanvase = Random.Range(0, 2);
        if(randomCanvase == 0)
        {
            mgc = MiniGameColorCanvas.GetComponent<MiniGameColor>();
            MiniGameColorCanvas.SetActive(true);
            mgc.RestartGame();
        }
        else
        {
            qte = QTECanvas.GetComponent<QTESystem>();
            qte.Refresh();
            QTECanvas.SetActive(true);
            qte.StartQTE();
        }
    }
}
