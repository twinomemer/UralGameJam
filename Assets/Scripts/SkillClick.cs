using UnityEngine;
using UnityEngine.UI;

public class SkillClick : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Streamer streamer;
    [SerializeField] private int skillNumber; //номер скилла, за который ответственна кнопка. От 1 до 3
    
    [SerializeField] private GameObject MiniGameColorCanvas;
    [SerializeField] private GameObject QTECanvas;
    
    private QTESystem qte;
    private MiniGameColor mgc;

    private int randomGame;
    
    private void Start()
    {
        button.onClick.AddListener(DisableButton);
        button.onClick.AddListener(OnClickSkill);
    }

    private void DisableButton()
    {
        button.interactable = false;
    }
    
    public void OnClickSkill()
    {
        randomGame = Random.Range(0, 2);
        if(randomGame == 0)
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
