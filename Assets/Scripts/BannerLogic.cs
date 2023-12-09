using AppodealStack.Monetization.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerLogic : MonoBehaviour
{
    [SerializeField] private Button bannerButton;
    [SerializeField] private Button bannerCloseButton;
    [SerializeField] private bool isBannerShowing;

    public event EventHandler BannerLauncher;
    public event EventHandler BannerCloser;
    public event Action<int> BannerCountChanged;

    private int bannerShowCount = 0;

    public int BannerShowCount
    {
        get => bannerShowCount;
        set
        {
            if (bannerShowCount != value)
            {
                bannerShowCount = value;
                BannerCountChanged?.Invoke(bannerShowCount);
            }
        }
    }

    private void Start()
    {
        bannerCloseButton.interactable = false;
        isBannerShowing = false;
    }

    public void CallBannerLauncher()
    {
        bannerButton.interactable = false;
        bannerCloseButton.interactable = true;
        BannerShowCount++;
        isBannerShowing = true;
        BannerLauncher?.Invoke(this, EventArgs.Empty);
    }

    public void CallBannerCloser()
    {
        bannerButton.interactable = true;
        bannerCloseButton.interactable = false;
        isBannerShowing = false;
        BannerCloser?.Invoke(this, EventArgs.Empty);
    }

    public void HideBannerTemporarily(object sender, EventArgs e)
    {
        if (isBannerShowing)
        {
            BannerCloser?.Invoke(this, EventArgs.Empty);
        }
        bannerCloseButton.interactable = false;
        bannerButton.interactable = false;
    }

    public void ReturnBannerIfWasShowing(object sender = null, EventArgs e = null)
    {
        if (isBannerShowing)
        {
            BannerLauncher?.Invoke(this, EventArgs.Empty);
            bannerCloseButton.interactable = true;
        }
        else
        {
            bannerButton.interactable = true;
        }        
    }
}
