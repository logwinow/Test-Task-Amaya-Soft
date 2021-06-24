using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<DifficultData> _levelsDifficulties;
    [SerializeField] private LevelBuilder _levelBuilder;

    private int currentLevelIndex;

    public UnityEvent onRecycle;

    private void Start()
    {
        GoToLevel(0, 0, playAppearance: true);
    }

    public void GoToNextLevel()
    {
        GoToLevel(currentLevelIndex + 1);
    }

    public void GoToLevel(int index)
    {
        GoToLevel(index, 2);
    }

    private void GoToLevel(int index, float delay, bool pause = true, bool playAppearance = false)
    {
        if (index >= _levelsDifficulties.Count)
        {
            PauseController.Instance.Pause();
            return;
        }
        
        if (pause)
            PauseController.Instance.Pause(false);
        
        DOTween.Sequence().InsertCallback(delay, delegate
        {
            if (pause)
                PauseController.Instance.Resume();
            _levelBuilder.BuildLevel(_levelsDifficulties[index], playAppearance);
        });
        
        currentLevelIndex = index;
    }

    public void Restart()
    {
        onRecycle?.Invoke();
        
        GoToLevel(0, 0, false, true);
    }
}
