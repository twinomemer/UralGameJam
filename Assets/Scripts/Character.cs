using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //Параметры бойца.
    protected float health;
    protected float armor;
    protected float damage;
    
    //Типы параметров. 1 - камень, 2 - ножницы, 3 - бумага. Остальное - психические отклонения.
    public int healthType;
    public int armorType;
    public int damageType;
    
    //Коэффициент, который отвечает за увеличение/снижение параметров при сопоставлении их типов.
    protected float effCoeff = 0.2f;
    
    public void TakeDamage(float _damage)
    {
        health -= _damage;
    }
    
    public void Attack(Character target)
    {
        //Если наш тип атаки сильнее типа брони оппонента, наносим доп урон.
        if ((this.damageType - target.armorType) == -1 || (this.damageType - target.armorType) == 2)
        {
            target.TakeDamage(damage * (1 + effCoeff) * (1 - target.armor));
        }
        //Если слабее, наносим сниженный урон.
        else if ((this.damageType - target.armorType) == 1 || (this.damageType - target.armorType) == -2)
        {
            target.TakeDamage(damage * (1 - effCoeff) * (1 - target.armor));
        }
        //Если типы одинаковы, урон не меняется.
        else
        {
            target.TakeDamage(damage * (1 - target.armor));
        }
    }
}
