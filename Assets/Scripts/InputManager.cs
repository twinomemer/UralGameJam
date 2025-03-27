using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public TMP_InputField healthField1;
    public TMP_InputField armorField1;
    public TMP_InputField damageField1;
    public TMP_InputField healthField2;
    public TMP_InputField armorField2;
    public TMP_InputField damageField2;
    public Button readyFirst;
    public Button readySecond;

    private int health;
    private int armor;
    private int damage;
    private bool isFirstPlayer = true;

    private void Start()
    {
        if (healthField1 == null || armorField1 == null || damageField1 == null)
        {
            Debug.LogError("InputField �� ��������!");
        }
        if (healthField2 == null || armorField2 == null || damageField2 == null)
        {
            Debug.LogError("InputField �� ��������!");
        }
        if (readyFirst == null || readySecond == null)
        {
            Debug.LogError("������ �� ���������!");
        }
        //������� ��������� ���� ����� ������� ������ � ��� ������
        healthField2.interactable = false;
        armorField2.interactable = false;
        damageField2.interactable = false;

        readySecond.interactable = false;
    }

    public void OnClickReady()
    {

        if (isFirstPlayer)
        {
            health = Convert.ToInt32(healthField1.text);
            armor = Convert.ToInt32(armorField1.text);
            damage = Convert.ToInt32(damageField1.text);

            //������� ���������� ����� ����� ������� ������
            healthField2.interactable = true;
            armorField2.interactable = true;
            damageField2.interactable = true;

            readySecond.interactable = true;

            isFirstPlayer = false;

            //��������� ���� ����� ������� ������
            healthField1.interactable = false;
            armorField1.interactable = false;
            damageField1.interactable = false;
            readyFirst.interactable = false;
            //������ �������� ���������� � ������� �������� �����
        }
        else
        {
            health = Convert.ToInt32(healthField2.text);
            armor = Convert.ToInt32(armorField2.text);
            damage = Convert.ToInt32(damageField2.text);
            //������ �������� ���������� � ������� �������� ����� � ������� ����
        }

    }
}
