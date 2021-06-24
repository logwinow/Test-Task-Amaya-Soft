using System;
using UnityEngine;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private SpriteRenderer _selfSpriteRenderer;
    [SerializeField] private ParticleSystem _vfxStarsPrefab;
    
    private Symbol _symbol;
    private bool _isRight;
    private Action _callbackIfIsRight;
    private GameObject _vfxStarsGameObject;

    public Vector2 Size => _selfSpriteRenderer.bounds.size;
    public Symbol Symbol => _symbol;

    private void OnDestroy()
    {
        if (_vfxStarsGameObject != null)
            Destroy(_vfxStarsGameObject);
    }

    public void Set(Symbol symbol, bool bounce = false)
    {
        _symbol = symbol;
        
        _icon.sprite = symbol.Sprite;
        transform.rotation *= Quaternion.Euler(0, 0, symbol.OriginRotate);
        
        if (bounce)
            BounceAppearance();
    }

    public void SetAsRight(Action callback)
    {
        _isRight = true;

        _callbackIfIsRight = callback;
    }

    private void OnMouseDown()
    {
        if (PauseController.Instance.IsPaused)
            return;
        
        if (_isRight)
        {
            OnRight();
        }
        else
        {
            OnMistakeAnimation();
        }
    }

    private void OnMistakeAnimation()
    {
        DOTween.Sequence().Append(_icon.transform.DOMoveX(transform.position.x + 0.2f, 0.1f))
            .Append(_icon.transform.DOMoveX(transform.position.x - 0.2f, 0.1f))
            .Append(_icon.transform.DOMoveX(transform.position.x + 0.1f, 0.1f))
            .Append(_icon.transform.DOMoveX(transform.position.x - 0.1f, 0.05f))
            .Append(_icon.transform.DOMoveX(transform.position.x, 0.05f));
    }

    private void OnRightAnimation()
    {
        _icon.transform.DOPunchScale(Vector3.one * 0.05f, 0.5f, elasticity: 0.5f);
    }

    private void OnRight()
    {
        OnRightAnimation();
        _vfxStarsGameObject = Instantiate(_vfxStarsPrefab.gameObject);
        _vfxStarsGameObject.transform.position = transform.position;
        _icon.sortingOrder = 3;
        
        _callbackIfIsRight.Invoke();
    }
    
    public void BounceAppearance()
    {
        var defaultScale = transform.localScale;
        transform.localScale = Vector3.zero;

        DOTween.Sequence()
            .Append(transform.DOScale(defaultScale * 1.2f, 0.5f))
            .Append(transform.DOScale(defaultScale * 0.8f, 0.4f))
            .Append(transform.DOScale(defaultScale * 1.1f, 0.3f))
            .Append(transform.DOScale(defaultScale * 0.9f, 0.2f))
            .Append(transform.DOScale(defaultScale, 0.1f));
    }
}
