using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Api;
public class TwitchManager : MonoBehaviour
{
    private Api twitchApi;
    private string clientId = "_CLIENT_ID"; //нужно вставить
    private string accessToken = "_ACCESS_TOKEN";//нужно вставить 

    void Start()
    {
        twitchApi = new Api();
        twitchApi.Settings.ClientId = clientId;
        twitchApi.Settings.AccessToken = accessToken;
        Debug.Log("Подключено к Twitch API!");
    }

}
