using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoSingleton<LevelLoader>
{
    [SerializeField] Animator _transitionAnimator;
    [SerializeField] float _transitionTime = 1f;

    Coroutine _levelLoadingCor;

    int _indexCurrentLevel;

    public int IndexCurrentLevel { get => _indexCurrentLevel; }

    public void Init()
    {
        _indexCurrentLevel = SaveHandler.LoadValue("IndexLevel", 1);

        LoadScene(_indexCurrentLevel, LoadSceneMode.Single);

        LoadScene(_indexCurrentLevel + 1);
        LoadScene(_indexCurrentLevel + 2);
        LoadScene(_indexCurrentLevel + 3);
        LoadScene(_indexCurrentLevel + 4);
    }



    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        _levelLoadingCor = StartCoroutine(LoadLevelCor(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevel(int levelIndex)
    {
        _levelLoadingCor = StartCoroutine(LoadLevelCor(_indexCurrentLevel));
    }

    IEnumerator LoadLevelCor(int levelIndex)
    {
        _transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        LoadScene(levelIndex);

        _transitionAnimator.SetTrigger("End");
    }

    public void LoadScene(int index, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
    {
        SceneManager.LoadScene(index, loadSceneMode);
    }
}
