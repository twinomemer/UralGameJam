using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillClick : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private KeyCode triggerKey; //клавиша
    [SerializeField] private int skillNumber; //номер скилла, за который ответственна кнопка. От 1 до 6
    
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

    private void Update()
    {
        if (Input.GetKeyDown(triggerKey) && !MiniGameColorCanvas.activeSelf && !QTECanvas.activeSelf)
        {
            button.onClick.Invoke();
        }
    }

    private void DisableButton()
    {
        button.interactable = false;
        button.onClick.RemoveAllListeners();
    }
    
    public void OnClickSkill()
    {
        randomGame = Random.Range(0, 2);
        if(randomGame == 0)
        {
            mgc = MiniGameColorCanvas.GetComponent<MiniGameColor>();
            mgc.skillNumber = skillNumber;
            MiniGameColorCanvas.SetActive(true);
            mgc.RestartGame();
        }
        else
        {
            qte = QTECanvas.GetComponent<QTESystem>();
            qte.skillNumber = skillNumber;
            qte.Refresh();
            QTECanvas.SetActive(true);
            qte.StartQTE();
        }
    }
}
