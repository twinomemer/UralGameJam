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

}
