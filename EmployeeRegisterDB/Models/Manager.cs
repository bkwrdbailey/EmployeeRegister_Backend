using MongoDB.Bson;


namespace EmployeeRegisterDB.Models;

public class Manager
{
    public ObjectId id { get; set; }
    public int managerId { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
}