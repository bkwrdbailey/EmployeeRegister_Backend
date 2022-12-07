using EmployeeRegisterDB.Models;

namespace EmployeeRegisterDB.Services;

public interface IDataHandlingService
{
    Task<bool> checkManagerId(int managerId);
    Task<bool> addNewEmployeeRecord(Employee newEmployee);
    Task<bool> addNewAttendanceRecords(EmployeeTabularData[] employeeAttendanceRecords);
    Task<string> getEmployeeName(int employeeId);
}