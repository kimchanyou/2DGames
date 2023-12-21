using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleManager : MonoBehaviour
{
    static GoogleManager instance;
    static bool init;

    BannerView bannerView;
    InterstitialAd interstitialAd;

    public static GoogleManager Instance
    {
        get
        {
            if (init == false)
            {
                init = true;

                GameObject go = GameObject.Find("GoogleManager");
                if (go == null)
                {
                    go = new GameObject("GoogleManager");
                    go.AddComponent<GoogleManager>();
                }

                DontDestroyOnLoad(go);
                instance = go.GetComponent<GoogleManager>();
            }
            return instance;
        }
    }
    public void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            print("Ads Initialized !!");

        });
    }
    public void ShowRanking() => Social.ShowLeaderboardUI();

    public void AddLeaderboard(int score) => Social.ReportScore(score, GPGSIds.leaderboard_ranking, (bool success) => {
        Debug.Log(success ? "Reported score successfully" : "Failed to report score");
    });

    public void RequestBanner()
    {
        #if UNITY_ANDROID
            string bannerId = "ca-app-pub-3940256099942544/6300978111"; //테스트용

        #elif UNITY_IPHONE
            string bannerId = "ca-app-pub-3940256099942544/2934735716";

        #else
            string bannerId = "unexpected_playform";
        #endif

        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        bannerView = new BannerView(bannerId, adaptiveSize, AdPosition.Bottom);

        var request = new AdRequest();

        bannerView.LoadAd(request);
    }

    public void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string interId = "ca-app-pub-3940256099942544/1033173712";

        #elif UNITY_IPHONE
            string interId = "ca-app-pub-3940256099942544/4411468910";

        #else
            string interId = "unexpected_playform";
        #endif

        LoadInterstitialAd(interId);
        ShowInterstitialAd();
    }

    public void LoadInterstitialAd(string interId)
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        var adRequest = new AdRequest();
        //adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(interId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                print("Interstitial ad failed to load" + error);
                return;
            }

            print("Interstitial ad loaded !!" + ad.GetResponseInfo());

            interstitialAd = ad;
            InterstitialEvent(interstitialAd);
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            print("Intersititial ad not ready !!");
        }
    }
    public void InterstitialEvent(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Interstitial ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
}
