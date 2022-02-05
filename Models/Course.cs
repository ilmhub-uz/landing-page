namespace landing.Models;

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