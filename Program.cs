// See https://aka.ms/new-console-template for more information
using System.Reflection;
using RetoTecnicoCobol.src.Services;

if (args.Contains("--version") || args.Contains("-v"))
{
    var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "1.0.0";
    Console.WriteLine($"RetoTecnicoCobol CLI v{version}");
    return;
}

Console.WriteLine();
Console.WriteLine("*****************************************");
Console.WriteLine("* Bienvenido a Reporte de Transacciones *");
Console.WriteLine("*****************************************");

TransactionReport transactionReport = new();
transactionReport.Initialize();