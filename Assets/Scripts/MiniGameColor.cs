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
        { Color.red, "красный" },
        { Color.green, "зелёный" },
        { Color.blue, "синий" },
        { Color.gray, "серый" },
        { Color.black, "чёрный" },
        { Color.yellow, "жёлтый" }
    };
    private int answerIndex;
    private bool isCorrectByColor;
    private List<Color> usedColors = new List<Color>();
    private List<string> usedTextsInRound = new List<string>();


    void Start()
    {
        WimMiniGameColor.SetActive(true);
        Generatecolor();
    }
    

    void Generatecolor()
    {
        usedColors.Clear();
        usedTextsInRound.Clear();

        Color randomColorForRect = GetUniqueColor(); //Выбираем рандомно цвет из массива цветов
        answerIndex = Random.Range(0, buttons.Length); //Выбираем рандомно кнопку на экране, которая будет окрашена в тот же цвет, что и 
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
                    buttonText.text = GetUniqueText(wrongColor);
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

        usedColors.Add(uniqueColor); // Добавляем цвет в список использованных
        return uniqueColor;
    }
    string GetUniqueText(Color excludeColor)
    {
        string uniqueText;
        do
        {
            uniqueText = GetRandomWrongName(excludeColor);
        } while (usedTextsInRound.Contains(uniqueText));

        usedTextsInRound.Add(uniqueText); // Добавляем текст в список использованных
        return uniqueText;
    }

    string GetRandomWrongName(Color excludeColor)
    {
        Color wrongColor = GetUniqueColor(excludeColor);
        return GetColorName(wrongColor);
    }

    string GetColorName(Color color)
    {
        return colorNames.ContainsKey(color) ? colorNames[color] : "неизвестный";
    }

    void OnButtonClick(int clickedIndex)
    {
        TMP_Text clickedButtonText = buttons[clickedIndex].GetComponentInChildren<TMP_Text>();

        if (isCorrectByColor)
        {
            // Проверяем правильность по цвету текста
            if (clickedButtonText.color == rectangle.color)
            {
                Debug.Log("Правильно! (по цвету текста)");
                //Логика вызова функции с применением скилла
            }
            else
            {
                Debug.Log("Неправильно!");
                //не применяем скилл 
                //Логика вызова наказания
            }
        }
        else
        {
            // Проверяем правильность по названию цвета
            if (clickedButtonText.text == GetColorName(rectangle.color))
            {
                Debug.Log("Правильно! (по названию цвета)");
                // Логика вызова функции с применением скилла
            }
            else
            {
                Debug.Log("Неправильно!");
                //не применяем скилл 
                //Логика вызова наказания
            }
        }
    }
 }
