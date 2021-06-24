using UnityEngine;

public class PauseController : Singleton<PauseController>
{
    [SerializeField] private PauseUI _pauseUi;
    
    public bool IsPaused { get; private set; }

    public void Pause(bool displayUI = true)
    {
        SetPause(true, displayUI);
    }

    public void Resume()
    {
        SetPause(false, false);
    }

    private void SetPause(bool isPause, bool displayUI)
    {
        IsPaused = isPause;
        
        DisplayUI(displayUI);
    }

    public void DisplayUI(bool value)
    {
        if (value)
            _pauseUi.Show();
        else
        {
            _pauseUi.Close();
        }
    }
}
