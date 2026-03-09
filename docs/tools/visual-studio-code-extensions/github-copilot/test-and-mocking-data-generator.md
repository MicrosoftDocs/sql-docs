---
title: "Quickstart: Data Generator for Testing and Mocking"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how GitHub Copilot helps developers quickly create realistic and themed datasets to support SQL database application development, testing, and demos.
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

# Quickstart: Generate data for testing and mocking

In this quickstart, you learn how to use GitHub Copilot to create realistic and themed datasets to support application development, testing, and demos. By analyzing the schema and context of your database, GitHub Copilot can generate mock data aligned with real-world formats, simulate edge cases, and reduce the manual effort of seeding databases, making testing faster and more representative of actual scenarios.

## Get started

[!INCLUDE [get-started](../includes/get-started.md)]

## Generate realistic and testable data with GitHub Copilot

GitHub Copilot can help you generate test and mock data directly from your SQL schema or JSON samples. It offers contextual suggestions to help reduce time and improve coverage, whether you're preparing datasets for demos, testing edge cases, or seeding your development environment with themed or randomized data. These suggestions are especially useful in scenarios where manual data entry would be slow or inconsistent.

Here are common use cases and examples of what you can ask via the chat participant.

### Mock data generation

Use GitHub Copilot to generate themed, randomized, or representative mock data for your existing tables. You can request specific row counts, apply name/value patterns, or build datasets based on external structures like JSON samples.

#### Mock customer data example

```copilot-prompt
Generate mock data for the `SalesLT.Customer` table with 100 sample records.
```

#### Mock product data example

```copilot-prompt
Populate the `SalesLT.Product` table with 50 items, each with unique names and prices.
```

#### Mock sales data example

```copilot-prompt
Generate mock data for the `SalesLT.SalesOrderHeader` table with 200 records, including order dates and customer IDs.
```

#### Generate mock data from JSON sample

```copilot-prompt
Based on this sample JSON with four records, generate a SQL table schema and populate it with 50 mock records. Use character names from well-known sci-fi books (for example, Dune, Foundation, Ready Player One) for the `firstName` and `lastName` fields to make the data more realistic and themed:

[
  { "firstName": "Alice", "lastName": "Smith", "email": "alice@example.com" },
  { "firstName": "Bob", "lastName": "Jones", "email": "bob@example.com" },
  { "firstName": "Charlie", "lastName": "Brown", "email": "charlie@example.com" },
  { "firstName": "Dana", "lastName": "White", "email": "dana@example.com" }
]
```

### Edge-case testing

Go beyond basic data generation by using GitHub Copilot to simulate edge cases and verify your system's behavior. GitHub Copilot can help generate the right data, whether you're stress-testing business logic, checking for data validation failures, or ensuring relational consistency. It can also write assertions or test logic to validate outcomes.

#### Test quantity constraints

```copilot-prompt
Generate insert statements for `SalesLT.SalesOrderDetail` with `OrderQty` values at the upper boundary (for example, 1,000 units) and verify that the system enforces quantity constraints.
```

#### Test email address format

```copilot-prompt
Create test data for `SalesLT.Customer` with invalid email formats and write a query that flags these records for review.
```

#### Test edge-case pricing anomalies

```copilot-prompt
Generate test data for `SalesLT.Product` with edge-case pricing, such as `StandardCost = 0` or negative values, and write a query that highlights anomalies.
```

#### Test data integrity with mocking

```copilot-prompt
Simulate data integrity by generating 500 `SalesOrderDetail` rows that correctly reference valid `ProductID` and `SalesOrderID` values from related tables, and ensure GitHub Copilot includes validation logic.
```

#### Test business logic

```copilot-prompt
Write a test script that confirms the `SalesOrderHeader.TotalDue` value is always greater than the `SubTotal` for each order, helpful for spotting miscalculations in business logic.
```

#### Test null validation

```copilot-prompt
Using SQLAlchemy, create a test that attempts to insert a `SalesOrderDetail` record with a null `ProductID` and verify that the ORM raises an integrity error due to the foreign key constraint.
```

#### Test negative values

```copilot-prompt
With Prisma, generate test logic that tries to insert a `Product` with a `StandardCost` of `-10`. Validate that Prisma rejects the entry and logs an appropriate error message.
```

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot for MSSQL extension for Visual Studio Code](overview.md)
- [Quickstart: Use chat and inline GitHub Copilot suggestions](inline-copilot-suggestions.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Limitations and known issues](limitations-and-known-issues.md)
