using Microsoft.AspNetCore.Components;
using Radzen;

namespace XmlConverter.UI.Components.ToolTipService
{
    public class CustomTooltipService(TooltipService tooltipService) : IToolTipService
    {
        public void ShowTooltip(ElementReference elementReference, string message)
        {
            var options = GetOptions();

            tooltipService!.Open(elementReference,
                                 message,
                                 options);
        }

        public void HideTooltip()
        {
            tooltipService.Close();
        }

        private TooltipOptions GetOptions()
        {
            var options = new TooltipOptions()
            {
                Duration = 0,
                Delay = 500,
            };

            return options;
        }
    }
}
