using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Image _fadePanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private LoadUI _loadUi;

    public void Show()
    {
        Display(true);

        _fadePanel.DOFade(0.5f, 1f);
    }

    public void Close()
    {
        Display(false);
        
        _fadePanel.DOFade(0f, 1f);
    }

    private void Display(bool value)
    {
        _fadePanel.gameObject.SetActive(value);
        _restartButton.gameObject.SetActive(value);
    }

    public void OnRestartButtonClick()
    {
        Close();
        
        _loadUi.FadeIn(delegate
        {
            GameManager.Instance.Restart();
            _loadUi.FadeOut(PauseController.Instance.Resume);
        });
    }
}
