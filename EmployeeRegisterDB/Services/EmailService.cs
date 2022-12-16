using EmployeeRegisterDB.Models;
using EmployeeRegisterDB.Configuration;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using EmployeeRegisterDB.DatabaseCalls;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace EmployeeRegisterDB.Services;

public class EmailService : IEmailService
{
    private readonly EmailInfo _settings;
    private readonly IEmployeeDatabase _db;
    public EmailService(IEmployeeDatabase db, IOptions<EmailInfo> settings)
    {
        _settings = settings.Value;
        _db = db;
    }

    // public async Task<bool> sendEmailReport(EmployeeTabularData[] employeeReport)
    // {
    //     // Acquire manager email
    //     // Email their employee attendance record to them
    //     // Return true boolean value if all emails were sent successfully
    // }

    public async Task<bool> sendEmailTest()
    {
        // baileybartontest@gmail.com for testing email retrieval

        MimeMessage message = new MimeMessage();

        message.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.EmailSender));

        message.To.Add(MailboxAddress.Parse("baileybartontest@gmail.com"));

        message.Subject = "Employee Attendance";

        message.Body = new TextPart("plain")
        {
            Text = @"Yes,
            Hello!.
            This is dog!."
        };

        SmtpClient client = new SmtpClient();

        try
        {
            Console.WriteLine(message);
            client.Connect(_settings.EmailHost, _settings.Port, SecureSocketOptions.StartTls);
            client.Authenticate(_settings.Username, _settings.Password);
            client.Send(message);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception found: {ex}");
            return false;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}