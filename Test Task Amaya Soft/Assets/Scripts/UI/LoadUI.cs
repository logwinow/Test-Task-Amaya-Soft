using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    [SerializeField] private Image _loadingScreen;

    public void FadeIn(Action afterFadeIn)
    {
        DisplayUI(true);
        
        DOTween.Sequence().Append(_loadingScreen.DOFade(1f, 3f)).AppendCallback(afterFadeIn.Invoke);
    }

    public void FadeOut(Action afterFadeOut)
    {
        DOTween.Sequence().Append(_loadingScreen.DOFade(0, 1.5f)).AppendCallback(delegate
        {
            DisplayUI(false);
            
            afterFadeOut.Invoke();
        });
    }

    private void DisplayUI(bool value)
    {
        _loadingScreen.gameObject.SetActive(value);
    }
}
