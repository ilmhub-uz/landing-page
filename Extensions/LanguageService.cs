using Microsoft.AspNetCore.Components;

namespace AKSoftware.Localization.MultiLanguages.Blazor
{
    public static class BlazorExtensions
    {
        public static void InitLocalizedComponent(this ILanguageContainerService language, ComponentBase component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var extension = new ComponentExtension()
            {
                Component = component,
            };
            
            var action = new Action<object>(e =>
            {
                var type = typeof(ComponentBase);
                var stateHasChangedMethod = type.GetMethod("StateHasChanged", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                stateHasChangedMethod.Invoke(extension.Component, null); 
            });

            extension.Action = action;

            language.AddExtension(extension);
        }

    }
    public class ComponentExtension : IExtension
    {
        public object Component { get; set; }
        public Action<object> Action { get; set; }
    }
}