namespace Consumer.Models;
public class MessageModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}