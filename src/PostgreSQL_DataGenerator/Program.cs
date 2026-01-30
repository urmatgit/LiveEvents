using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Определяем фиксированные OrganizationId
            var organizationIds = new List<Guid>()
            {
                Guid.Parse("7c6d1e50-e399-4771-a6e3-bf3fe5e554c7"),
                Guid.Parse("8e5f37d0-0e0e-4099-982e-d38a301dc5f4"),
                Guid.Parse("a17a024d-2979-4033-9daa-8683afb312dd")
            };

            // Генерируем 100 EventCatalog
            var eventCatalogs = GenerateEventCatalogs(100);
            
            // Сохраняем EventCatalogs в файл
            SaveEventCatalogsToFile(eventCatalogs, "PostgreSQL_EventCatalogs.sql");
            
            // Генерируем 2000 SomeEvent с рандомными OrganizationId и EventCatalogId из сгенерированных выше
            var someEvents = GenerateSomeEvents(2000, eventCatalogs, organizationIds);
            
            // Сохраняем SomeEvents в файл
            SaveSomeEventsToFile(someEvents, "PostgreSQL_SomeEvents.sql");

            // Также создаем объединенный файл для PostgreSQL
            SaveCombinedPostgreSQLScript(eventCatalogs, someEvents, "PostgreSQL_GeneratedData.sql");

            Console.WriteLine($"Сгенерировано {eventCatalogs.Count} EventCatalog и {someEvents.Count} SomeEvent для PostgreSQL");
        }

        static List<EventCatalog> GenerateEventCatalogs(int count)
        {
            var catalogs = new List<EventCatalog>();
            var random = new Random();
            
            for (int i = 0; i < count; i++)
            {
                var name = $"EventCatalog {i + 1}";
                var description = $"Описание для EventCatalog {i + 1}. Это тестовое описание для проверки работы системы.";
                
                var catalog = new EventCatalog
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
                catalogs.Add(catalog);
            }
            
            return catalogs;
        }

        static List<SomeEvent> GenerateSomeEvents(int count, List<EventCatalog> eventCatalogs, List<Guid> organizationIds)
        {
            var events = new List<SomeEvent>();
            var random = new Random();
            
            for (int i = 0; i < count; i++)
            {
                // Выбираем случайный EventCatalogId из существующих
                var eventCatalogIndex = random.Next(0, eventCatalogs.Count);
                var eventCatalogId = eventCatalogs[eventCatalogIndex].Id;
                
                // Выбираем случайный OrganizationId
                var orgIndex = random.Next(0, organizationIds.Count);
                var organizationId = organizationIds[orgIndex];
                
                // Генерируем остальные поля
                var name = $"SomeEvent {i + 1}";
                var description = $"Описание для SomeEvent {i + 1}. Это тестовое событие для проверки функциональности.";
                var minParticipants = random.Next(1, 10);
                var maxParticipants = random.Next(minParticipants, 100); // max >= min
                var duration = random.Next(30, 240); // продолжительность от 30 до 240 минут
                var price = (decimal)Math.Round(random.NextDouble() * 10000, 2); // цена до 10000 с 2 знаками после запятой
                var eventDate = DateTime.Now.AddDays(random.Next(1, 365)); // дата события в будущем
                
                var someEvent = new SomeEvent
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    OrganizationId = organizationId,
                    MinParticipant = minParticipants,
                    MaxParticipant = maxParticipants,
                    Durations = duration,
                    Price = price,
                    EventDate = eventDate,
                    EventCatalogId = eventCatalogId
                };
                
                events.Add(someEvent);
            }
            
            return events;
        }

        static void SaveEventCatalogsToFile(List<EventCatalog> catalogs, string fileName)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.WriteLine("-- SQL script to insert EventCatalog records (PostgreSQL version)");
                writer.WriteLine("-- Generated on " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine();
                writer.WriteLine("INSERT INTO \"EventCatalogs\" (\"Id\", \"Name\", \"Description\",\"TenantId\", \"CreatedBy\", \"LastModifiedBy\", \"Created\", \"LastModified\") VALUES ");
                
                for (int i = 0; i < catalogs.Count; i++)
                {
                    var catalog = catalogs[i];
                    var sql = $"('{catalog.Id}', '{catalog.Name.Replace("'", "''")}', '{catalog.Description?.Replace("'", "''") ?? ""}','{catalog.TenantId.Replace("'", "''")}', '8e5f37d0-0e0e-4099-982e-d38a301dc5f4', '8e5f37d0-0e0e-4099-982e-d38a301dc5f4', NOW(), NOW()){((i == catalogs.Count - 1) ? ";" : ",")}";
                    writer.WriteLine(sql);
                }
            }
        }

        static void SaveSomeEventsToFile(List<SomeEvent> events, string fileName)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.WriteLine("-- SQL script to insert SomeEvent records (PostgreSQL version)");
                writer.WriteLine("-- Generated on " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine();
                writer.WriteLine("INSERT INTO \"SomeEvents\" (\"Id\", \"Name\", \"Description\", \"OrganizationId\", \"MinParticipant\", \"MaxParticipant\", \"Durations\", \"Price\", \"EventDate\", \"EventCatalogId\",\"TenantId\", \"CreatedBy\", \"LastModifiedBy\", \"Created\", \"LastModified\") VALUES ");
                
                for (int i = 0; i < events.Count; i++)
                {
                    var evt = events[i];
                    var sql = $"('{evt.Id}', '{evt.Name.Replace("'", "''")}', '{evt.Description.Replace("'", "''")}', '{evt.OrganizationId}', {evt.MinParticipant}, {evt.MaxParticipant}, {evt.Durations}, {evt.Price}, '{evt.EventDate:yyyy-MM-dd HH:mm:ss}', '{evt.EventCatalogId}','{evt.TenantId.Replace("'", "''")}', '8e5f37d0-0e0e-4099-982e-d38a301dc5f4', '8e5f37d0-0e0e-4099-982e-d38a301dc5f4', NOW(), NOW()){((i == events.Count - 1) ? ";" : ",")}";
                    writer.WriteLine(sql);
                }
            }
        }

        static void SaveCombinedPostgreSQLScript(List<EventCatalog> eventCatalogs, List<SomeEvent> someEvents, string fileName)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.WriteLine("-- SQL script to insert test data for EventCatalog and SomeEvent tables (PostgreSQL version)");
                writer.WriteLine("-- Generated on " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine();

                // Записываем данные EventCatalog
                writer.WriteLine("-- Insert EventCatalog records");
                writer.WriteLine("INSERT INTO \"EventCatalogs\" (\"Id\", \"Name\", \"Description\", \"CreatedBy\", \"LastModifiedBy\", \"Created\", \"LastModified\") VALUES ");
                
                for (int i = 0; i < eventCatalogs.Count; i++)
                {
                    var catalog = eventCatalogs[i];
                    var sql = $"('{catalog.Id}', '{catalog.Name.Replace("'", "''")}', '{catalog.Description?.Replace("'", "''") ?? ""}', '8e5f37d0-0e0e-4099-982e-d38a301dc5f4', '8e5f37d0-0e0e-4099-982e-d38a301dc5f4', NOW(), NOW()){((i == eventCatalogs.Count - 1) ? ";" : ",")}";
                    writer.WriteLine(sql);
                }
                
                writer.WriteLine(); // Пустая строка для разделения
                
                // Записываем данные SomeEvent
                writer.WriteLine("-- Insert SomeEvent records");
                writer.WriteLine("INSERT INTO \"SomeEvents\" (\"Id\", \"Name\", \"Description\", \"OrganizationId\", \"MinParticipant\", \"MaxParticipant\", \"Durations\", \"Price\", \"EventDate\", \"EventCatalogId\", \"CreatedBy\", \"LastModifiedBy\", \"Created\", \"LastModified\") VALUES ");
                
                for (int i = 0; i < someEvents.Count; i++)
                {
                    var evt = someEvents[i];
                    var sql = $"('{evt.Id}', '{evt.Name.Replace("'", "''")}', '{evt.Description.Replace("'", "''")}', '{evt.OrganizationId}', {evt.MinParticipant}, {evt.MaxParticipant}, {evt.Durations}, {evt.Price}, '{evt.EventDate:yyyy-MM-dd HH:mm:ss}', '{evt.EventCatalogId}', 'System', 'System', NOW(), NOW()){((i == someEvents.Count - 1) ? ";" : ",")}";
                    writer.WriteLine(sql);
                }
                
                writer.WriteLine(); // Пустая строка в конце
            }
        }
    }
    
    // Классы данных для имитации доменных объектов
    public class EventCatalog
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string TenantId { get; set; } = "root";
    }
    
    public class SomeEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
        public int MinParticipant { get; set; }
        public int MaxParticipant { get; set; }
        public int Durations { get; set; }
        public decimal Price { get; set; }
        public DateTime EventDate { get; set; }
        public Guid EventCatalogId { get; set; }
        public string TenantId { get; set; } = "root";

    }
}
