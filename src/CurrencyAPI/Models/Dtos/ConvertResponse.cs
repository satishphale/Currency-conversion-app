public class ConvertResponse
{
	public string From { get; set; }
	public string To { get; set; }
	public decimal OriginalAmount { get; set; }
	public decimal ConvertedAmount { get; set; }
	public decimal Rate { get; set; }
}