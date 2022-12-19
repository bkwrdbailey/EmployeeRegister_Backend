using MongoDB.Bson;

namespace EmployeeRegisterDB.Models;

public class Attendance
{
    public ObjectId id { get; set; }
    public int empId { get; set; }
    public string? attendanceCode { get; set; }
    public string dateCreated { get; set; }
    public string? leaveType { get; set; }
}