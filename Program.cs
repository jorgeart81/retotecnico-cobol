// See https://aka.ms/new-console-template for more information
using RetoTecnicoCobol.src.Models;
using RetoTecnicoCobol.src.Services;
using RetoTecnicoCobol.src.Utils;

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

        if (string.IsNullOrEmpty(fileLocation))
        {
            RetryMenu();
        }
        else
        {
            transactions = ProcessCsv.LoadFile(fileLocation);

            if (transactions.Count > 0) dataExists = true;
            else Console.WriteLine("El archivo no fue procesado.");
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
                Console.Clear();
                Console.WriteLine("El programa fue finalizado.");
                wantContinue = false;
                break;
        }
    }

} while (wantContinue);


static string? FileLocationValidation()
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine("Por favor ingrese la ruta del archivo:");
    var fileLocation = $@"{Console.ReadLine()}";

    if (!File.Exists(fileLocation))
    {
        Console.Clear();
        Console.WriteLine("Solicitud incorrecta. El archivo no fue encontrado.");
        return null;
    }

    return fileLocation;
}

void RetryMenu()
{
    failureCount++;
    while (failureCount > 3)
    {
        Console.Clear();
        Console.WriteLine("\n ¿Quiere seguir intentado?");
        Console.WriteLine("\nPresione la tecla: (S) para Si / (N) para No");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.Y:
                failureCount = 0;
                break;

            case ConsoleKey.N:
                Console.Clear();
                Console.WriteLine("El programa fue finalizado.");
                failureCount = 0;
                dataExists = true;
                break;
        }
    }
}