namespace CartingService.Abstractions.Interfaces;

public interface IDocumentEntity
{
    string PrimaryId { get; set; }
    string RawId { get; set; }
}
