using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public SceneChanger sceneChanger;
    public GameScene gameScene;

    #region Game status
    private Level currentLevelData;
    private bool isGameWin = false;
    private bool isGameLose = false;
    private bool isGamePause = false;
    private float time = 0;
    #endregion

    private void Start()
    {
        currentLevelData = LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex);
        GameObject mathContainer = Instantiate(currentLevelData.mathContainer);
        OperationManager.instance.SetMathContainer(mathContainer.transform);
        time = currentLevelData.timeLimit;

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (isGameWin || isGameLose || isGamePause)
        {
            return;
        }
        time -= Time.deltaTime;
        gameScene.SetTime(currentLevelData.timeLimit ,time);
        if (time <= 0)
        {
            Lose();
        }
    }

    public void Win()
    {
        if (LevelManager.instance.levelData.GetLevels().Count > LevelManager.instance.currentLevelIndex + 1)
        {
            if (LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex + 1).isPlayable == false)
            {
                LevelManager.instance.levelData.SetLevelData(LevelManager.instance.currentLevelIndex + 1, true, false);
            }
        }
        SetAchivement();

        isGameWin = true;
        StartCoroutine(WaitToWin());
        LevelManager.instance.levelData.SaveDataJSON();
    }

    private void SetAchivement()
    {
        
    }

    public void Lose()
    {
        isGameLose = true;
        StartCoroutine(WaitToLose());
    }

    private IEnumerator WaitToLose()
    {
        yield return new WaitForSecondsRealtime(.5f);
        gameScene.ShowLosePanel();
    }

    private IEnumerator WaitToWin()
    {
        yield return new WaitForSecondsRealtime(.5f);
        gameScene.ShowWinPanel();
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public bool IsGameLose()
    {
        return isGameLose;
    }

    public void PauseGame()
    {
        isGamePause = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isGamePause = false;
        Time.timeScale = 1;
    }

    public bool IsGamePause()
    {
        return isGamePause;
    }
}

