namespace EmployeeRegisterDB.Models;

public class EmployeeTabularData
{
    public string? name { get; set; }
    public int empId { get; set; }
    public DateTime date { get; set; }
    public string? attendanceCode { get; set; }
    public string? leaveType { get; set; }
    public int managerId { get; set; }
}