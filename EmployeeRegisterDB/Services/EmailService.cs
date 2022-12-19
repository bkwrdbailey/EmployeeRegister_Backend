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

    public async Task<bool> sendEmailReport(EmployeeTabularData[] employeeReport)
    {
        // Acquires manager email
        Manager checkManager = await _db.getManagerRecordById(employeeReport[0].managerId);

        // Email employee records to manager email

        MimeMessage message = new MimeMessage();

        message.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.EmailSender));

        message.To.Add(MailboxAddress.Parse(checkManager.email));

        message.Subject = "Employee Attendance";

        await createEmployeeReportFile(employeeReport);

        var attachment = new MimePart("image", "gif")
        {
            Content = new MimeContent(File.OpenRead("AttachmentFile.txt"), ContentEncoding.Default),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = Path.GetFileName("AttachmentFile.txt")
        };

        var multipart = new Multipart("mixed");
        multipart.Add(new TextPart("plain") { Text = "Daily employee attendance report attached" });
        multipart.Add(attachment);

        message.Body = multipart;

        SmtpClient client = new SmtpClient();

        try
        {
            Console.WriteLine(message);
            client.Connect(_settings.EmailHost, _settings.Port, SecureSocketOptions.StartTls);
            client.Authenticate(_settings.Username, _settings.Password);
            client.Send(message);

            // Return true boolean value if email was sent successfully
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

    private async Task createEmployeeReportFile(EmployeeTabularData[] employeeReport)
    {
        try
        {
            string text = "";
            foreach (EmployeeTabularData employee in employeeReport)
            {
                text += $"{employee.empId} {employee.name} {employee.date} {employee.attendanceCode} {employee.leaveType} \n";
            }
            await File.WriteAllTextAsync("AttachmentFile.txt", text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception found: {ex}");
        }
    }
}