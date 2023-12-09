using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedVideoLogic : MonoBehaviour
{
    [SerializeField] private Button rewardedVideoButton;
    [SerializeField] private int rewardedVideoShowCount;
    private const int MaxRewardedVideosPerSession = 3;

    public int RewardedVideoShowCount
    {
        get => rewardedVideoShowCount;
        set
        {
            if (rewardedVideoShowCount != value)
            {
                rewardedVideoShowCount = value;
                RewardedVideoCountChanged?.Invoke(rewardedVideoShowCount);
            }
        }
    }

    public event EventHandler RewardedVideoLauncher;
    public event Action<int> RewardedVideoCountChanged;

    private void Start()
    {
        rewardedVideoButton.interactable = Appodeal.IsLoaded(AppodealAdType.RewardedVideo)
                                           && RewardedVideoShowCount < MaxRewardedVideosPerSession;
    }

    public void CallRewardedVideoLauncher()
    {
        if (RewardedVideoShowCount < MaxRewardedVideosPerSession)
        {
            RewardedVideoLauncher?.Invoke(this, EventArgs.Empty);
            RewardedVideoShowCount++;

            if (RewardedVideoShowCount >= MaxRewardedVideosPerSession)
            {
                rewardedVideoButton.interactable = false;
            }
        }
    }

    public void OnRewardedVideoLoaded(object sender, AdLoadedEventArgs e)
    {
        rewardedVideoButton.interactable = rewardedVideoShowCount < MaxRewardedVideosPerSession;
    }
}
