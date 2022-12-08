using EmployeeRegisterDB.Models;

namespace EmployeeRegisterDB.DatabaseCalls;

public interface IEmployeeDatabase
{
    Task<string> getEmployeeNameViaId(int employeeId);
    Task<bool> createEmployeeRecord(EmployeeDB newEmployee);
    Task<bool> createAttendanceRecord(Attendance newAttendance);
    Task<Manager> getManagerRecordById(int managerId);
    Task<Manager> getManagerRecordByName(string managerName);
}