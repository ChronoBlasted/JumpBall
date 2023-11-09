using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SkinType
{
    NONE, PLAYER, BALL
}

public class SkinLayout : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] string _skinName;
    [SerializeField] int _price;
    [SerializeField] CurrencyType _currencyType;
    [SerializeField] SkinType _skinType;

    [Header("Ref")]
    [SerializeField] TMP_Text _skinNameTxt;
    [SerializeField] TMP_Text _priceTxt;
    [SerializeField] Image _priceIco;

    [SerializeField] GameObject _buyLayoutGO;

    public void Init()
    {
        _priceTxt.text = _price.ToString();
        _skinNameTxt.text = _skinName;

        //_priceIco.sprite
    }

    public void BuySkin()
    {

    }

    public void HandleOnClick()
    {

        switch (_skinType)
        {
            case SkinType.NONE:
                break;
            case SkinType.PLAYER:
                UIManager.Instance.ShopPanel.ResetAllPlayerSkin();

                UIManager.Instance.ShopPanel.IsPlayerSkinReset = false;
                break;
            case SkinType.BALL:
                UIManager.Instance.ShopPanel.ResetAllBallSkin();

                UIManager.Instance.ShopPanel.IsBallSkinReset = false;
                break;
        }

        _buyLayoutGO.SetActive(true);
    }

    public void HandleResetBuyButton()
    {
        _buyLayoutGO.SetActive(false);
    }
}


