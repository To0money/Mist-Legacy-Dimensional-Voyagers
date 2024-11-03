using LK.SimpleFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI∞¥≈•¿‡–Õ
/// </summary>
public class UIButtonView : UIBase, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDescribable
{
    public Image target;
    public bool SetNativeSize;
    public Sprite normal;
    public Sprite highlight;
    public Sprite pressed;

    public UnityEvent mouseClick;
    public string description;

    public string GetDescription()
    {
        return description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mouseClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        target.sprite = pressed;
        if(SetNativeSize) target.SetNativeSize();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        target.sprite = highlight;
        if (SetNativeSize) target.SetNativeSize();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        target.sprite = normal;
        if (SetNativeSize) target.SetNativeSize();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        target.sprite = highlight;
        if (SetNativeSize) target.SetNativeSize();
    }

    private void Start()
    {
        target.sprite = normal;
        if (SetNativeSize) target.SetNativeSize();
    }
}
