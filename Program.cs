// See https://aka.ms/new-console-template for more information
using RetoTecnicoCobol.src.Utils;

string relativePath = "CsvFiles/transactions.csv";

string fileLocation = @"/Users/jorge/Documents/Developer/1_Owner/C#/RetoTecnicoCobol/CsvFiles/transactions.csv";
var transactions = ProcessCsv.LoadFile($@"{relativePath}");

foreach (var transaction in transactions)
{
    Console.WriteLine("id,tipo,monto");
    Console.WriteLine($"{transaction.Id},{transaction.Type},{transaction.Amount}");
}
