using System;
using UnityEngine;

[Serializable]
public class WatcherData : MonoBehaviour
{
    //Параметры зрителя.
    public float Health = 100f;
    public float Armor = 25f;
    public float Damage = 1f;
    
    public int HealthType = 2;
    public int ArmorType = 2;
    public int DamageType = 2;

    public WatcherData(float Health, float Armor, float Damage, int HealthType, int ArmorType, int DamageType)
    {
        this.Health = Health;
        this.Armor = Armor;
        this.Damage = Damage;
        this.HealthType = HealthType;
        this.ArmorType = ArmorType;
        this.DamageType = DamageType;
    }
}
