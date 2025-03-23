using UnityEngine;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;

public class TwitchChat : MonoBehaviour
{
    private TwitchClient twitchClient;
    
    void Start()
    {
        ConnectionCredentials credentials = new ConnectionCredentials("_TWITCH_USERNAME", "_TWITCH_OAUTH_TOKEN");//_TWITCH_USERNAME _TWITCH_OAUTH_TOKEN Нужно будет ввести
        twitchClient = new TwitchClient();
        twitchClient.Initialize(credentials, "_TWITCH_CHANNEL"); // _TWITCH_CHANNEL Нужно будет ввести

        twitchClient.OnMessageReceived += OnMessageReceived;
        twitchClient.Connect();
    }

    private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        // Обрабатываем сообщения чата
        Debug.Log($"Message from {e.ChatMessage.Username}: {e.ChatMessage.Message}");
    }
   
    void OnDestroy()
    {
        if (twitchClient.IsConnected)
        {
            twitchClient.Disconnect();
        }
    }
}
