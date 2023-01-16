using Xunit;
using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.DatabaseCalls;
using EmployeeRegisterDB.Services;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;

namespace EmployeeRegisterDB_Tests.ServiceTests;

public class DataHandlingServiceTests
{
    private Mock<IEmployeeDatabase> _context = new Mock<IEmployeeDatabase>();

    [Fact]
    public void TestDependencyInjection()
    {
        DataHandlingService dataService = new DataHandlingService(_context.Object);
        Assert.NotNull(dataService);
    }

    [Fact]
    public async void CheckManagerIdService_Verifies_Manager_Id_Given_Belongs_To_An_Existing_Manager_Record()
    {
        Manager mockManager = new Manager()
        {
            id = ObjectId.GenerateNewId(),
            managerId = 1234,
            name = "Bobby Testman",
            email = "testemail@test.com"
        };


        _context.Setup(test => test.getManagerRecordById(1234)).ReturnsAsync(mockManager);

        DataHandlingService dataService = new DataHandlingService(_context.Object);
        bool result = await dataService.checkManagerId(1234);
        bool secondResult = await dataService.checkManagerId(1111);

        Assert.Equal(true, result);
        Assert.Equal(false, secondResult);
    }

    [Fact]
    public async void CheckMangerDataService_Manager_Id_And_Name_Should_Match_Same_Record()
    {
        Manager mockManager = new Manager()
        {
            id = ObjectId.GenerateNewId(),
            managerId = 1234,
            name = "bobby testman",
            email = "testemail@test.com"
        };

        _context.Setup(test => test.getManagerRecordByName("bobby testman")).ReturnsAsync(mockManager);

        DataHandlingService dataService = new DataHandlingService(_context.Object);
        bool result = await dataService.checkManagerData(1234, "bobby testman");
        bool secondResult = await dataService.checkManagerData(1234, "jim gordon");

        Assert.Equal(true, result);
        Assert.Equal(false, secondResult);
    }

    [Fact]
    public async void addNewEmployeeRecordService_Does_Not_Create_New_EmployeeRecord_With_Matching_Data_To_A_ManagerRecord()
    {
        Manager mockManager = new Manager()
        {
            id = ObjectId.GenerateNewId(),
            managerId = 1234,
            name = "bobby testman",
            email = "testemail@test.com"
        };

        // Database employee model
        EmployeeDB mockEmployeeDB = new EmployeeDB()
        {
            id = ObjectId.GenerateNewId(),
            empId = 1234,
            name = "bobby testman",
            designation = "cashier",
            department = "frontend",
            managerName = "bobby testman",
            managerId = 1234,
            startingDate = new DateTime()
        };

        // Frontend employee models
        Employee mockEmployee = new Employee()
        {
            empId = 7777,
            name = "bobby testman",
            designation = "cashier",
            department = "frontend",
            managerName = "bobby testman",
            managerId = 1234,
            startingDate = new DateTime()
        };

        _context.Setup(test => test.createEmployeeRecord(mockEmployeeDB)).ReturnsAsync(true);
        _context.Setup(test => test.getManagerRecordById(mockEmployeeDB.empId)).ReturnsAsync(mockManager);
        _context.Setup(test => test.getManagerRecordByName(mockEmployeeDB.name)).ReturnsAsync(mockManager);

        DataHandlingService dataService = new DataHandlingService(_context.Object);
        bool result = await dataService.addNewEmployeeRecord(mockEmployee);

        Assert.Equal(false, result);
    }

    [Fact]
    public async void addNewAttendanceRecordsService_Successfully_Adds_Each_New_Attendance_Record()
    {
        EmployeeTabularData[] listOfEmployeeAttendances = new EmployeeTabularData[]
        {
            new EmployeeTabularData {
                name = "bill martin",
                empId = 4231,
                date = "12/7/2022",
                attendanceCode = "present",
                leaveType = "N/A",
                managerId = 1234
            },
            new EmployeeTabularData {
                name = "carl winslow",
                empId = 4122,
                date = "12/7/2022",
                attendanceCode = "absent",
                leaveType = "sick",
                managerId = 1234
            }
        };

        Attendance employeeOne = new Attendance()
        {
            id = ObjectId.GenerateNewId(),
            empId = 4231,
            attendanceCode = "present",
            dateCreated = "12/7/2022",
            leaveType = "N/A"
        };

        Attendance employeeTwo = new Attendance()
        {
            id = ObjectId.GenerateNewId(),
            empId = 4122,
            attendanceCode = "absent",
            dateCreated = "12/7/2022",
            leaveType = "sick"
        };

        _context.Setup(x => x.createAttendanceRecord(employeeOne)).ReturnsAsync(true);
        _context.Setup(x => x.createAttendanceRecord(employeeTwo)).ReturnsAsync(true);

        DataHandlingService dataService = new DataHandlingService(_context.Object);
        bool result = await dataService.addNewAttendanceRecords(listOfEmployeeAttendances);

        Assert.Equal(true, result);
    }

    [Fact]
    public async void getEmployeeNameService_Returns_Correct_Employee_Record()
    {
        EmployeeDB employeeRecord = new EmployeeDB()
        {
            id = ObjectId.GenerateNewId(),
            empId = 6231,
            name = "evan junior",
            designation = "janitor",
            department = "frontend",
            managerId = 1234,
            managerName = "bobby testman",
            startingDate = new DateTime()
        };

        _context.Setup(x => x.getEmployeeNameViaId(6231)).ReturnsAsync(employeeRecord);

        DataHandlingService dataService = new DataHandlingService(_context.Object);
        Employee result = await dataService.getEmployeeName(6231);

        Assert.Equal("evan junior", result.name);
    }
}