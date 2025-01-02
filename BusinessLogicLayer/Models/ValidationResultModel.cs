namespace UserProjectBusinessLogicLayer.Models;
public sealed class ValidationResultModel
{
    public bool IsValid { get; set; } = true;
    public List<string> Errors { get; set; } = new List<string>();
}
