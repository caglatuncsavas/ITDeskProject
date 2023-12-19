using System.Globalization;

namespace ITDeskServer.DTOs;

public class TicketDetailDto
{
    public Guid TicketId { get; set; }
    public string Content { get; set; } = String.Empty;
    public Guid AppUserId { get; set; } //Karşı taraftan alınacak
}
