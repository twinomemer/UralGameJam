using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpellLibraryPlayer1 : MonoBehaviour
{
    [SerializeField] private float fireballDamage; //урон фаербола
    [SerializeField] private float duration; // длительность смены типа урона
    [SerializeField] private float eCValue; // величина прибавки к коэффициенту эффективности

    private int baseType;
    private int changedType;
    
    void Start()
    {

    }
    
    //удар по макушке
    public void Fireball(Character target)
    {
        target.TakeDamage(fireballDamage);
    }
    
    //изменение типа урона на следующий по порядку
    public void ChangeDamageType(Character target)
    {
        StartCoroutine(DTChange(target));
    }
    
    private IEnumerator DTChange(Character target)
    {
        baseType = target.damageType;
        
        if (target.damageType != 3) target.damageType += 1;
        else target.damageType -= 2;
        Debug.Log($"Тип атаки изменён, теперь он: {target.damageType}");
        
        yield return new WaitForSeconds(duration);
        
        target.damageType = baseType;
        Debug.Log($"Тип атаки вернулся к базовому значению: {target.damageType}");
    }

    //повышение коэффициента эффективности
    public void IncreaseCoef(Character target)
    {
        target.effCoeff += eCValue;
        Debug.Log("кэфчик поднят");
    }
}
