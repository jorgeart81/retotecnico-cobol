// See https://aka.ms/new-console-template for more information
using System.Text;
using RetoTecnicoCobol.src.Models;
using RetoTecnicoCobol.src.Services;
using RetoTecnicoCobol.src.Utils;

Console.WriteLine("Bienvenido a Reporte de Transacciones");

List<TransactionModel> transactions = [];
bool fileExists = false;
int fileExistCount = 0;

while (!fileExists)
{
    Console.WriteLine("");
    Console.WriteLine("Por favor ingrese la ruta del archivo:");
    var fileLocation = $@"{Console.ReadLine()}";

    fileExists = File.Exists(fileLocation);

    if (fileExists) transactions = ProcessCsv.LoadFile(fileLocation);
    Console.WriteLine("Solicitud incorrecta. El archivo no fue encontrado.");

    fileExistCount++;
    while (fileExistCount > 3)
    {
        Console.Clear();
        Console.WriteLine("\nPresione la tecla: (Y) para volver intentar. / (N) para salir. ");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.Y:
                fileExistCount = 0;
                break;

            case ConsoleKey.N:
                fileExistCount = 0;
                fileExists = true;
                break;
        }
    }
}



string relativePath = "CsvFiles/transactions.csv";
// string fileLocation = @"/Users/jorge/Documents/Developer/1_Owner/C#/RetoTecnicoCobol/CsvFiles/transactions.csv";
// string fileLocation = @"C:\Users\AresR\Documents\Developer\1_Owner\5_Others\retotecnico-cobol\CsvFiles\transactions.csv";

if (transactions.Count > 0)
{
    TransactionReport report = new(transactions);

    Console.WriteLine("Reporte de Transacciones");
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine($"Balance Final: {report.FinalBalance:F2}");
    Console.WriteLine($"Transacción de Mayor Monto: ID {report?.HigherAmountTransaction?.Id} - {report?.HigherAmountTransaction?.Amount:F2}");
    Console.WriteLine($"Conteo de Transacciones: Crédito: {report?.CreditTransactionCount} Débito: {report?.DebitTransactionCount}");
}

