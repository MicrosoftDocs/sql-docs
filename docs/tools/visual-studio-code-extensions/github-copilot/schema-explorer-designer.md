---
title: Schema Explorer & Designer with GitHub Copilot
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how GitHub Copilot can help design, understand, and evolve database schemas with context-aware suggestions in Visual Studio Code.
author: croblesm
ms.author: roblescarlos
ms.reviewer: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: quickstart
ms.collection:
  - data-tools
  - ce-skilling-ai-copilot
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---

# Quickstart: Use the schema explorer and designer

In this quickstart, you learn how GitHub Copilot assists developers in designing, understanding, and evolving database schemas with context-aware suggestions. Whether you're building from scratch or reverse-engineering existing tables, GitHub Copilot streamlines the process across SQL and object-relational mapping (ORM) frameworks, making schema work faster, smarter, and easier to maintain.

This section covers both creating new schemas from scratch and working with existing databases. You can use GitHub Copilot to generate code-first schema definitions, update objects, or reverse-engineer and explore existing databases.

## Schema creation

#### Create basic schema

```copilot-prompt
Write a SQL script to create a new schema named `blog` for a blog application. The schema should include three tables: `Posts`, `Comments`, and `Users`. Each table must have appropriate primary keys, and the necessary foreign key relationships and constraints should be defined.
```

#### Modify schema

```copilot-prompt
Add a new column named `LastModified` of type `datetime` to the `Posts` table in the `blog` schema. Generate the updated SQL script reflecting this change, including the full definition of the modified schema.

It isn't needed to create the schema, but it would be great if you could use the script generated and run it to validate the accuracy of the generated code. The following section continues using this new schema called `blog`.
```

#### Create schema with relationships and constraints

```copilot-prompt
Generate a Prisma schema for a blog application using my current database. The schema should define a new database schema named `blog` and include tables for `posts`, `authors`, and `comments`, with appropriate relationships and constraints.
```

#### Create migration script to add a column

```copilot-prompt
Generate a Prisma migration to add a column called `LastModified` (`datetime`) to the `Post` table.
```

#### Reverse engineer an existing database

```copilot-prompt
Reverse engineer the current database and generate `CREATE TABLE` statements for all tables in the `SalesLT` schema.
```

#### Summarize a table structure

```copilot-prompt
Summarize the structure of the `SalesLT.Product` table in natural language.
```

#### Generate a Python model

```copilot-prompt
Generate a `models.py` (Django) file that reflects the structure of the `SalesLT.Customer` table.
```

#### Generate Entity Framework Core context and models

```copilot-prompt
Generate an Entity Framework Core DbContext and model classes for the `SalesLT` schema.
```

#### Create model definition and associations

```copilot-prompt
Create a Sequelize model definition for the `SalesLT.Product` and `SalesLT.Category` tables with appropriate associations.
```

#### Generate an entity from a table

```copilot-prompt
Generate a TypeORM entity for the `SalesLT.Customer` table, including primary key and indexed fields.
```

#### Generate a migration script to create a new table

```copilot-prompt
Generate a `knex.js` migration script to create the `SalesLT.SalesOrderHeader` table with `OrderDate`, `CustomerID`, and `TotalDue` columns.
```

## Define relationships

#### Define a relational script with foreign key references

```copilot-prompt
Write SQL to define a one-to-many relationship between `Users` and `Posts` in the `blog` schema. Ensure the foreign key in `Posts` references `Users(UserId)`.
```

#### Add a table to a schema with foreign key references

```copilot-prompt
Add a `Categories` table to the `blog` schema and update the `Posts` table to include a nullable foreign key referencing `Categories(CategoryId)`.
```

#### Update a database to add a table and update columns

```copilot-prompt
Write SQL to update the `Users` table to include a `RoleId` column and create a new `Roles` table. Define a foreign key relationship and enforce that every user must have a role.
```

#### Identify foreign key relationships for a table

```copilot-prompt
Identify and describe all foreign key relationships that involve the `SalesLT.SalesOrderHeader` table.
```

#### Replace a foreign key with a many-to-many relationship

```copilot-prompt
Write a SQL script that removes a foreign key between `Posts` and `Categories` in the `blog` schema and replaces it with a many-to-many relationship using a new join table.
```

#### Generate mapping between two tables

```copilot-prompt
Write Prisma relation mappings between `Customer`, `SalesOrderHeader`, and `SalesOrderDetail`.
```

#### Update a data model

```copilot-prompt
Update a Sequelize model to include a `hasMany` and `belongsTo` relationship between `Customer` and `Order`.
```

## Schema validation

#### Suggest constraints for sensitive data

```copilot-prompt
Suggest constraints for a table storing user passwords (for example, special characters and length limits).
```

#### Validate data type constraints

```copilot-prompt
Confirm that the `Name` column in `SalesLT.ProductCategory` doesn't use `nvarchar(max)` and has a reasonable maximum length constraint.
```

#### Validate primary key constraints

```copilot-prompt
Check whether the `SalesLT.Address` table has a primary key and all required fields defined.
```

#### Validate auditing records for tables

```copilot-prompt
Generate a SQL script to validate that all tables in the `SalesLT` schema include a `CreatedDate` or `ModifiedDate` column.
```

#### Define a model and include validation logic

```copilot-prompt
Define a SQLAlchemy model for the `Customer` table and include validation logic using Pydantic or custom Python validators before inserting into the database.
```

#### Add data annotations for format validation

```copilot-prompt
Add data annotations in an Entity Framework model to ensure fields like `Email` and `PhoneNumber` follow specific formats.
```

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot for MSSQL extension for Visual Studio Code](overview.md)
- [Quickstart: Use chat and inline GitHub Copilot suggestions](inline-copilot-suggestions.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
- [Limitations and known issues](limitations-and-known-issues.md)
