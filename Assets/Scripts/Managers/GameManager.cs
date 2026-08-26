using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}

    public bool IsPaused{get; private set;}
    public int TotalDeaths{get; private set;}

    public event Action<int> DeathCountChanged;
    public event Action<bool> PauseStateChanged;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        Time.timeScale = 1f;
    }

    public void RegisterDeath()
    {
        TotalDeaths++;
        DeathCountChanged?.Invoke(TotalDeaths);
        Debug.Log("Total Deaths: " + TotalDeaths);
    }

    public void PauseGame()
    {
        if(IsPaused)
        {
            return;
        }

        IsPaused = true;
        Time.timeScale = 0f;
        PauseStateChanged?.Invoke(IsPaused);
    }

    public void ResumeGame()
    {
        if(!IsPaused)
        {
            return;
        }

        IsPaused = false;
        Time.timeScale = 1f;
        PauseStateChanged?.Invoke(IsPaused);
    }

    public void TogglePause()
    {
        if(IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResetRun()
    {
        TotalDeaths = 0;
        DeathCountChanged?.Invoke(TotalDeaths);
        ResumeGame();
    }
}
