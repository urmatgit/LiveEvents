# EventImage Module

This module handles event image management functionality.

## Features

- Create event images associated with SomeEvent entities
- Retrieve individual event images by ID

## Domain Model

### EventImage Entity

```csharp
public class EventImage : AuditableEntity, IAggregateRoot
{
    public Uri? ImageUrl { get; private set; }
    public Guid SomeEventId { get; private set; }
    public virtual SomeEvent? SomeEvent { get; private set; }
}
```

### Relationships

- EventImage has a many-to-one relationship with SomeEvent
- SomeEvent has a one-to-many relationship with EventImage via `ICollection<EventImage> EventImages`

## Endpoints

### POST /catalog/eventimages

Creates a new event image.

Request body:
```json
{
  "imageUrl": "https://example.com/image.jpg",
  "someEventId": "guid-of-some-event"
}
```

### GET /catalog/eventimages/{id}

Retrieves an event image by its ID.

### GET /catalog/eventimages

Retrieves all event images, optionally filtered by SomeEventId.

Query parameters:
- `someEventId` (optional): Filter images by associated SomeEvent

### DELETE /catalog/eventimages/{id}

Deletes an event image by its ID.

### DELETE /catalog/eventimages/by-someevent/{someEventId}

Deletes all event images associated with a specific SomeEvent.

## Implementation Details

- Implements the CQRS pattern with commands, queries, and handlers
- Uses Entity Framework Core for data persistence
- Includes validation through FluentValidation
- Follows the same architectural patterns as other modules in the system