using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MiniGameColor : MonoBehaviour
{
    [SerializeField] private SpellLibraryPlayer1 spellLibraryPlayer1;
    [SerializeField] private SpellLibraryPlayer2 spellLibraryPlayer2;
    [SerializeField] private Streamer streamer;
    [SerializeField] private Watcher watcher;
    [SerializeField] private KeyCode[] triggerButtons;
    public Image rectangle;
    public GameObject WimMiniGameColor;
    public Button[] buttons = new Button[3];
    public int skillNumber;

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
    
    private void SpellCheck()
    {
        switch (skillNumber)
        {
            case 1:
                spellLibraryPlayer1.Fireball(watcher);
                break;
            case 2:
                spellLibraryPlayer1.ChangeDamageType(streamer);
                break;
            case 3:
                spellLibraryPlayer1.IncreaseCoef(streamer);
                break;
            case 4:
                spellLibraryPlayer2.Heal(watcher);
                break;
            case 5:
                spellLibraryPlayer2.StartBoost(watcher);
                break;
            case 6:
                spellLibraryPlayer2.DecreaseCoef(streamer);
                break;
        }
    }
    
    void Start()
    {
        // Инициализация игры при старте
        RestartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(triggerButtons[0]))
        {
            buttons[0].onClick.Invoke();
        }
        else if (Input.GetKeyDown(triggerButtons[1]))
        {
            // Имитируем нажатие кнопки
            buttons[1].onClick.Invoke();
        }
        else if (Input.GetKeyDown(triggerButtons[2]))
        {
            // Имитируем нажатие кнопки
            buttons[2].onClick.Invoke();
        }
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
                colorGuessed += SpellCheck;
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
                colorGuessed += SpellCheck;
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