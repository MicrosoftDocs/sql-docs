---
title: Object-Relational Mapping Integrations with GitHub Copilot
titleSuffix: MSSQL Extension for Visual Studio Code
description: Reference guide for using GitHub Copilot with the MSSQL extension across Entity Framework, Prisma, Sequelize, SQLAlchemy, Django ORM, TypeORM, Drizzle, and Dapper.
author: cwebb-msft
ms.author: chweb
ms.reviewer: roblescarlos
ms.date: 06/01/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: reference
ms.collection:
  - data-tools
  - ce-skilling-ai-copilot
ai-usage: ai-assisted
---

# Object-relational mapping integrations with GitHub Copilot

GitHub Copilot and the MSSQL extension work with every major object-relational mapping (ORM) framework for SQL Server and Azure SQL Database. This reference covers the supported ORMs, what each can do, and an end-to-end example that takes you from a SQL Server table to a typed model, migration, and data access method.

## Key takeaways

- GitHub Copilot generates models, migrations, and data access code tailored to your chosen ORM.
- The [`@mssql` chat participant](chat-ask-mode.md) reads your database schema and uses it as context when generating ORM code.
- [Custom instructions](custom-instructions.md) can enforce conventions across both Transact-SQL (T-SQL) and ORM layers.

## ORM support matrix

| ORM | Stack | Models | Migrations | Schema-first | Code-first |
| --- | --- | --- | --- | --- | --- |
| **Entity Framework Core** | .NET / C# | Yes | Yes | Yes | Yes |
| **Prisma** | Node.js / TypeScript | Yes | Yes | Yes | Yes |
| **Sequelize** | Node.js | Yes | Yes | Yes | Yes |
| **SQLAlchemy** | Python | Yes | Yes (via Alembic) | Yes | Yes |
| **Django ORM** | Python | Yes | Yes | Yes | Limited |
| **TypeORM** | Node.js / TypeScript | Yes | Yes | Yes | Yes |
| **Drizzle** | Node.js / TypeScript | Yes | Yes | Yes | Yes |
| **Dapper** | .NET / C# | Micro-ORM | Manual | No | Yes |

## End-to-end scenario

Each section uses the same scenario: you have `SalesLT.Customer` in SQL Server and want to add an `email` column, generate a typed model, and produce a data access method.

### Entity Framework Core

#### 1. Generate the model

```copilot-prompt
@mssql Generate an Entity Framework Core entity class for
SalesLT.Customer. Use C# records where appropriate. Target
Entity Framework Core 9 with SQL Server provider.
```

#### 2. Generate the migration

```copilot-prompt
@mssql Generate an Entity Framework Core migration to add an
`Email` column (nvarchar(256), nullable) to the Customer entity.
Use the EF Core Add-Migration conventions and include both the
Up and Down methods.
```

#### 3. Generate the data access method

```copilot-prompt
@mssql Write a CustomerRepository method that returns all active
customers ordered by LastName. Use the DbContext pattern, async/await,
and return IReadOnlyList<Customer>.
```

### Prisma

#### 1. Generate the schema model

```copilot-prompt
@mssql Generate a Prisma model for SalesLT.Customer using the
sqlserver provider. Use @map annotations to match the existing
column names. Set the primary key and unique constraints explicitly.
```

#### 2. Generate the migration

```copilot-prompt
@mssql Generate a Prisma migration SQL file to add an `email`
column (NVARCHAR(256), nullable) to the SalesLT.Customer table,
compatible with the sqlserver provider.
```

#### 3. Generate the data access method

```copilot-prompt
@mssql Write a TypeScript function that uses the Prisma client to
return all active customers ordered by last name, including strict
TypeScript types.
```

### Sequelize

#### 1. Generate the model

```copilot-prompt
@mssql Generate a Sequelize model class for SalesLT.Customer using
the sequelize-typescript decorators pattern. Include data type
mappings for DATETIME2 and NVARCHAR columns.
```

#### 2. Generate the migration

```copilot-prompt
@mssql Generate a Sequelize migration in JavaScript to add an
`email` column (STRING(256), nullable) to the `Customer` table in
the `SalesLT` schema.
```

#### 3. Generate the data access method

