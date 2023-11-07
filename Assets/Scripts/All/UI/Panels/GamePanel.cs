using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
    [SerializeField] TMP_Text _bananaTxt, _cherryTxt, _appleTxt;
    [SerializeField] TMP_Text _altitudeTxt, _maxAtitudeTxt;
    public override void Init()
    {
        base.Init();
    }

    public override void OpenPanel()
    {
        base.OpenPanel();

        GameManager.Instance.PlayerCanPlay = true;

        StartGame();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();

        GameManager.Instance.PlayerCanPlay = false;
        PlayerController.Instance.CanCastBlade = false;
    }

    public void StartGame()
    {

    }

    public void UpdateAltitude(int altitude, int maxAltitude)
    {
        _altitudeTxt.text = altitude.ToString();
        _maxAtitudeTxt.text = maxAltitude.ToString();
    }

    public void UpdateCurrency(CurrencyType currency, int amount)
    {
        switch (currency)
        {
            case CurrencyType.NONE:
                break;
            case CurrencyType.BANANA:
                _bananaTxt.text = amount.ToString();
                break;
            case CurrencyType.CHERRY:
                _cherryTxt.text = amount.ToString();
                break;
            case CurrencyType.APPLE:
                _appleTxt.text = amount.ToString();
                break;
        }
    }
}
