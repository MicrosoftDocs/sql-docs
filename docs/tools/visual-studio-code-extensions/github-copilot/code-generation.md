---
title: "Quickstart: Code Generation with GitHub Copilot for SQL"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how GitHub Copilot can accelerate database-related code generation for SQL scripts and ORM migrations in Visual Studio Code.
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

# Quickstart: Generate code

In this quickstart, you learn how GitHub Copilot accelerates SQL and object-relational mapping (ORM) development by generating context-aware code directly within Visual Studio Code. GitHub Copilot helps you scaffold tables, evolve schemas, and reduce repetitive scripting. Whether you're using Transact-SQL, or working with object-relational mapping (ORM) frameworks like Entity Framework, Sequelize, Prisma, or SQLAlchemy, GitHub Copilot helps you focus on building application logic.

## Get started

[!INCLUDE [get-started](../includes/get-started.md)]

## Code generation with GitHub Copilot

Use GitHub Copilot to generate SQL and ORM-compatible code that reflects your connected database's structure and follows best practices. From defining tables and relationships to scripting views, building migration files, or scaffolding data access layers and APIs, GitHub Copilot helps you move faster and with greater confidence.

Here are common use cases and examples of what you can ask through the chat participant:

### Generate SQL code

GitHub Copilot can help you generate SQL code for several development scenarios, from scripting, creating, and modifying tables to writing stored procedures and views. These examples illustrate how you can use GitHub Copilot to automate repetitive SQL scripting and follow best practices for T-SQL development.

#### Script out all tables in a schema

```copilot-prompt
Script out all the tables in the `SalesLT` schema as `CREATE TABLE` statements in SQL.
```

#### Retrieve customer data with a stored procedure

```copilot-prompt
Write a SQL stored procedure in my current database. The procedure should retrieve all customers from the `SalesLT.Customer` table where the `LastName` matches a given parameter. Make sure to use T-SQL best practices.
```

#### Script out a table with all constraints and indexes

```copilot-prompt
Script out the `SalesLT.Customer` table as a `CREATE TABLE` statement, including all constraints and indexes.
```

#### Script out a view that joins two tables

```copilot-prompt
Generate a SQL script to create a view that joins the `SalesLT.Customer` and `SalesLT.SalesOrderHeader` tables, showing customer names and their total order amounts.
```

#### Alter a table by adding a new column

```copilot-prompt
Write a SQL script to alter the `SalesLT.Customer` table by adding a `last_updated` column with a default timestamp.
```

### Generate ORM migrations

GitHub Copilot can generate ORM-compatible migrations and model definitions based on your schema context and framework of choice. From Sequelize to Entity Framework, Prisma, and SQLAlchemy, GitHub Copilot helps scaffold changes that align with your application's data model.

#### Generate a model to add a column

```copilot-prompt
Generate a Sequelize (JavaScript) model to add an `email` column (`varchar(256)`) to the `SalesLT.Customer` table.
```

#### Generate Entity Framework model class

```copilot-prompt
Generate an Entity Framework model class in C# to represent a `SalesLT.ProductModel` table with `id`, `name`, and `description` columns.
```

#### Generate Entity Framework model

```copilot-prompt
Generate an Entity Framework model in C# based on the existing `SalesLT.Product` table.
```

#### Write Python code to define a table

```copilot-prompt
Write SQLAlchemy code to define a `SalesLT.OrderDetails` table with `id`, `order_date`, and `customer_id` fields, ensuring compatibility with `Python`.
```

#### Write a parameterized query

```copilot-prompt
Using SQLAlchemy, write a parameterized query that retrieves all customers from the `SalesLT.Customer` table where the `LastName` matches a provided parameter.
```

#### Update existing model to add a table

```copilot-prompt
Update my existing Prisma model (schema.prisma) to define a new `SalesLT.Order` model with `id`, `customer_id`, and `order_date` fields.
```

#### Generate a model class for a table

```copilot-prompt
Generate a SQLAlchemy model class for the `SalesLT.Product` table, including columns and data types.
```

### Generate boilerplate app code

GitHub Copilot can also help scaffold backend and frontend components that interact with your SQL database. These examples show how you can go from schema to working application code using popular stacks like Azure Functions, Node.js, Django, and Next.js.

