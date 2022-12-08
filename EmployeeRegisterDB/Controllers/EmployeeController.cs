using Microsoft.AspNetCore.Mvc;
using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.Services;

namespace EmployeeRegisterDB.Controllers;

[ApiController]
public class EmployeeController
{
    private readonly IEmailService _emailService;
    private readonly IDataHandlingService _dataHandlingService;
    public EmployeeController(IEmailService emailService, IDataHandlingService dataHandlingService)
    {
        _emailService = emailService;
        _dataHandlingService = dataHandlingService;
    }

    [HttpGet("/verify/{managerId}")]
    public async Task<bool> verifyManagerId(int managerId)
    {
        return await _dataHandlingService.checkManagerId(managerId);
    }

    [HttpGet("/check/{employeeId}")]
    public async Task<string> checkEmployeeId(int employeeId)
    {
        return await _dataHandlingService.getEmployeeName(employeeId);
    }

    [HttpPost("/new/employee")]
    public async Task<bool> addNewEmployee([FromBody] Employee newEmployee)
    {
        return await _dataHandlingService.addNewEmployeeRecord(newEmployee);
    }

    // [HttpPost("/email/manager")]
    // public async Task<bool> emailEmployeeReport([FromBody] EmployeeTabularData[] employeeReports)
    // {
    //     if (await _dataHandlingService.addNewAttendanceRecords(employeeReports))
    //     {
    //         return await _emailService.sendEmailReport(employeeReports);
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }
}