using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private GameObject backCanvas;
    [SerializeField] private GameObject WinCanvas;
    [SerializeField] private TextMeshProUGUI WinText;
    
    private bool isAnybodyDead = false;
    private Character RoundWinner;
    void Start()
    {
        inputManager.BuildingEnded += StartBattle;
        
        streamer.OnDamaged += streamer._healthBar.DecreaseValue;
        streamer.OnDead += streamer._healthBar.Hide;
        streamer.OnDead += KillSomeone;
        streamer.OnDead += watcher.Win;
        streamer.OnDead += WatcherIsWinner;
        
        watcher.OnDamaged += watcher._healthBar.DecreaseValue;
        watcher.OnDead += watcher._healthBar.Hide;
        watcher.OnDead += KillSomeone;
        watcher.OnDead += streamer.Win;
        watcher.OnDead += StreamerIsWinner;
    }
    
    void Update()
    {
        if (isAnybodyDead)
        {
            WinCanvas.SetActive(true);
            if (RoundWinner == streamer)
            {
                WinText.text = "Победа игрока 1";
            }
            if (RoundWinner == watcher)
            {
                WinText.text = "Победа игрока 2";
            }
            Invoke("Deb1", 3f);
            //StartCoroutine(Deb());
            //inputManager.SetBlock(1, 2);
            //inputManager.canvas.SetActive(true);
        }
    }

    public void StartBattle()
    {
        backCanvas.SetActive(true);
        isAnybodyDead = false;
        
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
        StopAllCoroutines();
        backCanvas.SetActive(false);
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

    private IEnumerator Deb()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }

    private void Deb1()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
