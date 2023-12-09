using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using AppodealStack.ConsentManagement.Api;
using AppodealStack.ConsentManagement.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AppodealController : MonoBehaviour
{
    [SerializeField] InterstitialLogic interstitialLogic;
    [SerializeField] BannerLogic bannerLogic;
    [SerializeField] RewardedVideoLogic rewardedVideolLogic;
    [SerializeField] MrecLogic mrecLogic;

#if UNITY_EDITOR && !UNITY_ANDROID
        private const string AppKey = "";
#elif UNITY_ANDROID
    private const string AppKey = "05afaf9a80d17276ee15766358f295e4df8a3a5dabb4da79";
#endif

    private void Awake()
    {
        SubsribeToLogicEvents();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Appodeal.Cache(AppodealAdType.Interstitial);
        Appodeal.Cache(AppodealAdType.Banner);
        Appodeal.Cache(AppodealAdType.RewardedVideo);
        Appodeal.Cache(AppodealAdType.Interstitial);

        int adTypes = (AppodealAdType.Mrec) |
                      (AppodealAdType.Banner) |
                      (AppodealAdType.Interstitial) |
                      (AppodealAdType.RewardedVideo);

        Appodeal.Initialize(AppKey, adTypes);
    }

    public void ShowInterstitial()
    {
        if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
        {
            Appodeal.Show(AppodealShowStyle.Interstitial);
        }
    }

    public void ShowTopBanner()
    {
        Appodeal.Show(AppodealShowStyle.BannerTop, "default");  
    }

    public void HideTopBanner()
    {
        Appodeal.Hide(AppodealAdType.Banner);
    }

    public void ShowRewardedVideo()
    {
        if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
        {
            Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
    }

    public void ShowMrec()
    {
        Appodeal.ShowMrecView(AppodealViewPosition.VerticalTop, AppodealViewPosition.HorizontalCenter, "default");
    }

    public void HideMrec()
    {
        Appodeal.HideMrecView();
        bannerLogic.ReturnBannerIfWasShowing();
    }

    private void SubsribeToLogicEvents()
    {
        interstitialLogic.InterstitialLauncher += (sender, e) => { ShowInterstitial(); };

        bannerLogic.BannerLauncher += (sender, e) => { ShowTopBanner(); };
        bannerLogic.BannerCloser += (sender, e) => { HideTopBanner(); };
        bannerLogic.BannerCountChanged += interstitialLogic.OnBannerCountChanged;
        
        AppodealCallbacks.Interstitial.OnShown += bannerLogic.HideBannerTemporarily;
        AppodealCallbacks.Interstitial.OnClosed += bannerLogic.ReturnBannerIfWasShowing;

        AppodealCallbacks.RewardedVideo.OnShown += bannerLogic.HideBannerTemporarily;
        AppodealCallbacks.RewardedVideo.OnClosed += bannerLogic.ReturnBannerIfWasShowing;

        AppodealCallbacks.Mrec.OnShown += bannerLogic.HideBannerTemporarily;

        rewardedVideolLogic.RewardedVideoLauncher += (sender, e) => { ShowRewardedVideo(); };
        rewardedVideolLogic.RewardedVideoCountChanged += mrecLogic.OnRewardedVideosCountChanged;
        AppodealCallbacks.RewardedVideo.OnLoaded += rewardedVideolLogic.OnRewardedVideoLoaded;

        mrecLogic.MrecLauncher += (sender, e) => { ShowMrec(); };
        mrecLogic.MrecCloser += (sender, e) => { HideMrec(); };
    }

    private void UnSubsribeToLogicEvents()
    {
        interstitialLogic.InterstitialLauncher -= (sender, e) => { ShowInterstitial(); };

        bannerLogic.BannerLauncher -= (sender, e) => { ShowTopBanner(); };
        bannerLogic.BannerCloser -= (sender, e) => { HideTopBanner(); };
        bannerLogic.BannerCountChanged -= interstitialLogic.OnBannerCountChanged;

        AppodealCallbacks.Interstitial.OnShown -= bannerLogic.HideBannerTemporarily;
        AppodealCallbacks.Interstitial.OnClosed -= bannerLogic.ReturnBannerIfWasShowing;

        AppodealCallbacks.RewardedVideo.OnShown -= bannerLogic.HideBannerTemporarily;
        AppodealCallbacks.RewardedVideo.OnClosed -= bannerLogic.ReturnBannerIfWasShowing;

        AppodealCallbacks.Mrec.OnShown -= bannerLogic.HideBannerTemporarily;

        rewardedVideolLogic.RewardedVideoLauncher -= (sender, e) => { ShowRewardedVideo(); };
        rewardedVideolLogic.RewardedVideoCountChanged -= mrecLogic.OnRewardedVideosCountChanged;
        AppodealCallbacks.RewardedVideo.OnLoaded -= rewardedVideolLogic.OnRewardedVideoLoaded;

        mrecLogic.MrecLauncher -= (sender, e) => { ShowMrec(); };
        mrecLogic.MrecCloser -= (sender, e) => { HideMrec(); };
    }

    private void OnDestroy()
    {
        UnSubsribeToLogicEvents();
    }
}
