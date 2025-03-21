using UnityEngine;

public class Streamer : Character
{
    //Параметры бойца.
    [SerializeField] private float health;
    [SerializeField] private float armor;
    [SerializeField] private float damage;
    
    [SerializeField] private int healthType;
    [SerializeField] private int armorType;
    [SerializeField] private int damageType;
    
    [SerializeField] private Character enemy;

    
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
