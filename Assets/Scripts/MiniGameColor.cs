using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System.Collections.Generic;

public class MiniGameColor : MonoBehaviour
{
    public Image rectangle;
    public GameObject WimMiniGameColor;
    public Button[] buttons = new Button[3];
    
    private Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.gray, Color.black, Color.yellow, Color.green};
    private Dictionary<Color, string> colorNames = new Dictionary<Color, string>()
    {
        { Color.red, "�������" },
        { Color.green, "������" },
        { Color.blue, "�����" },
        { Color.gray, "�����" },
        { Color.black, "������" },
        { Color.yellow, "�����" }
    };
    private int answerIndex;
    private bool isCorrectByColor;
    private List<Color> usedColors = new List<Color>();


    void Start()
    {
        WimMiniGameColor.SetActive(true);
        Generatecolor();
    }
    

    void Generatecolor()
    {
        usedColors.Clear();
        Color randomColorForRect = GetUniqueColor(); //�������� �������� ���� �� ������� ������
        answerIndex = Random.Range(0, buttons.Length); //�������� �������� ������ �� ������, ������� ����� �������� � ��� �� ����, ��� � 
        isCorrectByColor = Random.value > 0.5f;

        if (rectangle != null)
        {
            rectangle.color = randomColorForRect;
        }
        
        
        for (int i=0; i< buttons.Length; i++)
        {

            TMP_Text buttonText = buttons[i].GetComponentInChildren<TMP_Text>();
            if (i == answerIndex && buttonText != null)
            {
                if (isCorrectByColor)
                {
                    buttonText.text = GetRandomWrongName(randomColorForRect);
                    buttonText.color = randomColorForRect;
                }
                else
                {
                    buttonText.text = GetColorName(randomColorForRect);
                    buttonText.color = GetUniqueColor(randomColorForRect);
                }
            }
            else
            {
                if (buttonText != null)
                {
                    Color wrongColor = GetUniqueColor(randomColorForRect);
                    buttonText.text = GetRandomWrongName(randomColorForRect);
                    buttonText.color = wrongColor;

                }
            }

            int buttonIndex = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
            
        }
    }
    Color GetUniqueColor(Color excludeColor = default)
    {
        Color uniqueColor;
        do
        {
            uniqueColor = colors[Random.Range(0, colors.Length)];
        } while (uniqueColor == excludeColor || usedColors.Contains(uniqueColor));

        usedColors.Add(uniqueColor); // ��������� ���� � ������ ��������������
        return uniqueColor;
    }

    Color GetRandomWrongColor(Color excludeColor)
    {
        Color wrongColor;
        do
        {
            wrongColor = colors[Random.Range(0, colors.Length)];
        } while (wrongColor == excludeColor);
        return wrongColor;
    }

    string GetRandomWrongName(Color excludeColor)
    {
        Color wrongColor = GetRandomWrongColor(excludeColor);
        return GetColorName(wrongColor);
    }
    string GetColorName(Color color)
    {
        return colorNames.ContainsKey(color) ? colorNames[color] : "�����������";
    }

    void OnButtonClick(int clickedIndex)
    {
        TMP_Text clickedButtonText = buttons[clickedIndex].GetComponentInChildren<TMP_Text>();

        if (isCorrectByColor)
        {
            // ��������� ������������ �� ����� ������
            if (clickedButtonText.color == rectangle.color)
            {
                Debug.Log("���������! (�� ����� ������)");
            }
            else
            {
                Debug.Log("�����������!");
            }
        }
        else
        {
            // ��������� ������������ �� �������� �����
            if (clickedButtonText.text == GetColorName(rectangle.color))
            {
                Debug.Log("���������! (�� �������� �����)");
            }
            else
            {
                Debug.Log("�����������!");
            }
        }
    }
 }
