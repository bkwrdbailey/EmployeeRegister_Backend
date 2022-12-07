using EmployeeRegisterDB.Models;

namespace EmployeeRegisterDB.DatabaseCalls;

public interface IEmployeeDatabase
{
    Task<string> getEmployeeNameViaId(int employeeId);
    Task<bool> createEmployeeRecord(Employee newEmployee);
    Task<bool> createAttendanceRecord(Attendance newAttendance);
    Task<Manager> getManagerRecordById(int managerId);
}