using Xunit;
using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.Services;
using EmployeeRegisterDB.Controllers;
using Moq;
using System;

namespace EmployeeRegisterDB_Tests.EmployeeControllerTests;

public class EmployeeControllerTests
{

    Mock<IDataHandlingService> _context = new Mock<IDataHandlingService>();

    [Fact]
    public void TestDependencyInjection()
    {
        EmployeeController empController = new EmployeeController(_context.Object);
        Assert.NotNull(empController);
    }

    [Fact]
    public async void VerifyManagerId_Returns_True_If_Manager_Id_Is_Valid()
    {
        _context.Setup(test => test.checkManagerId(1521)).ReturnsAsync(true);

        EmployeeController empController = new EmployeeController(_context.Object);
        var result = await empController.verifyManagerId(1521);

        Assert.Equal(true, result);
    }

    [Fact]
    public async void VerifyManagerData_Returns_True_If_Manager_Data_Exists_In_Database()
    {
        _context.Setup(test => test.checkManagerData(1521, "bob thorton")).ReturnsAsync(true);

        EmployeeController empController = new EmployeeController(_context.Object);
        var result = await empController.verifyManagerData(1521, "bob thorton");

        Assert.Equal(true, result);
    }

    [Fact]
    public async void CheckEmployeeId_Returns_Employee_Record_Based_On_Id()
    {
        Employee mockEmployee = new Employee()
        {
            empId = 1111,
            name = "test man",
            designation = "cashier",
            department = "frontend",
            managerId = 1521,
            managerName = "bob thorton",
            startingDate = new DateTime()
        };

        _context.Setup(test => test.getEmployeeName(1111)).ReturnsAsync(mockEmployee);

        EmployeeController empController = new EmployeeController(_context.Object);
        var result = await empController.checkEmployeeId(1111);

        Assert.Equal(mockEmployee.name, result.name);
    }

    [Fact]
    public async void AddNewEmployee_Returns_True_If_Employee_Was_Successfully_Added()
    {
        Employee mockEmployee = new Employee()
        {
            empId = 1111,
            name = "test man",
            designation = "cashier",
            department = "frontend",
            managerId = 1521,
            managerName = "bob thorton",
            startingDate = new DateTime()
        };

        _context.Setup(test => test.addNewEmployeeRecord(mockEmployee)).ReturnsAsync(true);

        EmployeeController empController = new EmployeeController(_context.Object);
        var result = await empController.addNewEmployee(mockEmployee);

        Assert.Equal(true, result);
    }

}