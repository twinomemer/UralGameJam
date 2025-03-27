using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpellLibraryPlayer2 : MonoBehaviour
{
    [SerializeField] private float healValue; // величина хила
    [SerializeField] private float eCValue; // величина понижения коэффициента эффективности
    [SerializeField] private float duration; // продолжительность неуязвимости

    private float startArmor;
    private float boostedArmor = 100f;
    void Start()
    {

    }
    
    //Хилка (на самом деле отрицательный урон, лол)
    public void Heal(Character target)
    {
        target.TakeDamage(-healValue);
        Debug.Log("хил похилен");
    }

    public void StartBoost(Character target)
    {
        StartCoroutine(ArmorBoost(target));
    }
    
    //изменение типа урона на следующий по порядку
    private IEnumerator ArmorBoost(Character target)
    {
        startArmor = target.armor;
        
        target.armor = boostedArmor;
        Debug.Log($"Броня усилена до: {target.armor}");
        
        yield return new WaitForSeconds(duration);
        
        target.armor = startArmor;
        Debug.Log($"Броня вернулась к базовому значению: {target.armor}");
    }

    //понижение коэффициента эффективности
    public void DecreaseCoef(Character target)
    {
        target.effCoeff -= eCValue;
        Debug.Log("кэфчик упал");
    }
}
