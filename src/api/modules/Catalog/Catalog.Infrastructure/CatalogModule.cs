using Carter;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.SomeEvents;
using FSH.Starter.WebApi.Catalog.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Infrastructure;
public static class CatalogModule
{
    public class Endpoints : CarterModule
    {
        public Endpoints() : base("catalog") { }
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var productGroup = app.MapGroup("products").WithTags("products");
            productGroup.MapProductCreationEndpoint();
            productGroup.MapGetProductEndpoint();
            productGroup.MapGetProductListEndpoint();
            productGroup.MapProductUpdateEndpoint();
            productGroup.MapProductDeleteEndpoint();

            var brandGroup = app.MapGroup("brands").WithTags("brands");
            brandGroup.MapBrandCreationEndpoint();
            brandGroup.MapGetBrandEndpoint();
            brandGroup.MapGetBrandListEndpoint();
            brandGroup.MapBrandUpdateEndpoint();
            brandGroup.MapBrandDeleteEndpoint();

            var eventCatalogGroup = app.MapGroup("eventcatalogs").WithTags("eventcatalogs");
            eventCatalogGroup.MapEventCatalogCreationEndpoint();
            eventCatalogGroup.MapGetEventCatalogEndpoint();
            eventCatalogGroup.MapGetEventCatalogListEndpoint();
            eventCatalogGroup.MapEventCatalogUpdateEndpoint();
            eventCatalogGroup.MapEventCatalogDeleteEndpoint();
            eventCatalogGroup.MapGetAllEventCatalogsEndpoint();

            var someEventGroup = app.MapGroup("someevents").WithTags("someevents");
            someEventGroup.MapSomeEventCreationEndpoint();
            someEventGroup.MapGetSomeEventEndpoint();
            someEventGroup.MapGetSomeEventListEndpoint();
            someEventGroup.MapSomeEventUpdateEndpoint();
            someEventGroup.MapSomeEventDeleteEndpoint();
        }
    }
    public static WebApplicationBuilder RegisterCatalogServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<CatalogDbContext>();
        builder.Services.AddScoped<IDbInitializer, CatalogDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<Product>, CatalogRepository<Product>>("catalog:products");
        builder.Services.AddKeyedScoped<IReadRepository<Product>, CatalogRepository<Product>>("catalog:products");
        builder.Services.AddKeyedScoped<IRepository<Brand>, CatalogRepository<Brand>>("catalog:brands");
        builder.Services.AddKeyedScoped<IReadRepository<Brand>, CatalogRepository<Brand>>("catalog:brands");
        builder.Services.AddKeyedScoped<IRepository<EventCatalog>, CatalogRepository<EventCatalog>>("catalog:eventcatalogs");
        builder.Services.AddKeyedScoped<IReadRepository<EventCatalog>, CatalogRepository<EventCatalog>>("catalog:eventcatalogs");
        builder.Services.AddKeyedScoped<IRepository<SomeEvent>, CatalogRepository<SomeEvent>>("catalog:someevents");
        builder.Services.AddKeyedScoped<IReadRepository<SomeEvent>, CatalogRepository<SomeEvent>>("catalog:someevents");
        return builder;
    }
    public static WebApplication UseCatalogModule(this WebApplication app)
    {
        return app;
    }
}
