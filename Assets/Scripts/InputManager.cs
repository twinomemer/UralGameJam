using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{ 
    public Button readyFirst;
    public Button readySecond;
    public int pointsFirstPlayer = 10;
    public int pointsSecondPlayer = 10;
    public int usedPointsFirstPlayer = 0;
    public int usedPointsSecondPlayer = 0;
    public TMP_Text firstPlayerPointsText;
    public TMP_Text secondPlayerPointsText;

    private int health;
    private int armor;
    private int damage;
    private bool isFirstPlayer = true;
    [System.Serializable]
    public class Parameter
    {
        public string name; // �������� ���������
        public int value;   // ������� ��������
        public TMP_Text valueText; // ��������� ���� ��� ����������� ��������
        public Button addButton; // ������ "+"
        public Button subtractButton; // ������ "-"
        public Button HealthType;
        public Button ArmorType;
        public Button DamageType;
    }

    public Parameter[] parametersFirstPlayer;
    public Parameter[] parametersSecondPlayer;
    private void Start()
    {
        foreach (var param in parametersFirstPlayer)
        {
            // ��������� ����������� ������
            param.addButton.onClick.AddListener(() => AddPoint(param));
            param.subtractButton.onClick.AddListener(() => SubtractPoint(param));
           // param.HealthType.onClick.AddListener(() => AddPointType(param));
            //param.HealthType.onClick.AddListener(() => AddPointType(param));
           // param.HealthType.onClick.AddListener(() => AddPointType(param));

            // ��������� ��������� ����
            UpdateValueText(param);
        }

        foreach (var param in parametersSecondPlayer)
        {
            param.addButton.onClick.AddListener(() => AddPoint(param));
            param.subtractButton.onClick.AddListener(() => SubtractPoint(param));
            UpdateValueText(param);
        }

        // ��������� ������ � ��������� ����� ��� ������� ������
        foreach (var param in parametersSecondPlayer)
        {
            param.addButton.interactable = false; // ��������� ������ ������� ������
            param.subtractButton.interactable = false;
            UpdateValueText(param);
        }
        // ��������� ������ "�����"
        readyFirst.onClick.AddListener(() => SwitchToSecondPlayer());
        readySecond.onClick.AddListener(() => FinishSetup());


        readySecond.interactable = false;
    }

    private void AddPoint(Parameter param)
    {
        if (isFirstPlayer)
        {
            if (usedPointsFirstPlayer < pointsFirstPlayer)
            {
                param.value += 10;
                usedPointsFirstPlayer++;
                UpdateValueText(param);
            }
        }
        else
        {
            if (usedPointsSecondPlayer < pointsSecondPlayer)
            {
                param.value += 10;
                usedPointsSecondPlayer++;
                UpdateValueText(param);
            }
        }
    }
    private void SubtractPoint(Parameter param)
    {
        if (isFirstPlayer)
        {
            if (param.value > 0)
            {
                if(param.name == "Health")
                {
                    if(param.value > 50)
                    {
                        param.value -= 10;
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
                    if (param.value > 50)
                    {
                        param.value -= 10;
                        usedPointsFirstPlayer--;
                        UpdateValueText(param);
                    }
                }
                else if (param.name == "Armor")
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
    }
    private void UpdateValueText(Parameter param)
    {
        if (param.valueText != null)
        {
            param.valueText.text = param.value.ToString();
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
        // ��������� ������ ������� ������
        foreach (var param in parametersFirstPlayer)
        {
            param.addButton.interactable = false;
            param.subtractButton.interactable = false;
        }

        // ������������ ������ ������� ������
        foreach (var param in parametersSecondPlayer)
        {
            param.addButton.interactable = true;
            param.subtractButton.interactable = true;
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

        foreach (var param in parametersFirstPlayer)
        {
            if (param.name == "Health")
                healthFirst = param.value;
            else if (param.name == "Armor")
                armorFirst = param.value;
            else if (param.name == "Damage")
                damageFirst = param.value;
        }

        float healthSecond = 0f;
        float armorSecond = 0f;
        float damageSecond = 0f;

        foreach (var param in parametersSecondPlayer)
        {
            if (param.name == "Health")
                healthSecond = param.value;
            else if (param.name == "Armor")
                armorSecond = param.value;
            else if (param.name == "Damage")
                damageSecond = param.value;
        }

        firstPlayerData = new StreamerData(healthFirst, armorFirst, damageFirst);
        secondPlayerData = new WatcherData(healthSecond, armorSecond, damageSecond);

        Debug.Log("������ ������� ������: Health=" + healthFirst + ", Armor=" + armorFirst + ", Damage=" + damageFirst);
        Debug.Log("������ ������� ������: Health=" + healthSecond + ", Armor=" + armorSecond + ", Damage=" + damageSecond);
    }

    private void FinishSetup()
    {
        // ��������� ��� ������ ����� ���������� ���������
        foreach (var param in parametersFirstPlayer)
        {
            param.addButton.interactable = false;
            param.subtractButton.interactable = false;
        }

        foreach (var param in parametersSecondPlayer)
        {
            param.addButton.interactable = false;
            param.subtractButton.interactable = false;
        }

        readySecond.interactable = false;

        Debug.Log("��������� ���������!");

        StreamerData streamerData;
        WatcherData watcherData;
        CreateCharacter(out streamerData, out watcherData);
    }

}
