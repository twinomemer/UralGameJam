using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Api;
public class TwitchManager : MonoBehaviour
{
    private Api twitchApi;
    private string clientId = "_CLIENT_ID"; //����� ��������
    private string accessToken = "_ACCESS_TOKEN";//����� �������� 

    void Start()
    {
        twitchApi = new Api();
        twitchApi.Settings.ClientId = clientId;
        twitchApi.Settings.AccessToken = accessToken;
        Debug.Log("���������� � Twitch API!");
    }

}
