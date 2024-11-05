using LK.SimpleFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI按钮类型。
/// </summary>
public class UIButtonView : UIBase, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDescribable
{
    public Image target;
    public bool SetNativeSize;
    public Sprite normal;
    public Sprite highlight;
    public Sprite pressed;

    public UnityEvent mouseClick;
    public UnityEvent mouseSelected;
    public UnityEvent mouseExit;
    public string description;

    private ButtonState state;
    private bool mouseStay;
    private bool mousePrees;
    /// <summary>
    /// 当前按钮的状态。
    /// </summary>
    public ButtonState State => state;

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
        mousePrees = true;
        state = ButtonState.Pressed;
        UpdateButtonSprite();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        mousePrees = false;
        if(mouseStay)
        {
            state = ButtonState.Selected;
        }
        else 
        {
            state = ButtonState.Normal;
            mouseExit?.Invoke();
        }
        UpdateButtonSprite();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        mouseStay = true;
        if(!mousePrees)
        {
            state = ButtonState.Selected;
            mouseSelected?.Invoke();
        }
        UpdateButtonSprite();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        mouseStay = false;
        if(!mousePrees)
        {
            state = ButtonState.Normal;
            mouseExit?.Invoke();
        }
        UpdateButtonSprite();
    }


    private void Start()
    {
        state = ButtonState.Normal;
        target.sprite = normal;
        if (SetNativeSize) target.SetNativeSize();
    }
    
    /// <summary>
    /// 更新按钮外观。
    /// </summary>
    private void UpdateButtonSprite()
    {
        switch (state)
        {
            case ButtonState.Normal:
            default:
                target.sprite = normal;
                break;
            case ButtonState.Selected:
                target.sprite = highlight;
                break;
            case ButtonState.Pressed:
                target.sprite = pressed;
                break;
        }
        if (SetNativeSize)
        {
            target.SetNativeSize();
        }
    }
}
