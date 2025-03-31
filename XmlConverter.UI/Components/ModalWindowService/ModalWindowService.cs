using Microsoft.AspNetCore.Components;
using Radzen;
using XmlConverter.UI.Components.ModalDialogs;

namespace XmlConverter.UI.Components.ModalWindowService
{
    public sealed class ModalWindowService(DialogService dialogService) : IModalWindowService
    {
        public async Task OpenAsync<T>(
            string title,
            Dictionary<string, object> parameters = null!)
            where T : ComponentBase
        {
            await dialogService!.OpenAsync<T>(
                title,
                parameters,
                new DialogOptions
                {
                    CloseDialogOnEsc = true,
                    CloseDialogOnOverlayClick = false,
                    Draggable = false,
                    Resizable = true,
                });
        }

        public void Close(dynamic result = null!)
        {
            dialogService.Close(result);
        }

        public async Task Alert(string message)
        {
            var alertMessage = string.IsNullOrEmpty(message) ? "Operation completed successfully" : message;
            await dialogService.OpenAsync<AlertMessageModalComponent>(title: string.Empty,
                new Dictionary<string, object>
                {
                    { "Message", alertMessage },
                    { "Title", string.Empty }
                },
                new DialogOptions
                {
                    CloseDialogOnEsc = true,
                    CloseDialogOnOverlayClick = false,
                    Draggable = false,
                    Resizable = true,
                });
        }
    }
}
