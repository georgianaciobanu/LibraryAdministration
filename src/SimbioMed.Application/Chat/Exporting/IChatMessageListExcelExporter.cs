using System.Collections.Generic;
using Abp;
using SimbioMed.Chat.Dto;
using SimbioMed.Dto;

namespace SimbioMed.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
