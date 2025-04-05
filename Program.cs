// See https://aka.ms/new-console-template for more information
using System.Reflection;
using RetoTecnicoCobol.src.Models;
using RetoTecnicoCobol.src.Services;
using RetoTecnicoCobol.src.Utils;

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

List<TransactionModel> transactions = [];
bool dataExists = false;
bool wantContinue = true;
int failureCount = 0;

do
{
    while (!dataExists)
    {
        var fileLocation = FileLocationValidation();

        if (!string.IsNullOrEmpty(fileLocation))
        {
            transactions = ProcessCsv.LoadFile(fileLocation);

            if (transactions.Count > 0) dataExists = true;
            else Console.WriteLine("El archivo no fue procesado.");
        }
        else
        {
            RetryMenu();
        }
    }

    if (transactions.Count > 0)
    {
        TransactionReport report = new(transactions);

        Console.WriteLine();
        Console.WriteLine("Reporte de Transacciones");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Balance Final: {report.FinalBalance:F2}");
        Console.WriteLine($"Transacción de Mayor Monto: ID {report?.HigherAmountTransaction?.Id} - {report?.HigherAmountTransaction?.Amount:F2}");
        Console.WriteLine($"Conteo de Transacciones: Crédito: {report?.CreditTransactionCount} Débito: {report?.DebitTransactionCount}");

        Console.WriteLine();
        Console.WriteLine("\n ¿Generar un nuevo reporte?");
        Console.WriteLine("\nPresione la tecla: (C) para Continuar  / (T) para Terminar");
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.C:
                failureCount = 0;
                dataExists = false;
                transactions = [];
                break;

            case ConsoleKey.T:
                Console.WriteLine();
                Console.WriteLine("El programa fue finalizado.");
                wantContinue = false;
                break;
        }
    }
} while (wantContinue);


string? FileLocationValidation()
{
    Console.WriteLine();
    Console.WriteLine("Por favor ingrese la ruta del archivo:");
    var fileLocation = $@"{Console.ReadLine()}";

    string? message = true switch
    {
        _ when string.IsNullOrWhiteSpace(fileLocation) => "Estructura inválida",
        _ when !IsCsvFile(fileLocation) => "El archivo no es un CSV válido",
        _ when !File.Exists(fileLocation) => "El archivo no existe",
        _ => null
    };

    if (!string.IsNullOrEmpty(message))
    {
        ConsoleHelper.WriteError(message);
        return null;
    }

    return fileLocation;
}

bool IsCsvFile(string filePath)
{
    return Path.GetExtension(filePath).Equals(".csv", StringComparison.CurrentCultureIgnoreCase);
}

void RetryMenu()
{
    failureCount++;
    while (failureCount >= 3)
    {
        Console.WriteLine("\nTiene 3 intentos fallidos ¿Quiere seguir intentado?");
        Console.WriteLine("\nPresione la tecla: (S) para Si / (N) para No");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.S:
                failureCount = 0;
                break;

            case ConsoleKey.N:
                Console.WriteLine();
                Console.WriteLine("El programa fue finalizado.");
                failureCount = 0;
                dataExists = true;
                wantContinue = false;
                break;
        }
    }
}