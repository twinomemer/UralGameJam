using UnityEngine;

public abstract class Character
{
    //Параметры бойца.
    protected float health;
    protected float armor;
    protected float damage;
    protected int blessing;
    
    //Типы параметров. 1 - камень, 2 - ножницы, 3 - бумага. Остальное - психические отклонения.
    protected int healthType;
    protected int armorType;
    protected int damageType;
    protected int blessingType;
    
    //Коэффициент, который отвечает за увеличение/снижение параметров при сопоставлении их типов.
    protected float effCoeff = 0.2f;

    private void TakeDamage(float _damage)
    {
        health -= _damage;
    }
    
    public void Attack(Character target)
    {
        //Если наш тип атаки сильнее типа брони оппонента, наносим доп урон
        if ((this.damageType - target.armorType) == -1 || (this.damageType - target.armorType) == 2)
        {
            target.TakeDamage(damage * (1 + effCoeff));
        }
        //Если слабее, наносим сниженный урон
        else if ((this.damageType - target.armorType) == 1 || (this.damageType - target.armorType) == -2)
        {
            target.TakeDamage(damage * (1 - effCoeff));
        }
        //Если типы одинаковы, урон не меняется
        else
        {
            target.TakeDamage(damage);
        }
    }
}
