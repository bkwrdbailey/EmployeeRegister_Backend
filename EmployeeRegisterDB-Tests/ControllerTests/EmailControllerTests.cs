using Xunit;
using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.Services;
using EmployeeRegisterDB.Controllers;
using MongoDB.Bson;
using Moq;
using System;

namespace EmployeeRegisterDB_Tests.EmployeeControllerTests;

public class EmailControllerTests
{
    Mock<IEmailService> _emailContext = new Mock<IEmailService>();
    Mock<IDataHandlingService> _dataContext = new Mock<IDataHandlingService>();

    [Fact]
    public void TestDependencyInjection()
    {
        EmailController emailController = new EmailController(_emailContext.Object, _dataContext.Object);
        Assert.NotNull(emailController);
    }

    [Fact]
    public async void EmailEmployeeReport_Returns_True_If_Email_Report_Successfully_Was_Sent()
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

        _dataContext.Setup(test => test.addNewAttendanceRecords(listOfEmployeeAttendances)).ReturnsAsync(true);
        _emailContext.Setup(test => test.sendEmailReport(listOfEmployeeAttendances)).ReturnsAsync(true);

        EmailController emailController = new EmailController(_emailContext.Object, _dataContext.Object);
        var result = await emailController.emailEmployeeReport(listOfEmployeeAttendances);

        Assert.Equal(true, result);
    }
}