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
    [SerializeField] private SpellLibraryPlayer1 spellLibraryPlayer1;
    [SerializeField] private SpellLibraryPlayer2 spellLibraryPlayer2;
    
    private bool isAnybodyDead = false;
    void Start()
    {
        StartBattle();
    }
    
    void Update()
    {
        
    }

    public void StartBattle()
    {
        streamer.OnDead += KillSomeone;
        
        watcher.OnDead += KillSomeone;
        StartCoroutine(Fighting());
        
        BlessingCompare();
    }
    
    IEnumerator Fighting()
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
}
