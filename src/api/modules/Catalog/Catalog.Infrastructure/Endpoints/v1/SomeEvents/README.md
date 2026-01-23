# SomeEvent Endpoints

This directory contains the API endpoints for the SomeEvent entity.

## Available Endpoints

### POST /api/v1/someevents
Creates a new SomeEvent.

### GET /api/v1/someevents/{id}
Retrieves a SomeEvent by its unique identifier.

### GET /api/v1/someevents
Retrieves a list of SomeEvents with optional filtering and pagination.

### PUT /api/v1/someevents/{id}
Updates an existing SomeEvent.

### DELETE /api/v1/someevents/{id}
Deletes a SomeEvent by its unique identifier.

## Permissions

Each endpoint requires specific permissions:
- `Permissions.SomeEvents.Create` - for creation
- `Permissions.SomeEvents.View` - for individual retrieval
- `Permissions.SomeEvents.Search` - for list retrieval
- `Permissions.SomeEvents.Update` - for updates
- `Permissions.SomeEvents.Delete` - for deletion