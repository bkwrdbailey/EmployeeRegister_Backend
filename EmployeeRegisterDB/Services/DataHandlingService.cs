using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.DatabaseCalls;

namespace EmployeeRegisterDB.Services;

public class DataHandlingService : IDataHandlingService
{
    private readonly IEmployeeDatabase _db;
    public DataHandlingService(IEmployeeDatabase db)
    {
        _db = db;
    }

    public async Task<bool> checkManagerId(int managerId)
    {
        Manager checkManager = await _db.getManagerRecordById(managerId);

        return false;
    }

    public async Task<bool> addNewEmployeeRecord(Employee newEmployee)
    {
        return await _db.createEmployeeRecord(newEmployee);
    }

    public async Task<bool> addNewAttendanceRecords(EmployeeTabularData[] employeeAttendanceRecords)
    {
        foreach (var record in employeeAttendanceRecords)
        {
            Attendance newAttendanceRecord = new Attendance();

            newAttendanceRecord.employeeId = record.empId;
            newAttendanceRecord.attendanceCode = record.attendanceCode;
            newAttendanceRecord.leaveType = record.leaveType;
            newAttendanceRecord.dateCreated = record.date;

            if (!(await _db.createAttendanceRecord(newAttendanceRecord)))
            {
                return false;
            }
        }

        return true;
    }

    public async Task<string> getEmployeeName(int employeeId)
    {
        string nameCheck = await _db.getEmployeeNameViaId(employeeId);

        if (nameCheck != null)
        {
            return nameCheck;
        }
        else
        {
            return "";
        }
    }
}