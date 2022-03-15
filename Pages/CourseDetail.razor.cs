using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Components;
using landing.Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using AKSoftware.Localization.MultiLanguages.Blazor;
using System.Globalization;
using Blazored.LocalStorage;

namespace landing.Pages;

public partial class CourseDetail
{
    [Parameter]
	public string CourseTitle { get; set; } = "";
	[Inject]
    public HttpClient Http { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public IJSRuntime jsRuntime { get; set; }
	[Inject]
    public ILocalStorageService localStorage { get; set; }
	
	private Course course = new Course(){Themes=new()};
	private Instructor instructor = new();
	private static List<Course> courses = new();
	private static List<Course> PaginatedCourses = new();
	private Connection connection = new();
	private int paginatedCount = 1;
	private StringBuilder tab = new StringBuilder("");
	private List<StringBuilder> tabs = new List<StringBuilder>(){new("active"), new(), new()};

	protected override async Task OnInitializedAsync()
	{
		languageContainer.InitLocalizedComponent(this);
        await base.OnInitializedAsync();
		await OnParametersSetAsync();
	}
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		var courseLanguage = (new CultureInfo((await localStorage.GetItemAsync<string>("culture"))??"uz-UZ")).TwoLetterISOLanguageName;
		course = (await Http.GetFromJsonAsync<List<Course>>($"data/courses.{courseLanguage}.json"))
			.FirstOrDefault(c => c.Title==CourseTitle);
		
		courses = (await Http.GetFromJsonAsync<List<Course>>($"data/courses.{courseLanguage}.json"))
			.Where(c => c.InstructorName == course.InstructorName && c.Title!=course.Title).ToList();
		StateHasChanged();
	}

	protected override async Task OnParametersSetAsync()
	{
		var courseLanguage = (new CultureInfo((await localStorage.GetItemAsync<string>("culture"))??"uz-UZ")).TwoLetterISOLanguageName;
		course = (await Http.GetFromJsonAsync<List<Course>>($"data/courses.{courseLanguage}.json"))
			.FirstOrDefault(c => c.Title==CourseTitle);

		courses = (await Http.GetFromJsonAsync<List<Course>>($"data/courses.{courseLanguage}.json"))
			.Where(c => c.InstructorName == course.InstructorName && c.Title!=course.Title).ToList();

		connection = await Http.GetFromJsonAsync<Connection>("credential.json");

		instructor = (await Http.GetFromJsonAsync<List<Instructor>>("data/instructors.json"))
			.Where(c => c.InstructorName == course.InstructorName).FirstOrDefault();
		PaginatedCourses = courses.Skip(0).Take(3).ToList();

		await jsRuntime.InvokeVoidAsync("SetCountryCode");
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("tab", out var param))
        {
            tab = new StringBuilder(param);
        }

		if(tab.ToString()=="register")
		{
			tabs[2] = new("active");
			tabs[0].Clear();
			tabs[1].Clear();
		}
		else if(tab.ToString() == "instructor")
		{
			tabs[2].Clear();
			tabs[0].Clear();
			tabs[1] = new("active");
		}
	}

	private void ChangeQueryTab(string active)
	{
		tab = new StringBuilder(active);
	}

	private void GetPaginatedCourses(int page)
	{
		if(page==0)
		{
			paginatedCount = paginatedCount+1 <= (courses.Count()+2)/3?++paginatedCount:paginatedCount;
		}
		else if(page==-1)
		{
			paginatedCount = paginatedCount > 1?--paginatedCount:paginatedCount;
		}
		else
		{
			paginatedCount = page;
		}
		PaginatedCourses = courses.Skip((paginatedCount-1)*3).Take(3).ToList();
	}
}