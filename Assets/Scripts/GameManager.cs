using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Character streamer;
    [SerializeField] private Character watcher;
    
    [SerializeField] private float blessingDamage;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    private void BlessingCompare(Character _streamer, Character _watcher)
    {
        //Если наш бог сильнее бога  оппонента, наносим урон со старта.
        if ((_streamer.healthType - _watcher.healthType) == -1 || (_streamer.healthType - _watcher.healthType) == 2)
        {
            _watcher.TakeDamage(blessingDamage);
        }
        //Если слабее, получаем.
        else if ((_streamer.healthType - _watcher.healthType) == 1 || (_streamer.healthType - _watcher.healthType) == -2)
        {
            _streamer.TakeDamage(blessingDamage);
        }
        //Если типы одинаковы, то ничего не происходит.
        else
        {
            Debug.Log("vse doma");
        }
    }
}
