namespace landing.Components;

using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

public partial class PopularCourses : ComponentBase
{
    [Inject]
    public HttpClient HttpClient { get; set; }
    
    public List<Course> Courses { get; set; } = new List<Course>();

    public List<string> Tags { get; set; } = new List<string> { "Hammasi " };

    public List<Course> SelectedCourses { get; set; }
    
    public string SelectedTag { get; set; }
    
    

    protected override async Task OnInitializedAsync()
    {
        Courses = await HttpClient.GetFromJsonAsync<List<Course>>("data/courses.json");
        
        Tags.AddRange(Courses.Select(c => c.Tag).Distinct().ToList());
        
        SelectedTag = Tags[0];
        // SelectedCourses = courses.Where(c => c.Tag.Contains(SelectedTag, StringComparison.InvariantCultureIgnoreCase)).ToList();

        StateHasChanged();
    }

    private string Capitalize(string text)
    {
        if(string.IsNullOrWhiteSpace(text)) return text;

        var arr = text.ToLower().ToCharArray();
        arr[0] = char.ToUpper(arr[0]);
        return new string(arr);
    }
}


public class Course
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string Tag { get; set; }
    
    public Double Score { get; set; }
    
    public int CostInUzs { get; set; }
    
    public int DurationInMonth { get; set; }

    public string BannerId { get; set; }
}