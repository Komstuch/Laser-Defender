using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;


public class SimpleAds : MonoBehaviour
{
    private BannerView bannerView;
    private AdRequest request;

    //private void Awake()
    //{
    //    SetUpSingleton();
    //}

    void Start()
    {
        // banner iD: ca-app-pub-1987428931362758/2456676520
        // Test ID: ca-app-pub-3940256099942544/6300978111
        // My app  ID: ca-app-pub-1987428931362758~1123413553
        string appId = "ca-app-pub-1987428931362758~1123413553";

        MobileAds.Initialize(appId);
        RequestBanner();
        LoadBanner();
    }
    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-1987428931362758/2456676520";

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        request = new AdRequest.Builder().Build();
    }

    public void LoadBanner()
    {
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        if(SceneManager.GetActiveScene().name != "Win Screen")
        {
            DestroyBanner();
        }
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void DestroyBanner()
    {
        bannerView.Destroy();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
