using LK.SimpleFramework;
using TMPro;
using UnityEngine;

/// <summary>
/// UI��ʾ�ı�����
/// </summary>
public class UIDescriptionView : UIBase
{
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        GetController<UIController>().Focusing.Register((focuse) =>
        {
            if(focuse == null)
            {
                textMesh.text = string.Empty;
            }
            else if (focuse is IDescribable)
            {
                IDescribable describable = focuse as IDescribable;
                textMesh.text = describable.GetDescription();
            }
        });
    }
}
