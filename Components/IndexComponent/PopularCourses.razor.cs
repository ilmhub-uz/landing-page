namespace landing.Components.IndexComponent;

using AKSoftware.Localization.MultiLanguages;
using Blazored.LocalStorage;
using landing.Models;
using Microsoft.AspNetCore.Components;

public partial class PopularCourses : ComponentBase
{
    [Inject]
    public HttpClient HttpClient { get; set; }
    [Inject]
    public ILocalStorageService localStorage { get; set; }

    [Inject]
    public ILanguageContainerService languageContainer { get; set; }
    
    public List<Course> Courses { get; set; } = new List<Course>();

    public List<string> Tags { get; set; } = new List<string> {};

    public List<Course> SelectedCourses { get; set; }
    
    public string SelectedTag { get; set; }
    private string Capitalize(string text)
    {
        if(string.IsNullOrWhiteSpace(text)) return text;

        var arr = text.ToLower().ToCharArray();
        arr[0] = char.ToUpper(arr[0]);
        return new string(arr);
    }
}