using EmployeeRegisterDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeeRegisterDB.DatabaseCalls;

public class EmployeeDatabase : IEmployeeDatabase
{
    private readonly MongoClient client;
    public EmployeeDatabase(string connectionString)
    {
        MongoClientSettings settings = MongoClientSettings.FromUrl(
            new MongoUrl(connectionString)
        );
        settings.SslSettings = new SslSettings() { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
        client = new MongoClient(settings);
    }

    public async Task<EmployeeDB> getEmployeeNameViaId(int employeeId)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var employeesCollection = database.GetCollection<EmployeeDB>("Employee");
        var filter = Builders<EmployeeDB>.Filter.Eq(e => e.empId, employeeId);

        var employeeData = await employeesCollection.Find<EmployeeDB>(filter).FirstOrDefaultAsync() ?? null;

        return employeeData;
    }

    public async Task<bool> createEmployeeRecord(EmployeeDB newEmployee)
    {
        var database = client.GetDatabase("EmployeeRegistrar");
        var employeesCollection = database.GetCollection<EmployeeDB>("Employee");

        var checkId = await employeesCollection.Find<EmployeeDB>(Builders<EmployeeDB>.Filter.Eq(e => e.empId, newEmployee.empId)).FirstOrDefaultAsync();
        var checkName = await employeesCollection.Find<EmployeeDB>(Builders<EmployeeDB>.Filter.Eq(e => e.name, newEmployee.name)).FirstOrDefaultAsync();

        if (checkId == null && checkName == null)
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

        await attendancesCollection.InsertOneAsync(newAttendance);
        
        return true;
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