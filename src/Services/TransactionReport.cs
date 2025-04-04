using System;
using RetoTecnicoCobol.src.Models;

namespace RetoTecnicoCobol.src.Services;

public class TransactionReport
{
    private readonly List<TransactionModel> _transactions;
    private readonly List<string> _types = ["Crédito", "Débito"];
    private readonly IEnumerable<IGrouping<string, TransactionModel>> _transactionByType;

    public TransactionReport(List<TransactionModel> transactions)
    {
        _transactions = [.. transactions.Where(t => _types.Any(type => type == t.Type))];
        _transactionByType = transactions.GroupBy(t => t.Type);
    }

    public decimal FinalBalance => FinalBalanceCalulate();
    public TransactionModel? HigherAmountTransaction => _transactions.MaxBy(t => t.Amount);
    public int CreditTransactionCount => CountTransactionByType(_types[0]);
    public int DebitTransactionCount => CountTransactionByType(_types[1]);

    private Dictionary<string, int> TransactionCounting =>
        _transactionByType.ToDictionary(g => g.Key, g => g.Count());
    private int CountTransactionByType(string type) =>
        TransactionCounting.First(t => t.Key.Equals(type)).Value;

    private decimal FinalBalanceCalulate()
    {
        decimal debit = 0;
        decimal credit = 0;

        foreach (var group in _transactionByType)
        {
            if (group.Key == _types[0]) credit = group.Sum(t => t.Amount);

            debit = group.Sum(t => t.Amount);
        }

        return credit - debit;
    }
}
