using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FindHint : MonoBehaviour
{
    private TextMeshProUGUI _titleTMP;

    private void Awake()
    {
        _titleTMP = GetComponent<TextMeshProUGUI>();
    }

    public void Show(string title, bool fadeIn = false)
    {
        _titleTMP.text = $"Find {title.ToUpper()}";
        
        if (fadeIn)
            FadeInAppearance();
    }

    public void FadeInAppearance()
    {
        _titleTMP.DOFade(0, 0);
        _titleTMP.DOFade(1f, 0.5f);
    }
}
