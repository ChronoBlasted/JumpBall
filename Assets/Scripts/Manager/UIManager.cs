using BaseTemplate.Behaviours;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] MenuPanel _menuPanel;
    [SerializeField] GamePanel _gamePanel;

    [SerializeField] SettingPanel _settingPanel;
    [SerializeField] PausePanel _pausePanel;
    [SerializeField] ShopPanel _shopPanel;
    [SerializeField] LeaderboardPanel _leaderboardPanel;

    [SerializeField] BlankPanel _blankPanel;

    Panel _currentPanel, _lastPanel;

    public MenuPanel MenuPanel { get => _menuPanel; }
    public GamePanel GamePanel { get => _gamePanel; }
    public ShopPanel ShopPanel { get => _shopPanel; }
    public LeaderboardPanel LeaderboardPanel { get => _leaderboardPanel; }
    public SettingPanel SettingPanel { get => _settingPanel; }
    public Panel CurrentPanel { get => _currentPanel; }

    public void Init()
    {
        InitPanel();

        GameManager.Instance.OnGameStateChanged += HandleStateChange;

        ChangePanel(_menuPanel);
    }

    public void InitPanel()
    {
        _menuPanel.Init();
        _gamePanel.Init();
        _shopPanel.Init();
        _pausePanel.Init();
        _settingPanel.Init();
        _leaderboardPanel.Init();
    }

    void ChangePanel(Panel newPanel, bool _isAddingCanvas = false)
    {
        if (newPanel == _currentPanel) return;

        if (_currentPanel != null)
        {
            if (_isAddingCanvas == false)
            {
                _currentPanel.ClosePanel();
                _currentPanel.gameObject.SetActive(false);
            }
        }

        _lastPanel = _currentPanel;
        _currentPanel = newPanel;

        _currentPanel.gameObject.SetActive(true);
        _currentPanel.OpenPanel();
    }

    public void OpenLastPanel()
    {
        ChangePanel(_lastPanel);
    }

    #region GameState

    void HandleStateChange(GameState newState)
    {
        switch (newState)
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
    }

    void HandleMenu()
    {
        ChangePanel(_menuPanel);
    }

    void HandleGame()
    {
        ChangePanel(_gamePanel);
    }

    void HandlePause()
    {
        ChangePanel(_pausePanel, true);
    }

    public void HandleOpenSettings()
    {
        ChangePanel(_settingPanel, true);
    }

    public void HandleOpenShop()
    {
        ChangePanel(_shopPanel, true);
    }

    public void HandleOpenLeaderboard()
    {
        ChangePanel(_leaderboardPanel, true);
    }

    public void HandleOpenBlank()
    {
        ChangePanel(_blankPanel);
    }

    #endregion
}
