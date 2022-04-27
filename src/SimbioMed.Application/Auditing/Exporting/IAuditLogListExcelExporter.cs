using System.Collections.Generic;
using SimbioMed.Auditing.Dto;
using SimbioMed.Dto;

namespace SimbioMed.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
