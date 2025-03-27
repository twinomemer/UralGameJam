using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StreamerData : MonoBehaviour
{
    //Параметры стримера.
    public float Health = 100f;
    public float Armor = 25f;
    public float Damage = 1f;
    
    public int HealthType = 1;
    public int ArmorType = 1;
    public int DamageType = 1;
    public StreamerData(float Health, float Armor, float Damage)
    {
        this.Health = Health;
        this.Armor = Armor;
        this.Damage = Damage;
    }
}
