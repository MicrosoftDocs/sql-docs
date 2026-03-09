---
title: "Quickstart: Security Analyzer with GitHub Copilot"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how GitHub Copilot helps developers identify and address common security risks in SQL code and application-layer queries.
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

# Quickstart: Security analyzer

GitHub Copilot helps developers identify and address common security risks in SQL code and application-layer queries. It detects vulnerabilities like SQL injection, overexposed data, and unsafe patterns. Developers without a strong security background can use GitHub Copilot to get practical, context-aware recommendations during development.

## Get started

[!INCLUDE [get-started](../includes/get-started.md)]

## Detect and fix security risks with GitHub Copilot

GitHub Copilot helps developers detect and fix common security vulnerabilities early in the development process, before they reach production. Whether you're using raw SQL, object-relational mapping (ORM) frameworks, or stored procedures, GitHub Copilot can identify unsafe patterns, explain potential risks, and suggest safer alternatives based on your database context. This ability is especially useful for developers who don't specialize in security but need to follow secure coding practices.

The following sections describe common use cases and examples of what you can ask via the chat participant.

### SQL injection detection

SQL injection is one of the most common and dangerous security vulnerabilities in database applications. GitHub Copilot can help you identify unparameterized queries, string interpolation issues, and misuse of dynamic SQL. It also recommends safer, parameterized alternatives that fit your context.

#### SQLAlchemy in Python example

```copilot-prompt
I'm working with SQLAlchemy in Python for my current database `SalesLT` schema. Check the following `SQLAlchemy` query for potential security risks, such as SQL injection, over-fetching, or performance issues. If applicable, suggest improvements using parameterized queries, connection pooling, and other secure `SQL Server` practices to ensure performance and security.

query = f"SELECT * FROM SalesLT.Customer WHERE LastName = '{user_input}'"
result = engine.execute(query).fetchall()
```

#### JavaScript SQL example

```copilot-prompt
Analyze the following JavaScript SQL query for potential security vulnerabilities. Identify risks such as SQL injection, over-fetching, and poor authentication practices. Explain why this query is insecure and provide a secure alternative.

const query = `SELECT * FROM Users WHERE Username = '${username}' AND Password = '${password}'`;
```

#### SQL injection attack simulation

```copilot-prompt
Using my current database, simulate a SQL injection attack for the `SalesLT.uspGetCustomerOrderHistory` stored procedure and suggest fixes.
```

#### Review stored procedure example

```copilot-prompt
Review the stored procedure `SalesLT.uspGetCustomerOrderHistory` in my current database for potential SQL injection vulnerabilities. Explain how unparameterized or improperly validated inputs could be exploited and recommend secure coding practices.
```

#### Identify security issues example

```copilot-prompt
Review the `SalesLT.uspGetCustomerOrderHistory_Insecure` stored procedure. Identify any potential security issues in the implementation and then provide a revised version of the stored procedure that addresses these concerns without explicitly listing security best practices.

You can use the following T-SQL to create the stored procedure:

CREATE OR ALTER PROCEDURE [SalesLT].[uspGetCustomerOrderHistory_Insecure]
@CustomerID NVARCHAR (50)
AS
BEGIN
    DECLARE @SQL AS NVARCHAR (MAX) = N'SELECT *
    FROM SalesLT.SalesOrderHeader
    WHERE CustomerID = ' + @CustomerID + ';';
    EXECUTE (@SQL);
END
GO
```

### General security suggestions

Beyond SQL injection, many database applications expose sensitive data or rely on insecure default configurations.

GitHub Copilot can assist by providing guidance on encrypting connections, protecting or masking personal data, and following secure authentication and authorization practices across various development stacks.

#### Sensitive data storage example

```copilot-prompt
Recommend secure methods for storing sensitive data in the `SalesLT.Address` table.
```

#### Masking personal data example

```copilot-prompt
What are the best strategies or built-in features in my database for masking personal data in the `SalesLT.Customer` table?
```

#### Enforce encryption in Entity Framework Core example

```copilot-prompt
How can I configure my connection string in Entity Framework Core to enforce encryption and avoid exposing credentials?
```

#### Microsoft Entra ID in Node.js authentication example

```copilot-prompt
In a Prisma or Node.js environment, how can I securely use Microsoft Entra ID authentication or managed identity with SQL Server instead of storing passwords?
```

#### Recommend SQL Server options for securing data example

```copilot-prompt
What SQL Server options should I enable or verify (for example, Always Encrypted, Transparent Data Encryption) to protect customer data when using object-relational mapping (ORM) frameworks like Sequelize or EF Core?
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
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
- [Limitations and known issues](limitations-and-known-issues.md)
