using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    //public TextMeshProUGUI DetailsText;
    
    void Start()
    {
        SingIn();
    }

    public void SingIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services

            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            //DetailsText.text = "Success \n " + name;
            Debug.LogWarning("Sign in Success!!");
        }
        else
        {
            //DetailsText.text = "Sign in Failed!!";
            Debug.LogWarning("Sign in Failed!!");
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