```copilot-prompt
@mssql Write a Sequelize repository method using async/await that
returns all active customers ordered by LastName. Use model scopes
for the `active` filter.
```

### SQLAlchemy

#### 1. Generate the model

```copilot-prompt
@mssql Generate a SQLAlchemy 2.0 model for SalesLT.Customer using
the declarative Mapped[] syntax. Use the pyodbc driver connection
string format for SQL Server.
```

#### 2. Generate the Alembic migration

```copilot-prompt
@mssql Generate an Alembic migration script to add an `email`
column (NVARCHAR(256), nullable) to the SalesLT.Customer table.
Include both upgrade and downgrade functions.
```

#### 3. Generate the data access method

```copilot-prompt
@mssql Write a SQLAlchemy 2.0 async repository method using
select() and scalars() that returns all active customers ordered
by last name.
```

### Django ORM

#### 1. Generate the model

```copilot-prompt
@mssql Generate a Django model for SalesLT.Customer using
django-mssql-backend. Include Meta.db_table to map to the
existing table name with the SalesLT schema.
```

#### 2. Generate the migration

```copilot-prompt
@mssql Generate a Django migration to add an `email` field
(CharField, max_length=256, null=True) to the Customer model.
Use the AddField operation.
```

#### 3. Generate the data access method

```copilot-prompt
@mssql Write a Django queryset manager method that returns all
active customers ordered by last_name, using select_related for
any foreign key fields.
```

### TypeORM

#### 1. Generate the entity

```copilot-prompt
@mssql Generate a TypeORM entity class for SalesLT.Customer.
Use decorators for @Entity, @Column, @PrimaryGeneratedColumn.
Include DATETIME2 and NVARCHAR mappings for SQL Server.
```

#### 2. Generate the migration

```copilot-prompt
@mssql Generate a TypeORM migration class to add an `email`
column (nvarchar 256, nullable) to the SalesLT.Customer table.
Include both up and down methods.
```

#### 3. Generate the data access method

```copilot-prompt
@mssql Write a TypeORM repository method that uses the query
builder to return all active customers ordered by last name.
Include strict TypeScript types.
```

### Drizzle

#### 1. Generate the schema

```copilot-prompt
@mssql Generate a Drizzle schema definition for SalesLT.Customer
using the mssql dialect. Include type-safe column mappings for
nvarchar, datetime2, and bit types.
```

#### 2. Generate the migration

```copilot-prompt
@mssql Generate a Drizzle migration SQL file to add an `email`
column (nvarchar 256, nullable) to the SalesLT.Customer table.
```

#### 3. Generate the data access method

```copilot-prompt
@mssql Write a Drizzle query using the query builder that returns
all active customers ordered by last name. Use type-safe column
references.
```

### Dapper

Dapper is a micro-ORM with no schema generation or migration support, but GitHub Copilot can generate data access methods that use Dapper's extension methods against an existing schema.

```copilot-prompt
@mssql Write a Dapper-based repository method in C# that returns
all active customers ordered by LastName. Use parameterized
queries and a typed Customer record.
```

## Common patterns and caveats

- **Entity Framework Core + SQL Server collation.** For case-sensitive comparisons, explicitly set `EF.Functions.Collate` in queries. Don't assume default collation matches SQL Server's server-level setting.
- **Prisma + MSSQL connection string quoting.** Azure SQL Database connection strings need careful URL encoding of special characters in passwords. See the [Prisma SQL Server provider docs](https://www.prisma.io/docs/orm/core-concepts/supported-databases/sql-server).
- **SQLAlchemy + pyodbc driver.** Install and reference the correct driver version (`ODBC Driver 18 for SQL Server` as of 2026). Pin versions in `requirements.txt` to avoid surprises.
- **Django ORM + primary key mapping.** `IDENTITY` columns require the `django-mssql-backend` package and `IDENTITY_INSERT` handling for data loads.
- **TypeORM + camelCase.** Set `entityPrefix` and use naming strategies to align with your [custom instructions](custom-instructions.md) if your team uses a specific convention.

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [Quickstart: Chat with the `@mssql` participant (ask mode)](chat-ask-mode.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use custom instructions to align GitHub Copilot with your T-SQL conventions](custom-instructions.md)
- [Limitations and known issues](limitations-and-known-issues.md)
