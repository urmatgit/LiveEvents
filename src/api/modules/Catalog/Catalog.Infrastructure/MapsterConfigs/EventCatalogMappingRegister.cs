using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.Starter.WebApi.Catalog.Application.EventCatalogs.Get.v1;
using FSH.Starter.WebApi.Catalog.Domain;
using Mapster;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.MapsterConfigs;
public class EventCatalogMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<EventCatalog, EventCatalogResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl);
    }
}
