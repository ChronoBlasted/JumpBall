using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform _startTransform;

    private void Awake()
    {
        PlayerController.Instance.transform.position = _startTransform.position;

        StartLevel();
    }

    public void StartLevel()
    {
        PlayerController.Instance.OnStartLevel();
    }

    public void EndLevel()
    {
        PlayerController.Instance.OnEndLevel();
    }
}
