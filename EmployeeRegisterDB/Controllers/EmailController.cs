using Microsoft.AspNetCore.Mvc;
using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.Services;

namespace EmployeeRegisterDB.Controllers;

[ApiController]
public class EmailController
{
    private readonly IDataHandlingService _dataHandlingService;
    private readonly IEmailService _emailService;
    public EmailController(IEmailService emailService, IDataHandlingService dataHandlingService)
    {
        _emailService = emailService;
        _dataHandlingService = dataHandlingService;
    }

    [HttpPut("/email/manager")]
    public async Task<bool> emailEmployeeReport([FromBody] EmployeeTabularData[] employeeReports)
    {
        await _dataHandlingService.addNewAttendanceRecords(employeeReports);
        return await _emailService.sendEmailReport(employeeReports);
    }
}