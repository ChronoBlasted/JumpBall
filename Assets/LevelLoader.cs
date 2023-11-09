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
    public void Init()
    {
        _indexCurrentLevel = SaveHandler.LoadValue("IndexLevel", 1);

        LoadScene(_indexCurrentLevel);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    LoadNextLevel();
                }*/
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

        SceneManager.LoadSceneAsync(levelIndex);

        _transitionAnimator.SetTrigger("End");
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
