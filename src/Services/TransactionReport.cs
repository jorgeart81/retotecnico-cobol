using System;
using RetoTecnicoCobol.src.Models;
using RetoTecnicoCobol.src.Utils;

namespace RetoTecnicoCobol.src.Services;

public class TransactionReport
{
    private List<TransactionModel> Transactions { get; set; } = [];
    private bool DataExists { get; set; } = false;
    private bool WantContinue { get; set; } = true;
    private int FailureCount { get; set; } = 0;

    public void Initialize()
    {
        do
        {
            while (!DataExists)
            {
                var fileLocation = FileLocationValidation();

                if (!string.IsNullOrEmpty(fileLocation))
                {
                    Transactions = ProcessCsv.LoadFile(fileLocation);

                    if (Transactions.Count > 0) DataExists = true;
                    else Console.WriteLine("El archivo no fue procesado.");
                }
                else
                {
                    RetryMenu();
                }
            }

            if (Transactions.Count > 0)
            {
                TransactionCalculation report = new(Transactions);
                PrintReport(report);
                ShowGenerateNewReportMenu();
            }
        } while (WantContinue);
    }

    private void PrintReport(TransactionCalculation report)
    {
        Console.WriteLine();
        Console.WriteLine("Reporte de Transacciones");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Balance Final: {report.FinalBalance:F2}");
        Console.WriteLine($"Transacción de Mayor Monto: ID {report?.HigherAmountTransaction?.Id} - {report?.HigherAmountTransaction?.Amount:F2}");
        Console.WriteLine($"Conteo de Transacciones: Crédito: {report?.CreditTransactionCount} Débito: {report?.DebitTransactionCount}");
    }

    private void ShowGenerateNewReportMenu()
    {
        Console.WriteLine();
        Console.WriteLine("\n ¿Generar un nuevo reporte?");
        Console.WriteLine("\nPresione la tecla: (C) para Continuar  / (T) para Terminar");
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.C:
                FailureCount = 0;
                DataExists = false;
                Transactions = [];
                break;

            case ConsoleKey.T:
                Console.WriteLine();
                Console.WriteLine("El programa fue finalizado.");
                WantContinue = false;
                break;
        }
    }

    private string? FileLocationValidation()
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

    private bool IsCsvFile(string filePath)
    {
        return Path.GetExtension(filePath).Equals(".csv", StringComparison.CurrentCultureIgnoreCase);
    }

    private void RetryMenu()
    {
        FailureCount++;
        while (FailureCount >= 3)
        {
            Console.WriteLine("\nTiene 3 intentos fallidos ¿Quiere seguir intentado?");
            Console.WriteLine("\nPresione la tecla: (S) para Si / (N) para No");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.S:
                    FailureCount = 0;
                    break;

                case ConsoleKey.N:
                    Console.WriteLine();
                    Console.WriteLine("El programa fue finalizado.");
                    FailureCount = 0;
                    DataExists = true;
                    WantContinue = false;
                    break;
            }
        }
    }
}
