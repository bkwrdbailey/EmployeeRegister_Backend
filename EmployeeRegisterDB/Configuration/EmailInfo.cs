using System.Diagnostics.CodeAnalysis;

namespace EmployeeRegisterDB.Configuration;

[ExcludeFromCodeCoverage]
public class EmailInfo
{
public string? Username { get; set; }
public string? Password { get; set; }
public string? EmailDisplayName { get; set; }
public int Port { get; set; }
public string? EmailSender { get; set; }
public string? EmailHost { get; set; }
}