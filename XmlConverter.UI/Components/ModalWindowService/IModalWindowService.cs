using Microsoft.AspNetCore.Components;

namespace XmlConverter.UI.Components.ModalWindowService
{
    public interface IModalWindowService
    {
        public Task OpenAsync<T>(string title, Dictionary<string, object> parameters = null!)
            where T : ComponentBase;

        public Task Alert(string message);

        public void Close(dynamic result = null!);
    }
}
