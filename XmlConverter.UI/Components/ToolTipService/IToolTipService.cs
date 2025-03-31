using Microsoft.AspNetCore.Components;

namespace XmlConverter.UI.Components.ToolTipService
{
    public interface IToolTipService
    {
        void ShowTooltip(ElementReference elementReference, string message);

        void HideTooltip();
    }
}
