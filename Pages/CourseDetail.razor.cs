using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Components;
using landing.Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;

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
	private Course course = new Course(){Themes=new()};
	private Instructor instructor = new();
	private static byte Discount = 0;
	private static List<Course> courses = new();
	private static List<Course> PaginatedCourses = new();
	private Connection connection = new();
	private int paginatedCount = 1;
	private StringBuilder tab = new StringBuilder("");
	private StringBuilder time = new StringBuilder(DateTime.Now.ToString("ddd, dd MMM, HH':'mm, yyyy "));
	private List<StringBuilder> tabs = new List<StringBuilder>(){new("active"), new(), new()};

	protected override async Task OnParametersSetAsync()
	{
		course = (await Http.GetFromJsonAsync<List<Course>>("data/courses.json"))
			.Where(c => c.Title==CourseTitle).FirstOrDefault();
		Discount = (byte)((1 - (double)course.OnlineCourseCost/(double)course.CostInUzs)*100);
		courses = (await Http.GetFromJsonAsync<List<Course>>("data/courses.json"))
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