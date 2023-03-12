# Movies

A web API project which can be used to perform CRUD operations on movie objects. Also supports CRUD
operations for rating movies.

## Class Diagram

Find a draw.io class diagram file in the `Assests` folder of this project.

## File Structure

- Movies.Api: Web project which contains all the API layer logic, including endpoints, Swagger UI and
contracts.
- Movies.Application: Class library which contains all the businesslayer logic and the data access layer
logic. Includes all the service and repositorys used by this project.
- Movies.Api.Test.Unit: Test project for the Movies.Api project.

## Evaluation of the role of object-oriented programming

OOP features used include inheritance, abstraction and the use of objects and classes.

I made use of inheritance when creating the domain models and the API contracts. These objects shared
properties which were used in both the request and response contracts as well as the domain models.
For example, all movie classes made use of a `title`, `description` and a `releaseDate`. Although
these base classes could be used to represent a movie they had no way of being tracked, therefore they
were made `abstract` so they could not be instantiated. The `Movie` class which inherited this base model
included an `Id` as well as tracking properties.

By laying out the models like this, it added the following benefits:
- Helped isolate server set properties from user defined properties.
- Reduced the amount of repeated code.
- When adding or updating user defined properties, the changes only needed to be made in a single location.

## Design Patterns

### Vertical Slice

The vertical slice pattern works by focusing on features rather than layers. A feature of this project is
the ability to perform CRUD operations on movies.

Following the vertical slice pattern, each feature gets its own controller, service and repository. This
helps features decouple from each other allowing the focus of each layer to be primarily the features it
relates to.

Benefits of using the vertical slice pattern:
- Improves code clarity and maintainability.
- Each layer follows the 'S' in SOLID (Single-responsibility principle).
- Reduction in coupling of features.

### Repository Pattern

The repository pattern works by creating an abstraction between the data access layer and the business
logic layer.

In the case of this project, only the repository has access to the `MoviesApiContext`. The repository is
then injected into the required services via dependency injection. This allows for the repository to decide
how consumers i.e the business logic layer, use the database. It can also limit the actions which can be
performed on the database; for example, the consumer cannot make a request to delete all items in a table
unless the repository wants to allow that type of operation.

Benefits of using the repository pattern:
- Helps abstract the data access layer logic from the business logic layer.
- Allows for better control of the database.
- The isolation of the business logic layer and data access layer makes it easier to unit test each
layer individually.

### API Contracts

API contracts are a set of models agreed upon by you, the API provider, and the consumer of the API. They
often dictate how a user can interact with an API, letting them know what data they can provide and what
data they can expect back from the API.

By isolating the domain model from the contracts allows for developers to change the domain model without
having to worry about breaking the consumer’s code. The consumers only interact with the contracts so 
additional properties and logic can be added to the domain model will have no effect on the consumer. If
the contracts were to change then a new major version of the API would have to be released as the changes
would most likely break the consumer’s code.

API contracts also give greater control over what data can be received and sent to a user. If a property is
only used internally i.e an Entity Framework relationship, it can simple not be added to the list of
available properties in a response contract.

Benefits of API contracts:
- Isolates the domain model from the agreed contracts.
- Can help detect breaking changes if the contents of the `Contracts` folder is modified.
- Only exposes the desired properties to the consumer.

## Discussion of effectiveness

> Did it simplify the development process?

Yes. By creating abstractions to be inherited, features which were share between models could be easily
added. The abstractions also helped when code changes needed to be made, as the code only needed to be
changed in one place.

> Did it provide any benefits?

Yes. By abstracting services and repositories into interfaces they could made the code easier to maintain.
Any logic-based changes made to the implementation had no effect on the interface and the consumer of that
interface.

> Any limitations?

Due to the nature of this project, there weren’t many opportunities to use all the features of OO.

