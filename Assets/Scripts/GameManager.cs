using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Streamer streamer;
    [SerializeField] private Character watcher;
    
    [SerializeField] private float blessingDamage;

    private StreamerData streamerData;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void StartBattle()
    {
        streamer.Initialize(streamerData);
    }
    
    private void BlessingCompare()
    {
        //Если наш бог сильнее бога  оппонента, наносим урон со старта.
        if ((streamer.healthType - watcher.healthType) == -1 || (streamer.healthType - watcher.healthType) == 2)
        {
            watcher.TakeDamage(blessingDamage);
        }
        //Если слабее, получаем.
        else if ((streamer.healthType - watcher.healthType) == 1 || (streamer.healthType - watcher.healthType) == -2)
        {
            streamer.TakeDamage(blessingDamage);
        }
        //Если типы одинаковы, то ничего не происходит.
        else
        {
            Debug.Log("vse doma");
        }
    }
}
