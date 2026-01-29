using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.Starter.WebApi.Catalog.Application.SomeEvents.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using Mapster;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.MapsterConfigs;
public class SomeEventMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SomeEvent, SomeEventResponse>()
            .Map(dest => dest.EventCatalogName, src => src.EventCatalog!=null ? src.EventCatalog.Name : "");
    }
}
