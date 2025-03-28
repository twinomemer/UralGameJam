using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float blessingDamage;
    [SerializeField] private float spellDamage;
    [SerializeField] private float attackInterval;
    
    [SerializeField] private Streamer streamer;
    [SerializeField] private Watcher watcher;
    [SerializeField] private QTESystem qte;
    [SerializeField] private MiniGameColor mgc;
    [SerializeField] private InputManager inputManager;
    
    private bool isAnybodyDead = false;
    private Character RoundWinner;
    void Start()
    {
        inputManager.BuildingEnded += StartBattle;
    }
    
    void Update()
    {
        if (isAnybodyDead)
        {
            //nado vivod kto podebil
            inputManager.SetBlock(1, 2);
            inputManager.canvas.SetActive(true);
        }
    }

    public void StartBattle()
    {
        streamer.OnDead += KillSomeone;
        streamer.OnDead += watcher.Win;
        streamer.OnDead += WatcherIsWinner;
        
        watcher.OnDead += KillSomeone;
        watcher.OnDead += streamer.Win;
        watcher.OnDead += StreamerIsWinner;
        StartCoroutine(Fighting());
        
        BlessingCompare();
    }
    
    private IEnumerator Fighting()
    {
        // Пока событие не произошло, выполняем метод
        while (!isAnybodyDead)
        {
            streamer.Attack(watcher);
            watcher.Attack(streamer);
            yield return new WaitForSeconds(attackInterval);
        }

        Debug.Log("Событие произошло, корутина остановлена.");
    }

    private void KillSomeone()
    {
        isAnybodyDead = true;
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

    private void StreamerIsWinner()
    {
        RoundWinner = streamer;
    }
    private void WatcherIsWinner()
    {
        RoundWinner = watcher;
    }
}
