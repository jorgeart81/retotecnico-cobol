using System;
using RetoTecnicoCobol.src.Models;

namespace RetoTecnicoCobol.src.Utils;

public static class ProcessCsv
{
    private const string _separator = ",";

    public static List<TransactionModel> LoadFile(string fileLocation)
    {
        List<TransactionModel> transactions = [];
        StreamReader file = new(fileLocation);

        string[]? header = file?.ReadLine()?.Split(_separator);
        int? columns = header?.Length;

        while (file?.ReadLine() is string line)
        {
            string[] row = line.Split(_separator);
            if (columns is not null && row.Length == columns)
            {
                var transaction = new TransactionModel
                {
                    Id = int.Parse(row[0]),
                    Type = row[1],
                    Amount = decimal.Parse(row[2])
                };
                transactions.Add(transaction);
            }
        }

        return transactions;
    }
}
