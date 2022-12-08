using MongoDB.Bson;

namespace EmployeeRegisterDB.Models;

public class Employee
{
    public ObjectId id { get; set; }
    public int empId { get; set; }
    public string? name { get; set; }
    public string? designation { get; set; }
    public string? department { get; set; }
    public string? managerName { get; set; }
    public int managerId { get; set; }
    public DateTime startingDate { get; set; }
}