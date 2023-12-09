using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterstitialLogic : MonoBehaviour
{
    [SerializeField] private Button interstitialButton;

    [SerializeField] private int minBannerShown;
    [SerializeField] private float interstitialCooldown;

    [SerializeField] private bool isEnoughBannerShown;
    [SerializeField] private bool isInterstitialCooldown;

    public event EventHandler InterstitialLauncher;

    private void Start()
    {
        interstitialButton.interactable = false;
    }

    public void OnBannerCountChanged(int count)
    {
        isEnoughBannerShown = count >= minBannerShown;
        UpdateInterstitialButtonInteractable();
    }

    public void CallInterstitialLauncher()
    {
        InterstitialLauncher?.Invoke(this, EventArgs.Empty);
        isInterstitialCooldown = true;
        interstitialButton.interactable = false;
        StartCoroutine(InterstitialCooldownCoroutine());
    }

    private IEnumerator InterstitialCooldownCoroutine()
    {
        yield return new WaitForSeconds(interstitialCooldown);
        isInterstitialCooldown = false;
        UpdateInterstitialButtonInteractable();
    }

    private void UpdateInterstitialButtonInteractable()
    {
        interstitialButton.interactable = isEnoughBannerShown && !isInterstitialCooldown;
    }
}
