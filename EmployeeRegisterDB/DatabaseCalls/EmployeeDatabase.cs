using EmployeeRegisterDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeeRegisterDB.DatabaseCalls;

public class EmployeeDatabase : IEmployeeDatabase
{
    private readonly MongoClient client;
    public EmployeeDatabase(string connectionString)
    {
        client = new MongoClient(connectionString);
    }

    public async Task<string> getEmployeeNameViaId(int employeeId)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var employeesCollection = database.GetCollection<EmployeeDB>("Employee");
        var filter = Builders<EmployeeDB>.Filter.Eq(e => e.empId, employeeId);

        var employeeData = await employeesCollection.Find<EmployeeDB>(filter).FirstOrDefaultAsync();

        return employeeData.name ?? "";
    }

    public async Task<bool> createEmployeeRecord(EmployeeDB newEmployee)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var employeesCollection = database.GetCollection<EmployeeDB>("Employee");

        var checkData = await employeesCollection.Find<EmployeeDB>(Builders<EmployeeDB>.Filter.Eq(e => e.empId, newEmployee.empId)).FirstOrDefaultAsync();

        if (checkData == null)
        {
            await employeesCollection.InsertOneAsync(newEmployee);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> createAttendanceRecord(Attendance newAttendance)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var attendancesCollection = database.GetCollection<Attendance>("Attendance");

        Attendance checkData = await attendancesCollection.Find<Attendance>(Builders<Attendance>.Filter.Eq(a => a.dateCreated, newAttendance.dateCreated)).FirstOrDefaultAsync();

        await attendancesCollection.InsertOneAsync(newAttendance);

        if (checkData.dateCreated == newAttendance.dateCreated && checkData.empId == newAttendance.empId)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<Manager> getManagerRecordById(int managerId)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var managersCollection = database.GetCollection<Manager>("Manager");

        return await managersCollection.Find<Manager>(Builders<Manager>.Filter.Eq(m => m.managerId, managerId)).FirstOrDefaultAsync();
    }

    public async Task<Manager> getManagerRecordByName(string managerName)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var managersCollection = database.GetCollection<Manager>("Manager");

        return await managersCollection.Find<Manager>(Builders<Manager>.Filter.Eq(m => m.name, managerName)).FirstOrDefaultAsync();
    }
}