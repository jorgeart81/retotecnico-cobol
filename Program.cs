// See https://aka.ms/new-console-template for more information
using RetoTecnicoCobol.src.Models;
using RetoTecnicoCobol.src.Services;
using RetoTecnicoCobol.src.Utils;

string relativePath = "CsvFiles/transactions.csv";
// string fileLocation = @"/Users/jorge/Documents/Developer/1_Owner/C#/RetoTecnicoCobol/CsvFiles/transactions.csv";
// string fileLocation = @"C:\Users\AresR\Documents\Developer\1_Owner\5_Others\retotecnico-cobol\CsvFiles\transactions.csv";

List<TransactionModel> transactions = ProcessCsv.LoadFile(relativePath);
TransactionReport report = new(transactions);

Console.WriteLine("Reporte de Transacciones");
Console.WriteLine("--------------------------------------------");
Console.WriteLine($"Balance Final: {report.FinalBalance:F2}");
Console.WriteLine($"Transacción de Mayor Monto: ID {report?.HigherAmountTransaction?.Id} - {report?.HigherAmountTransaction?.Amount:F2}");
Console.WriteLine($"Conteo de Transacciones: Crédito: {report?.CreditTransactionCount} Débito: {report?.DebitTransactionCount}");
