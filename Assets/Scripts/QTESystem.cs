using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QTESystem : MonoBehaviour
{
    [SerializeField] public float qteDuration = 3f;
    [SerializeField] private SpellLibraryPlayer1 spellLibraryPlayer1;
    [SerializeField] private SpellLibraryPlayer2 spellLibraryPlayer2;
    [SerializeField] private Streamer streamer;
    [SerializeField] private Watcher watcher;
    
    public KeyCode[] possibleKeys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W }; // ��������� �������
    public TextMeshProUGUI qteText; // ����� ��� ����������� QTE
    public TextMeshProUGUI progressText; // ����� ��� ����������� ���������
    public float messageDisplayTime = 2f; // ����� ����������� ��������� "�����" � "������"
    public GameObject canvas;

    private List<KeyCode> currentSequence = new List<KeyCode>();
    private int currentStep = 0;
    private bool isQTEActive = false;
    private float qteTimer;
    private float messageTimer;
    private bool isMessageDisplayed = false;
    public int skillNumber;

    public event UnityAction QTECompleted;

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
        StartQTE();
    }

    public void Refresh()
    {
        currentStep = 0;
        qteText.text = "";
        progressText.text = "";
    }

    void Update()
    {
        if (isQTEActive)
        {
            qteTimer -= Time.deltaTime;

            if (qteTimer <= 0)
            {
                FailQTE();
            }
            else
            {
                CheckInput();
            }
        }

        if (isMessageDisplayed)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
            {
                ClearMessages();
            }
        }
    }

    public void StartQTE()
    {
        GenerateSequence();
        qteTimer = qteDuration;
        isQTEActive = true;
        currentStep = 0;
        UpdateUI();
    }

    // ��������� ��������� ������������������ �� 5 ������
    private void GenerateSequence()
    {
        currentSequence.Clear();
        for (int i = 0; i < 5; i++)
        {
            KeyCode randomKey = possibleKeys[Random.Range(0, possibleKeys.Length)];
            currentSequence.Add(randomKey);
        }
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(currentSequence[currentStep]))
            {
                currentStep++;
                UpdateUI();

                if (currentStep >= currentSequence.Count)
                {
                    CompleteQTE();
                }
            }
            else
            {
                FailQTE();
            }
        }
    }

    private void UpdateUI()
    {
        qteText.text = string.Join(" ", currentSequence);
        progressText.text = "Прогресс: " + currentStep + "/" + currentSequence.Count;
    }

    private void CompleteQTE()
    {
        QTECompleted += SpellCheck;
        qteText.text = "Успех!";
        progressText.text = "";
        isQTEActive = false;
        ShowMessage();
        canvas.SetActive(false);
        QTECompleted?.Invoke();
    }

    private void FailQTE()
    {
        qteText.text = "Провал!";
        progressText.text = "";
        isQTEActive = false;
        ShowMessage();
        canvas.SetActive(false);
    }

    private void ShowMessage()
    {
        isMessageDisplayed = true;
        messageTimer = messageDisplayTime;
    }

    private void ClearMessages()
    {
        qteText.text = "";
        progressText.text = "";
        isMessageDisplayed = false;
    }
}
