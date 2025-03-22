using UnityEngine;
using UnityEngine.Events;

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
    
    public event UnityAction<float> OnDamaged;
    public event UnityAction OnDead;
    
    public void TakeDamage(float _damage)
    {
        if (_damage >= health)
        {
            health = 0;
            OnDamaged?.Invoke(_damage);
            OnDead?.Invoke();
            return;
        }
        health -= _damage;
        OnDamaged?.Invoke(_damage);
        Debug.Log(_damage);
    }
    
    public void Attack(Character target)
    {
        //Если наш тип атаки сильнее типа брони оппонента, наносим доп урон.
        if ((this.damageType - target.armorType) == -1 || (this.damageType - target.armorType) == 2)
        {
            target.TakeDamage(damage * (1 + effCoeff) * (1 - target.armor / 100));
        }
        //Если слабее, наносим сниженный урон.
        else if ((this.damageType - target.armorType) == 1 || (this.damageType - target.armorType) == -2)
        {
            target.TakeDamage(damage * (1 - effCoeff) * (1 - target.armor / 100));
        }
        //Если типы одинаковы, урон не меняется.
        else
        {
            target.TakeDamage(damage * (1 - target.armor / 100));
        }
    }
}
