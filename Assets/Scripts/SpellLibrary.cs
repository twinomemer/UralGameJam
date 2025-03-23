using System;
using UnityEngine;
using UnityEngine.Events;

public class SpellLibrary : MonoBehaviour
{
    [SerializeField] private float fireballDamage; //урон фаербола
    [SerializeField] private float HealValue; // величина хила
    [SerializeField] private float eCValue; // величина прибавки к коэффициенту эффективности
    
    public event UnityAction<Character> FireballCasted;
    public event UnityAction<Character> HealCasted;
    public event UnityAction<Character> ChangeDamageTypeCasted;
    public event UnityAction<Character> IncreaseCoefCasted;

    void Start()
    {
        FireballCasted += Fireball;
        HealCasted += Heal;
        ChangeDamageTypeCasted += ChangeDamageType;
        IncreaseCoefCasted += IncreaseCoef;
    }
    
    //удар по макушке
    private void Fireball(Character target) 
    {
        target.TakeDamage(fireballDamage);
    }
    
    //лечение (на самом деле отрицательный урон, лол)
    private void Heal(Character target)
    {
        target.TakeDamage(-HealValue);
    }

    //изменение типа урона на следующий по порядку
    private void ChangeDamageType(Character target)
    {
        if (target.damageType != 3) target.damageType += 1;
        else target.damageType -= 2;
    }

    //повышение коэффициента эффективности
    private void IncreaseCoef(Character target)
    {
        target.effCoeff += eCValue;
    }
}
