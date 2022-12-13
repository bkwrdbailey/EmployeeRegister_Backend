using Microsoft.AspNetCore.Mvc;
using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.Services;

namespace EmployeeRegisterDB.Controllers;

[ApiController]
public class EmailController
{
    private readonly IEmailService _service;
    public EmailController(IEmailService service)
    {
        _service = service;
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

    [HttpGet("/email/test")]
    public async Task<bool> emailTest()
    {
        return await _service.sendEmailTest();
    }
}