#### Serverless backend SQL bindings and Blazor

The following example shows full prompts you can use with GitHub Copilot Chat to scaffold an end-to-end solution. These prompts include detailed instructions and context to help Copilot generate accurate, structured code across both backend and frontend layers.

```copilot-prompt
Generate a full-stack app using Azure SQL bindings for Functions and Blazor WebAssembly. Follow these steps:

1. Backend: Azure Functions (C#) with SQL Bindings

   - Configure SQL Bindings to automatically read and write data from the `SalesLT.Customer` table.
   - Implement HTTP-triggered functions with the following endpoints:
     - `GET /api/customers` - Fetch all customers.
     - `GET /api/customers/{id}` - Get a specific customer by ID.
     - `POST /api/customers` - Create a new customer.
     - `PUT /api/customers/{id}` - Update an existing customer.
     - `DELETE /api/customers/{id}` - Delete a customer.
   - Use `Dependency Injection` for database connections and logging.
   - Include an `appsettings.json` file to store database connection strings and environment variables.
   - Use `Azure Functions Core Tools` to run and test the functions locally.

1. Frontend: Blazor WebAssembly (Optional)

   - Create a Blazor WebAssembly frontend that consumes the API.
   - Display a table with customer data and a form to add new customers.
   - Use `HttpClient` to call the Azure Functions endpoints.
   - Implement two-way data binding to handle form inputs dynamically.
   - Use Bootstrap or Blazor components for styling and layout.

Ensure the project includes setup instructions for running both the Azure Functions backend and Blazor WebAssembly frontend locally, with proper `.env` or `local.settings.json` configurations for database connections.
```

#### Full-stack with Node.js and Next.js

The following example is a detailed prompt you can provide in GitHub Copilot Chat to generate the full backend setup, including API routes and database integration.

```copilot-prompt
Generate a REST API using Node.js with Express that connects to my local SQL Database. Use the Tedious package for SQL Server connections and Prisma as the ORM. Follow these steps:

1. Backend: Node.js + Express

   - Establish a database connection using Prisma with Tedious as the SQL Server driver.
   - Implement API routes for `SalesLT.Customer` with the following endpoints:
     - `GET /customers` - Fetch all customers.
     - `GET /customers/:id` - Get a specific customer by ID.
     - `POST /customers` - Create a new customer.
     - `PUT /customers/:id` - Update an existing customer.
     - `DELETE /customers/:id` - Delete a customer.
   - Configure `Prisma` to map the `SalesLT.Customer` table and generate database migrations using `prisma migrate dev`.
   - Use `dotenv` for environment variables (database credentials, ports, etc.).
   - Add `Jest` for testing the API endpoints.

1. Frontend: Next.js + TypeScript (Optional)

   - Create a Next.js frontend that consumes the API.
   - Display a table with customer data and a form to add new customers.
   - Use React hooks (`useState`, `useEffect`) to manage state and fetch data dynamically.
   - Style the UI using Tailwind CSS.
   - Implement server-side data fetching (`getServerSideProps`) in Next.js for improved performance.

Ensure the project includes setup instructions for running both the backend and frontend independently, with proper `.env` configurations for the database connection.
```

#### Backend: Django + Django REST framework

The following example is a detailed prompt you can provide in GitHub Copilot Chat to generate the full backend setup, including API routes and database integration.

```copilot-prompt
Scaffold a Django backend with Django REST Framework for the `SalesLT.Customer` table. Follow these steps:

- Implement API routes using Django's `ModelViewSet` with the following endpoints:
  - `GET /customers` - Fetch all customers.
  - `GET /customers/{id}` - Get a specific customer by ID.
  - `POST /customers` - Create a new customer.
  - `PUT /customers/{id}` - Update an existing customer.
  - `DELETE /customers/{id}` - Delete a customer.

- Add instructions for generating database migrations with `python manage.py makemigrations` and `migrate`.
```

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot for MSSQL extension for Visual Studio Code](overview.md)
- [Quickstart: Use chat and inline GitHub Copilot suggestions](inline-copilot-suggestions.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
- [Limitations and known issues](limitations-and-known-issues.md)
