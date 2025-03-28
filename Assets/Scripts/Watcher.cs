using UnityEngine;
using UnityEngine.UI;

public class Watcher : Character
{
    [SerializeField] private Image image;
    
    [SerializeField] public HealthBar _healthBar;

    public void Initialize(WatcherData watcherData)
    {
        health = watcherData.Health;
        armor = watcherData.Armor;
        damage = watcherData.Damage;
        healthType = watcherData.HealthType;
        armorType = watcherData.ArmorType;
        damageType = watcherData.DamageType;
        
        Debug.Log($"uron watcher {damage}");
        _healthBar.SetMaxValue(health);
        _healthBar.Show();
    }
}
