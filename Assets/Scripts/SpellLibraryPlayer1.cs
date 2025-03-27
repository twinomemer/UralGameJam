using System;
using UnityEngine;
using UnityEngine.Events;

public class SpellLibraryPlayer1 : MonoBehaviour
{
    [SerializeField] private float fireballDamage; //урон фаербола
    [SerializeField] private float eCValue; // величина прибавки к коэффициенту эффективности
    
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
        if (target.damageType != 3) target.damageType += 1;
        else target.damageType -= 2;
        Debug.Log("тип урона изменён");
    }

    //повышение коэффициента эффективности
    public void IncreaseCoef(Character target)
    {
        target.effCoeff += eCValue;
        Debug.Log("кэфчик поднят");
    }
}
