﻿using Abp.Application.Services.Dto;

namespace SimbioMed.DynamicEntityProperties.Dto
{
    public class DynamicEntityPropertyValueDto : EntityDto
    {
        public string Value { get; set; }

        public string EntityId { get; set; }

        public int DynamicEntityPropertyId { get; set; }
    }
}
