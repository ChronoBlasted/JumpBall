using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    APPLE,
    BANANA,
    CHERRIES,
}

public class FruitController : MonoBehaviour
{
    [SerializeField] FruitType _fruitType;
    [SerializeField] Animator _animator;
    [SerializeField] BoxCollider2D _boxCollider;

    private void Start()
    {
        switch (_fruitType)
        {
            case FruitType.APPLE:
                _animator.Play("AppleIdle");
                break;
            case FruitType.BANANA:
                _animator.Play("BananaIdle");
                break;
            case FruitType.CHERRIES:
                _animator.Play("CherriesIdle");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15 || collision.gameObject.layer == 10)
        {
            _animator.Play("CollectFruit");

            switch (_fruitType)
            {
                case FruitType.APPLE:
                    ProfileManager.Instance.AddCurrency(CurrencyType.APPLE, 1);
                    break;
                case FruitType.BANANA:
                    ProfileManager.Instance.AddCurrency(CurrencyType.BANANA, 1);
                    break;
                case FruitType.CHERRIES:
                    ProfileManager.Instance.AddCurrency(CurrencyType.CHERRY, 1);
                    break;
            }

            _boxCollider.enabled = false;
        }
    }
}
