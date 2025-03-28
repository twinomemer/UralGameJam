using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Streamer : Character
{
    [SerializeField] private Image image;
    
    [SerializeField] public HealthBar _healthBar;
    
    public void Initialize(StreamerData streamerData)
    {
        health = streamerData.Health;
        armor = streamerData.Armor;
        damage = streamerData.Damage;
        healthType = streamerData.HealthType;
        armorType = streamerData.ArmorType;
        damageType = streamerData.DamageType;
        
        Debug.Log($"uron streamer {damage}");
        _healthBar.SetMaxValue(health);
        _healthBar.Show();
    }
}
