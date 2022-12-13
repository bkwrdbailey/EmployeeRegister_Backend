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

        if (checkManager == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<bool> checkManagerData(int managerId, string managerName)
    {
        Manager checkManager = await _db.getManagerRecordByName(managerName);

        if (checkManager == null)
        {
            return false;
        }
        else if (checkManager.managerId != managerId || checkManager.name != managerName)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<bool> addNewEmployeeRecord(Employee newEmployee)
    {
        // Creating and setting backend employee class model values via frontend employee class model
        EmployeeDB newEmployeeDB = new EmployeeDB();

        newEmployeeDB.empId = newEmployee.empId;
        newEmployeeDB.name = newEmployee.name.ToLower();
        newEmployeeDB.department = newEmployee.department.ToLower();
        newEmployeeDB.designation = newEmployee.designation.ToLower();
        newEmployeeDB.managerId = newEmployee.managerId;
        newEmployeeDB.managerName = newEmployee.managerName.ToLower();
        newEmployeeDB.startingDate = newEmployee.startingDate;

        if (await _db.getManagerRecordById(newEmployee.empId) != null)
        {
            return false;
        }
        else if (await _db.getManagerRecordByName(newEmployee.name) != null)
        {
            return false;
        }

        return await _db.createEmployeeRecord(newEmployeeDB);
    }

    public async Task<bool> addNewAttendanceRecords(EmployeeTabularData[] employeeAttendanceRecords)
    {
        foreach (var record in employeeAttendanceRecords)
        {
            Attendance newAttendanceRecord = new Attendance();

            newAttendanceRecord.empId = record.empId;
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
        EmployeeDB checkEmployee = await _db.getEmployeeNameViaId(employeeId);
        
        if (checkEmployee != null)
        {
            return checkEmployee.name;
        }
        else
        {
            return "DNE";
        }
    }
}