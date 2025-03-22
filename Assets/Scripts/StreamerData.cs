using System;
using UnityEngine;

[Serializable]
public class StreamerData
{
    //Параметры стримера.
    public float Health;
    public float Armor;
    public float Damage;
    
    public  int HealthType;
    public int ArmorType;
    public int DamageType;

    public Sprite Sprite;
}
