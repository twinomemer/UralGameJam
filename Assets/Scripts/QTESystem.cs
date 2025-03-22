using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{
    [SerializeField] public float qteDuration = 3f;
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

    void Start()
    {
        StartQTE();
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
        progressText.text = "��������: " + currentStep + "/" + currentSequence.Count;
    }

    private void CompleteQTE()
    {
        qteText.text = "�����!";
        progressText.text = "";
        isQTEActive = false;
        ShowMessage();
        canvas.SetActive(false);


    }

    private void FailQTE()
    {
        qteText.text = "������!";
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
