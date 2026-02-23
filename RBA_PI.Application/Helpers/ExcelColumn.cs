namespace RBA_PI.Application.Helpers
{
    public class ExcelColumn<T>
    {
        public string Header { get; init; } = string.Empty;
        public Func<T, object?> Value { get; init; } = default!;
        public string? NumberFormat { get; init; }
    }
}
