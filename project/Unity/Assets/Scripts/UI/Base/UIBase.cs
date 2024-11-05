using LK.SimpleFramework;
using UnityEngine.EventSystems;

/// <summary>
/// UI»ùÀà¡£
/// </summary>
public abstract class UIBase : UIViewBase, IPointerEnterHandler, IPointerExitHandler
{
    protected UIController UICtrl { get; private set; }

    private void Awake()
    {
        UICtrl = GetController<UIController>();
        UIAwake();
    }

    protected virtual void UIAwake() {}

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        UICtrl.Focusing.Value = this;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        UICtrl.Focusing.Value = null;
    }
}
