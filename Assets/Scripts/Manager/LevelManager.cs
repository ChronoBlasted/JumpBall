using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int _level;
    [SerializeField] Transform _startTransform;
    [SerializeField] Transform _mapTransform;


    private void Awake()
    {
        _mapTransform.position = new Vector3(0, 5.5f * _level, 0);

        GameManager.Instance.LevelManagers.Add(this);

        if (LevelLoader.Instance.IndexCurrentLevel == _level + 1)
        {
            Init();
        }
    }

    public void Init()
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
