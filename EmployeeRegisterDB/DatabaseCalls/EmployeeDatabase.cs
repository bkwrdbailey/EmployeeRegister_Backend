using EmployeeRegisterDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeeRegisterDB.DatabaseCalls;

public class EmployeeDatabase : IEmployeeDatabase
{
    private readonly MongoClient client;
    public EmployeeDatabase(string connectionString) {
        client = new MongoClient(connectionString);
    }

    public async Task<string> getEmployeeNameViaId(int employeeId)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var employeesCollection = database.GetCollection<BsonDocument>("Employee");
        var filter = new BsonDocument("")
    }

    public async Task<bool> createEmployeeRecord(Employee newEmployee)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var employeesCollection = database.GetCollection<BsonDocument>("Employee");

    }

    public async Task<bool> createAttendanceRecord(Attendance newAttendance)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var employeesCollection = database.GetCollection<BsonDocument>("Attendance");

    }

    public async Task<Manager> getManagerRecordById(int managerId)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var employeesCollection = database.GetCollection<BsonDocument>("Manager");

    }
}