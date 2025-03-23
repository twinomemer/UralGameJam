using UnityEngine;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;

public class TwitchChat : MonoBehaviour
{
    private TwitchClient twitchClient;
    
    void Start()
    {
        ConnectionCredentials credentials = new ConnectionCredentials("_TWITCH_USERNAME", "_TWITCH_OAUTH_TOKEN");//_TWITCH_USERNAME _TWITCH_OAUTH_TOKEN ����� ����� ������
        twitchClient = new TwitchClient();
        twitchClient.Initialize(credentials, "_TWITCH_CHANNEL"); // _TWITCH_CHANNEL ����� ����� ������

        twitchClient.OnMessageReceived += OnMessageReceived;
        twitchClient.Connect();
    }

    private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        // ������������ ��������� ����
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
