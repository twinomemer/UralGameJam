using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{ 
    [SerializeField] private Streamer streamer;
    [SerializeField] private Watcher watcher;
    
    public Button readyFirst;
    public Button readySecond;
    public int pointsFirstPlayer = 10;
    public int pointsSecondPlayer = 10;
    public int usedPointsFirstPlayer = 0;
    public int usedPointsSecondPlayer = 0;
    public TMP_Text firstPlayerPointsText;
    public TMP_Text secondPlayerPointsText;
    public GameObject canvas;

    private int health;
    private int armor;
    private int damage;
    private bool isFirstPlayer = true;
    private Color originalColor;

    private bool statBlock = false;
    //private int changesP1 = 3;
    //private int changesP2 = 3;
    [System.Serializable]
    public class Parameter
    {
        public string name; // �������� ���������
        public int value;   // ������� ��������
        public int modificator; //�����������
        public TMP_Text valueText; // ��������� ���� ��� ����������� ��������
        public TMP_Text typeText; // ��������� ���� ��� ����������� ������������
        public Button addButton; // ������ "+"
        public Button subtractButton; // ������ "-"
        public Button Rock;
        public Button Scissors;
        public Button Page;
        
    }

    public Parameter[] parametersFirstPlayer;
    public Parameter[] parametersSecondPlayer;
    
    public event UnityAction BuildingEnded;
    //public event UnityAction StatChanged;
    
    private void OnEnable()
    {
        Debug.Log("vkl");
        if (statBlock)
        {
            pointsFirstPlayer = 0;
            pointsSecondPlayer = 0;
        }
      
        foreach (var param in parametersFirstPlayer)
        {
            // ��������� ����������� ������
            if (!statBlock)
            {
                param.addButton.onClick.AddListener(() => AddPoint(param));
                param.subtractButton.onClick.AddListener(() => SubtractPoint(param));
            }
            else
            {
                param.addButton.onClick.RemoveAllListeners();
                param.subtractButton.onClick.RemoveAllListeners();
            }
            
            param.Rock.onClick.AddListener(() => AddPointTypeRock(param));
            
            param.Page.onClick.AddListener(() => AddPointTypePage(param));
            
            param.Scissors.onClick.AddListener(() => AddPointTypeScissors(param));

            // ��������� ��������� ����
            UpdateValueText(param);
        }
        
        foreach (var param in parametersFirstPlayer)
        {
            param.addButton.interactable = true;
            param.subtractButton.interactable = true;
            param.Rock.interactable = true;
            param.Page.interactable = true;
            param.Scissors.interactable = true;
            UpdateValueText(param);
        }

        foreach (var param in parametersSecondPlayer)
        {
            if (!statBlock)
            {
                param.addButton.onClick.AddListener(() => AddPoint(param));
                param.subtractButton.onClick.AddListener(() => SubtractPoint(param));
            }
            else
            {
                param.addButton.onClick.RemoveAllListeners();
                param.subtractButton.onClick.RemoveAllListeners();
            }
            
            param.Rock.onClick.AddListener(() => AddPointTypeRock(param));
            
            param.Page.onClick.AddListener(() => AddPointTypePage(param));
            
            param.Scissors.onClick.AddListener(() => AddPointTypeScissors(param));
            UpdateValueText(param);
        }

        // ��������� ������ � ��������� ����� ��� ������� ������
        foreach (var param in parametersSecondPlayer)
        {
            param.addButton.interactable = false; // ��������� ������ ������� ������
            param.subtractButton.interactable = false;
            param.Rock.interactable = false;
            param.Page.interactable = false;
            param.Scissors.interactable = false;
            param.valueText.text = "***";
            param.typeText.text = "**";
        }
        // ��������� ������ "�����"
        readyFirst.onClick.AddListener(() => SwitchToSecondPlayer());
        readySecond.onClick.AddListener(() => FinishSetup());

        readySecond.interactable = false;
    }

    private void Update()
    {
        /*if (changesP1 == 0)
        {
            foreach (var param in parametersFirstPlayer)
            {
                param.Rock.onClick.RemoveAllListeners();
                param.Page.onClick.RemoveAllListeners();
                param.Scissors.onClick.RemoveAllListeners();
            }
        }
        
        if (changesP2 == 0)
        {
            foreach (var param in parametersSecondPlayer)
            {
                param.Rock.onClick.RemoveAllListeners();
                param.Page.onClick.RemoveAllListeners();
                param.Scissors.onClick.RemoveAllListeners();
            }
        }*/
    }

    private void AddPoint(Parameter param)
    {

        if (isFirstPlayer)
        {
            if (param.value > 0 && usedPointsFirstPlayer < pointsFirstPlayer)
            {
                if (param.name == "Health")
                {
                        param.value += 30;
                        usedPointsFirstPlayer++;
                        UpdateValueText(param);
                }
                else if (param.name == "Armor" && param.value < 80)
                {
                        param.value += 10;
                        usedPointsFirstPlayer++;
                        UpdateValueText(param);
                }
                else if (param.name == "Damage")
                {
                        param.value += 10;
                        usedPointsFirstPlayer++;
                        UpdateValueText(param);
                }

            }
        }
        else
        {
            if (param.value > 0 && usedPointsSecondPlayer < pointsSecondPlayer)
            {
                if (param.name == "Health")
                {
                    param.value += 30;
                    usedPointsSecondPlayer++;
                    UpdateValueText(param);
                }
                else if (param.name == "Armor" && param.value < 80)
                {
                    param.value += 10;
                    usedPointsSecondPlayer++;
                    UpdateValueText(param);
                }
                else if(param.name == "Damage")
                {
                    param.value += 10;
                    usedPointsSecondPlayer++;
                    UpdateValueText(param);
                }
            }
        }
    }

    private void AddPointTypeRock(Parameter param)
    {
        param.modificator = 1;
        UpdateValueText(param);
    }
    private void AddPointTypePage(Parameter param)
    {
        param.modificator = 3;
        UpdateValueText(param);
    }
    private void AddPointTypeScissors(Parameter param)
    {
        param.modificator = 2;
        UpdateValueText(param);
    }

    private void SubtractPoint(Parameter param)
    {
        if (isFirstPlayer)
        {
            if (param.value > 0)
            {
                if(param.name == "Health")
                {
                    if(param.value > 100)
                    {
                        param.value -= 30;
                        usedPointsFirstPlayer--;
                        UpdateValueText(param);
                    }
                }
                else if(param.name == "Armor")
                {
                    if (param.value > 10)
                    {
                        param.value -= 10;
                        usedPointsFirstPlayer--;
                        UpdateValueText(param);
                    }
                }
                else
                {
                    if (param.value > 20)
                    {
                        param.value -= 10;
                        usedPointsFirstPlayer--;
                        UpdateValueText(param);
                    }
                }
               
            }
        }
        else
        {
            if (param.value > 0)
            {
                if (param.name == "Health")
                {
                    if (param.value > 100)
                    {
                        param.value -= 30;
                        usedPointsSecondPlayer--;
                        UpdateValueText(param);
                    }
                }
                else if (param.name == "Armor")
                {
                    if (param.value > 10)
                    {
                        param.value -= 10;
                        usedPointsSecondPlayer--;
                        UpdateValueText(param);
                    }
                }
                else
                {
                    if (param.value > 20)
                    {
                        param.value -= 10;
                        usedPointsSecondPlayer--;
                        UpdateValueText(param);
                    }
                }
            }
        }
    }
    private void UpdateValueText(Parameter param)
    {
        if (param.valueText != null)
        {
            param.valueText.text = param.value.ToString();
        }
        if (param.typeText != null)
        {
            param.typeText.text = param.modificator.ToString();
        }
        if (isFirstPlayer)
        {
            if (firstPlayerPointsText != null)
            {
                firstPlayerPointsText.text = (pointsFirstPlayer-usedPointsFirstPlayer).ToString();
            }
        }
        else
        {
            if (secondPlayerPointsText != null)
            {
                secondPlayerPointsText.text = (pointsFirstPlayer-usedPointsSecondPlayer).ToString();
            }
        }
       
    }
    
    private void SwitchToSecondPlayer()
    {
        if(usedPointsFirstPlayer < pointsFirstPlayer)
        {
            return;
        }
        // ��������� ������ ������� ������
        foreach (var param in parametersFirstPlayer)
        {
            param.addButton.interactable = false;
            param.subtractButton.interactable = false;
            param.Rock.interactable = false;
            param.Page.interactable = false;
            param.Scissors.interactable = false;
            param.valueText.text = "***";
            param.typeText.text = "**";
        }

        // ������������ ������ ������� ������
        foreach (var param in parametersSecondPlayer)
        {
            param.addButton.interactable = true;
            param.subtractButton.interactable = true;
            param.Rock.interactable = true;
            param.Page.interactable = true;
            param.Scissors.interactable = true;
            UpdateValueText(param);
        }

        // ����������� ���� �� ������� ������
        isFirstPlayer = false;

        // ������������ ������ "�����" ������� ������
        readyFirst.interactable = false;
        readySecond.interactable = true;
    }
    public void CreateCharacter(out StreamerData firstPlayerData, out WatcherData secondPlayerData)
    {
        float healthFirst = 0f;
        float armorFirst = 0f;
        float damageFirst = 0f;
        int healthModific1 = 0;
        int armorModific1 = 0;
        int damageModific1 = 0;

        foreach (var param in parametersFirstPlayer)
        {
            if (param.name == "Health")
            {
                healthFirst = param.value;
                healthModific1 = param.modificator;
            }
            else if (param.name == "Armor")
            {
                armorFirst = param.value;
                armorModific1 = param.modificator;
            }
            else if (param.name == "Damage")
            {
                damageFirst = param.value;
                damageModific1 = param.modificator;
            }
                
        }

        float healthSecond = 0f;
        float armorSecond = 0f;
        float damageSecond = 0f;
        int healthModific2 = 0;
        int armorModific2 = 0;
        int damageModific2 = 0;

        foreach (var param in parametersSecondPlayer)
        {
            if (param.name == "Health")
            {
                healthSecond = param.value;
                healthModific2 = param.modificator;
            }
            else if (param.name == "Armor")
            {
                armorSecond = param.value;
                armorModific2 = param.modificator;
            }
            else if (param.name == "Damage")
            {
                damageSecond = param.value;
                damageModific2 = param.modificator;
            }
               
        }

        firstPlayerData = new StreamerData(healthFirst, armorFirst, damageFirst, healthModific1, armorModific1, damageModific1);
        secondPlayerData = new WatcherData(healthSecond, armorSecond, damageSecond, healthModific2, armorModific2, damageModific2);

        Debug.Log("������ ������� ������: Health=" + healthFirst + ", Armor=" + armorFirst + ", Damage=" + damageFirst);
        Debug.Log("������ ������� ������: Health=" + healthSecond + ", Armor=" + armorSecond + ", Damage=" + damageSecond);
    }

    private void FinishSetup()
    {
        if (usedPointsSecondPlayer < pointsSecondPlayer)
        {
            return;
        }
        // ��������� ��� ������ ����� ���������� ���������
        foreach (var param in parametersFirstPlayer)
        {
            param.addButton.interactable = false;
            param.subtractButton.interactable = false;
            param.Rock.interactable = false;
            param.Page.interactable = false;
            param.Scissors.interactable = false;

        }

        foreach (var param in parametersSecondPlayer)
        {
            param.addButton.interactable = false;
            param.subtractButton.interactable = false;
            param.Rock.interactable = false;
            param.Page.interactable = false;
            param.Scissors.interactable = false;
        }

        readySecond.interactable = false;

        Debug.Log("��������� ���������!");

        StreamerData streamerData;
        WatcherData watcherData;
        CreateCharacter(out streamerData, out watcherData);
        streamer.Initialize(streamerData);
        watcher.Initialize(watcherData);
        BuildingEnded?.Invoke();
        canvas.SetActive(false);
    }

    //метод, ограничивающий количество вносимых изменений в билд. n - кол-во разрешённых изменений
    public void SetBlock(int n1, int n2)
    {
        //changesP1 = n1;
        //changesP2 = n2;
        statBlock = true;
    }

    /*private void DecreaseChanges(bool firstPlayer)
    {
        if (firstPlayer) changesP1 -= 1;
        else changesP2 -= 1;

        if (changesP1 < 0) changesP1 = 0;
        if (changesP2 < 0) changesP2 = 0;
    }*/
}
