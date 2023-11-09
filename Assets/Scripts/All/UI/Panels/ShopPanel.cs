using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : Panel
{
    [SerializeField] List<SkinLayout> _skinPlayerLayouts;
    [SerializeField] List<SkinLayout> _skinBallLayouts;

    bool _isPlayerSkinReset;
    bool _isBallSkinReset;

    public bool IsPlayerSkinReset { get => _isPlayerSkinReset; set => _isPlayerSkinReset = value; }
    public bool IsBallSkinReset { get => _isBallSkinReset; set => _isBallSkinReset = value; }

    public override void Init()
    {
        base.Init();

        foreach (SkinLayout skinLayout in _skinPlayerLayouts)
        {
            skinLayout.Init();
        }

        foreach (SkinLayout skinLayout in _skinBallLayouts)
        {
            skinLayout.Init();
        }

        _isPlayerSkinReset = true;
        _isBallSkinReset = true;
    }

    public void ResetAllPlayerSkin()
    {
        if (_isPlayerSkinReset) return;

        foreach (SkinLayout skinLayout in _skinPlayerLayouts)
        {
            skinLayout.HandleResetBuyButton();
        }

        _isPlayerSkinReset = true;
    }

    public void ResetAllBallSkin()
    {
        if (_isBallSkinReset) return;

        foreach (SkinLayout skinLayout in _skinBallLayouts)
        {
            skinLayout.HandleResetBuyButton();
        }

        _isBallSkinReset = true;
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();

        ResetAllPlayerSkin();
        ResetAllBallSkin();
    }
}
