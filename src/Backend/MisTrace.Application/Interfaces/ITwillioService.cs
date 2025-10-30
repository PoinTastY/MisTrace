using MisTrace.Application.DTOs.Notify;

namespace MisTrace.Application.Interfaces;

public interface ITwillioService
{
    void SendWhatsAppMessage(WhatsAppMessageRequest request);
}
