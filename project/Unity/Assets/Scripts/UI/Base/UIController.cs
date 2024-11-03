using LK.SimpleFramework;

/// <summary>
/// UI控制器
/// </summary>
public class UIController : ControllerBase
{
    /// <summary>
    /// 当前正在聚焦的UI对象
    /// </summary>
    public EventProperty<UIBase> Focusing { get; private set; }

    public override void Init()
    {
        Focusing = new EventProperty<UIBase>(null);
    }
}
