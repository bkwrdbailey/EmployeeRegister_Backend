using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.DatabaseCalls;

namespace EmployeeRegisterDB.Services;

public class EmailService : IEmailService
{
    private readonly IEmployeeDatabase _db;
    public EmailService(IEmployeeDatabase db)
    {
        _db = db;
    }

    public async Task<bool> sendEmailReport(EmployeeTabularData[] employeeReport)
    {
        // Acquire manager email
        // Email their employee attendance record to them
        // Return true boolean value if all emails were sent successfully
    }
}