namespace DebtTracker.Services.Contracts
{
    public interface IFileProcessor
    {
        Task<Dictionary<string, List<string>>> ProcessDebts(byte[] debtFile);
    }
}
