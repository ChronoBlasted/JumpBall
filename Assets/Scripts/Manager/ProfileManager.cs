using BaseTemplate.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType { NONE, BANANA, CHERRY, APPLE }

public class ProfileManager : MonoSingleton<ProfileManager>
{
    [SerializeField] int _banana, _cherry, _apple;
    [SerializeField] int _altitude, _maxAltitude;

    public float Coins { get => _banana; }

    PlayerController _playerController;

    public void Init()
    {
        _playerController = PlayerController.Instance;

        LoadData();
        InitValue();
    }

    void LoadData()
    {
        _banana = SaveHandler.LoadValue("banana", 0);
        _cherry = SaveHandler.LoadValue("cherry", 0);
        _apple = SaveHandler.LoadValue("apple", 0);

        _maxAltitude = SaveHandler.LoadValue("maxAltitude", 0);
    }

    void InitValue()
    {
        AddCurrency(CurrencyType.BANANA, 0);
        AddCurrency(CurrencyType.CHERRY, 0);
        AddCurrency(CurrencyType.APPLE, 0);

        UpdateAltitude();
    }

    void Update()
    {
        if (_playerController.transform.position.y * 10 != _altitude) UpdateAltitude();
    }

    public void UpdateAltitude()
    {
        _altitude = (int)(_playerController.transform.position.y * 10);

        if (_altitude > _maxAltitude) _maxAltitude = _altitude;

        UIManager.Instance.GamePanel.UpdateAltitude(_altitude, _maxAltitude);
    }

    public void AddCurrency(CurrencyType currencyType, int amount)
    {
        switch (currencyType)
        {
            case CurrencyType.NONE:
                break;
            case CurrencyType.BANANA:
                _banana += amount;
                if (_banana < 0) _banana = 0;
                SaveHandler.SaveValue("banana", _banana);
                UIManager.Instance.GamePanel.UpdateCurrency(currencyType, _banana);
                break;

            case CurrencyType.CHERRY:
                _cherry += amount;
                if (_cherry < 0) _cherry = 0;
                SaveHandler.SaveValue("cherry", _cherry);
                UIManager.Instance.GamePanel.UpdateCurrency(currencyType, _cherry);
                break;

            case CurrencyType.APPLE:
                _apple += amount;
                if (_apple < 0) _apple = 0;
                SaveHandler.SaveValue("apple", _apple);
                UIManager.Instance.GamePanel.UpdateCurrency(currencyType, _apple);
                break;
        }
    }
}