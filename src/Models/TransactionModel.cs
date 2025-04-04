using System;

namespace RetoTecnicoCobol.src.Models;

public class TransactionModel
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public decimal Amount { get; set; }
}
