namespace EmployeeRegisterDB.Models;


public class Attendance
{
    public int employeeId { get; set; }
    public string? attendanceCode { get; set; }
    public DateTime dateCreated { get; set; }
    public string? leaveType { get; set; }
}