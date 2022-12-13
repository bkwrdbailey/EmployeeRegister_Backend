using Microsoft.AspNetCore.Mvc;
using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.Services;

namespace EmployeeRegisterDB.Controllers;

[ApiController]
public class EmployeeController
{
    private readonly IDataHandlingService _dataHandlingService;
    public EmployeeController(IDataHandlingService dataHandlingService)
    {
        _dataHandlingService = dataHandlingService;
    }

    [HttpGet("/verify/{managerId}")]
    public async Task<bool> verifyManagerId(int managerId)
    {
        return await _dataHandlingService.checkManagerId(managerId);
    }

    [HttpGet("/new/employee/verify/manager/{managerId}/{managerName}")]
    public async Task<bool> verifyManagerData(int managerId, string managerName)
    {
        return await _dataHandlingService.checkManagerData(managerId, managerName.ToLower());
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
}