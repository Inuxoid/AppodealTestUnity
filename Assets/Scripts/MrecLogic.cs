using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MrecLogic : MonoBehaviour
{
    [SerializeField] private Button mrecButton;
    [SerializeField] private Button mrecCloseButton;
    [SerializeField] private int minRewardedVideosShown;

    public event EventHandler MrecLauncher;
    public event EventHandler MrecCloser;

    private void Start()
    {
        mrecCloseButton.interactable = false;
        mrecCloseButton.gameObject.SetActive(false);
        mrecButton.gameObject.SetActive(false);
    }

    public void OnRewardedVideosCountChanged(int count)
    {
        mrecButton.gameObject.SetActive(count >= minRewardedVideosShown);
        mrecCloseButton.gameObject.SetActive(count >= minRewardedVideosShown);
    }

    public void CallMrecLauncher()
    {
        MrecLauncher?.Invoke(this, EventArgs.Empty);
        mrecCloseButton.interactable = true;
        mrecButton.interactable = false;
    }

    public void CallMrecCloser()
    {
        MrecCloser?.Invoke(this, EventArgs.Empty);
        mrecCloseButton.interactable = false;
        mrecButton.interactable = true;
    }
}
