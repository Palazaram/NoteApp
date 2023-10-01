## Data Model:

In the `Models` folder, you'll find a `Note` model with the following fields:
- `Id` (unique identifier)
- `Title` (note title)
- `Text` (note text)
- `CreatedAt` (creation date of the note)

## Data Context:

Inside the `Data` folder, there's the `ApplicationDbContext` class, responsible for configuring the connection to the PostgreSQL database and defining the database schema. This class also defines the `Notes` table using Entity Framework Core.

## Model Configuration:

In the `ConfigureClasses` folder, you may find a `NoteConfiguration` class that specifies additional settings for the `Note` entity, such as indexes, constraints, and other aspects of the database schema.

## Controllers:

The `Controllers` folder contains the `NoteController`, which provides an API for interacting with notes. This controller includes methods for retrieving a list of notes, editing a note, and adding a new note.

## Dependencies and Services:

In the `Interfaces` folder, you've defined the `INote` interface, which contains methods for working with notes (such as retrieving a list, adding, and editing).
In the `Services` folder, the `NoteManager` service has been implemented, which implements the `INote` interface and provides the business logic for managing notes.

## Blazor Pages:

In the `Pages` folder, two pages have been created:
- `NoteDetails` - a page designed for displaying a list of notes along with their details.
- `AddNote` - a page featuring a form for adding a new note.

## Validation:

Model validation has been implemented using data annotations (e.g., `[Required]`) for the `Title` and `Text` fields to ensure that these fields are required before adding a note.

## Testing:

Unit and NUnit tests have been developed to validate the functionality of your application. These tests cover services, controllers, and model validation.
