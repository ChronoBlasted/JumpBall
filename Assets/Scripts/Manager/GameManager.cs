using BaseTemplate.Behaviours;
using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState { MENU, GAME, PAUSE }

public class GameManager : MonoSingleton<GameManager>
{
    public event Action<GameState> OnGameStateChanged;

    GameState _gameState;

    Coroutine _launchGameCoroutine;

    public GameState GameState { get => _gameState; }
    public bool PlayerCanPlay;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Time.timeScale = 1;

        Application.targetFrameRate = 60;

        Screen.orientation = ScreenOrientation.Portrait;

        PoolManager.Instance.Init();

        ProfileManager.Instance.Init();

        UIManager.Instance.Init();

        PlayerController.Instance.Init();

        LevelLoader.Instance.Init();

        UpdateStateToMenu();
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReloadScene();
        }
    }

    public void UpdateGameState(GameState newState)
    {
        _gameState = newState;

        Debug.Log(_gameState);

        switch (_gameState)
        {
            case GameState.MENU:
                HandleMenu();
                break;
            case GameState.GAME:
                HandleGame();
                break;
            case GameState.PAUSE:
                HandlePause();
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(_gameState);
    }

    void HandleMenu()
    {
    }

    void HandleGame()
    {
        Time.timeScale = 1;
    }

    void HandlePause()
    {
        Time.timeScale = 0;
    }

    public void UpdateStateToMenu() => UpdateGameState(GameState.MENU);
    public void UpdateStateToGame() => UpdateGameState(GameState.GAME);
    public void UpdateStateToPause() => UpdateGameState(GameState.PAUSE);

    public void ReloadScene()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitApp() => Application.Quit();


}