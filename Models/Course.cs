namespace landing.Models;

public class Course
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string Tag { get; set; }
    
    public float Score { get; set; }
    
    public int CostInUzs { get; set; }
    
    public float CourseDuration { get; set; }

    public float ClassDuration { get; set; }
    
    public string InstructorName { get; set; }

    public string BannerId { get; set; }

    public List<string> Themes { get; set; }

    public string CourseLevel { get; set; }
    
    public string Deadline { get; set; }
    
    
}