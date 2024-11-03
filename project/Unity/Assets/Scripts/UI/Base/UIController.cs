using LK.SimpleFramework;

/// <summary>
/// UI������
/// </summary>
public class UIController : ControllerBase
{
    /// <summary>
    /// ��ǰ���ھ۽���UI����
    /// </summary>
    public EventProperty<UIBase> Focusing { get; private set; }

    public override void Init()
    {
        Focusing = new EventProperty<UIBase>(null);
    }
}
