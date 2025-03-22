using System;
using UnityEngine;

[Serializable]
public class StreamerData : MonoBehaviour
{
    //Параметры стримера.
    public float Health = 100f;
    public float Armor = 25f;
    public float Damage = 10f;
    
    public int HealthType = 1;
    public int ArmorType = 1;
    public int DamageType = 1;
}
