using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Streamer : Character
{
    [SerializeField] private Image image;
    
    [SerializeField] private Character enemy;
    
    [SerializeField] private HealthBar _healthBar;

    [SerializeField] private SpellLibrary _spellLibrary;
    
    public void Initialize(StreamerData streamerData)
    {
        health = streamerData.Health;
        armor = streamerData.Armor;
        damage = streamerData.Damage;
        spells = streamerData.Spells;
        healthType = streamerData.HealthType;
        armorType = streamerData.ArmorType;
        damageType = streamerData.DamageType;
        
        _healthBar.SetMaxValue(health);
        _healthBar.Show();
        
        this.OnDamaged += _healthBar.DecreaseValue;
        this.OnDead += _healthBar.Hide;

        for (int i = 0; i < spells.Count; i++)
        {
            switch (spells[i])
            {
                case 1:
                    
                    break;
            }
        }
    }
}
