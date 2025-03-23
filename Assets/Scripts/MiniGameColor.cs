using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MiniGameColor : MonoBehaviour
{
    public Image rectangle;
    public GameObject WimMiniGameColor;
    public Button[] buttons = new Button[3];

    private Color[] colors = new Color[]
    {
        Color.red, Color.green, Color.blue, Color.gray, Color.black, Color.yellow
    };

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

    public event UnityAction colorGuessed;
    
    void Start()
    {
        // Инициализация игры при старте
        RestartGame();
    }

    // Метод для перезапуска игры
    public void RestartGame()
    {
        WimMiniGameColor.SetActive(true);
        Generatecolor();
    }
    void Generatecolor()
    {
        usedColors.Clear(); // Очищаем список использованных цветов

        Color randomColorForRect = GetUniqueColor(); // Выбираем случайный цвет для прямоугольника
        answerIndex = Random.Range(0, buttons.Length); // Выбираем случайную кнопку для правильного ответа
        isCorrectByColor = Random.value > 0.5f; // Случайно выбираем, проверять ли по цвету или по названию

        if (rectangle != null)
        {
            rectangle.color = randomColorForRect;
        }

        for (int i = 0; i < buttons.Length; i++)
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

            // Добавляем обработчик нажатия
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

        usedColors.Add(uniqueColor);
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
                colorGuessed?.Invoke();
                Debug.Log("Правильно! (по цвету текста)");
            }
            else
            {
                Debug.Log("Неправильно!");
            }
        }
        else
        {
            // Проверяем правильность по названию цвета
            if (clickedButtonText.text == GetColorName(rectangle.color))
            {
                colorGuessed?.Invoke();
                Debug.Log("Правильно! (по названию цвета)");
            }
            else
            {
                Debug.Log("Неправильно!");
            }
        }

        // Завершаем игру
        WimMiniGameColor.SetActive(false);

    }
}