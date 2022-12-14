using EmployeeRegisterDB.Models;

namespace EmployeeRegisterDB.Services;

public interface IDataHandlingService
{
    Task<bool> checkManagerId(int managerId);
    Task<bool> checkManagerData(int managerId, string managerName);
    Task<bool> addNewEmployeeRecord(Employee newEmployee);
    Task<bool> addNewAttendanceRecords(EmployeeTabularData[] employeeAttendanceRecords);
    Task<Employee> getEmployeeName(int employeeId);
}