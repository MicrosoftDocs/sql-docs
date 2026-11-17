---
title: "Quickstart: Query Optimizer Assistant with GitHub Copilot"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Use GitHub Copilot to optimize SQL queries, investigate Query Store performance, and analyze execution plans in the MSSQL extension for Visual Studio Code.
author: cwebb-msft
ms.author: chweb
ms.reviewer: yoleichen
ms.date: 07/07/2026
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

# Quickstart: Query optimizer assistant

GitHub Copilot helps you optimize queries and investigate performance bottlenecks in the MSSQL extension for Visual Studio Code. You can ask for help with Transact-SQL (T-SQL) and object-relational mapping (ORM) code, interpret execution plans, or use agent tools to investigate captured Query Store data.

## Get started

[!INCLUDE [get-started](../includes/get-started.md)]

## Optimize performance with GitHub Copilot

Use the `@mssql` chat participant to review SQL and ORM queries, explain execution plans, and suggest improvements. The examples start with query optimization and code-first scenarios, followed by a Query Store agent example in preview.

### Optimize queries

Use GitHub Copilot to identify inefficiencies in SQL or object-relational mapping (ORM) queries and suggest ways to improve performance. GitHub Copilot helps you apply T-SQL and ORM best practices, from rewriting slow queries to recommending indexes or avoiding anti-patterns like Cartesian joins, based on your current context.

#### Basic example

```copilot-prompt
Optimize the following query:

SELECT *
FROM SalesLT.SalesOrderHeader
WHERE OrderDate > '2023-01-01';
```

#### Index improvement example

```copilot-prompt
Suggest indexing improvements for this query:

SELECT ProductID
FROM SalesLT.SalesOrderDetail
WHERE Quantity > 100;
```

#### Join improvement example

```copilot-prompt
Rewrite this query to avoid a Cartesian join. Make sure the new query follows T-SQL best practices:

SELECT * FROM Customers, Order;
```

#### Nested select example

```copilot-prompt
Rewrite this Prisma query to avoid unnecessary nested selects and improve readability:

const orders = await prisma.salesOrderHeader.findMany({
  where: {
    orderDate: {
      gt: new Date('2023-01-01')
    }
  }
});
```

### Execution plan analysis

Execution plans provide a detailed breakdown of how the SQL engine processes queries. GitHub Copilot can help you interpret execution plans, identify bottlenecks like nested loop joins, and suggest improvements based on real-world query patterns and indexing strategies.

You can use the following query as an example to generate the execution plan using the Estimated/Actual plan option in the MSSQL extension:

```sql
SELECT soh1.SalesOrderID AS OrderA,
       soh2.SalesOrderID AS OrderB,
       soh1.TotalDue AS TotalA,
       soh2.TotalDue AS TotalB
FROM SalesLT.SalesOrderHeader AS soh1
    CROSS JOIN SalesLT.SalesOrderHeader AS soh2
WHERE soh1.TotalDue < soh2.TotalDue
ORDER BY soh2.TotalDue DESC;
```

Include as much context as possible, by selecting the query from the editor and including the `sqlplan` file in the GitHub Copilot chat window, as shown in this screenshot.

:::image type="content" source="media/query-optimizer-assistant/vscode-execution-plan-example.png" alt-text="Screenshot showing an execution plan example in Visual Studio Code." lightbox="media/query-optimizer-assistant/vscode-execution-plan-example.png":::

```copilot-prompt
According to the execution plan shared by my database expert, the following query is using a nested loop join which is affecting the performance of my app. Can you explain in simple terms why this might be happening? Additionally, suggest optimization strategies that could improve the query's performance.
```

You can use the following query as an example to generate the execution plan using the Estimated/Actual plan option in the MSSQL extension:

```sql
SELECT c1.CustomerID,
       c1.LastName,
       c2.CustomerID AS MatchingCustomerID,
       c2.LastName AS MatchingLastName
FROM SalesLT.Customer AS c1
     INNER JOIN SalesLT.Customer AS c2
         ON c1.LastName = c2.LastName
        AND c1.CustomerID <> c2.CustomerID
OPTION (LOOP JOIN);
```

Include as much context as possible by selecting the query from the editor and including the `sqlplan` file in the GitHub Copilot chat window, as shown in this screenshot.

:::image type="content" source="media/query-optimizer-assistant/vscode-nested-loop-join-example.png" alt-text="Screenshot showing an execution plan with nested loop join in Visual Studio Code." lightbox="media/query-optimizer-assistant/vscode-nested-loop-join-example.png":::

```copilot-prompt
Explain the execution plan for this query that performs a join with a filter on TotalDue:

SELECT c.CustomerID,
       c.FirstName,
       c.LastName,
       soh.SalesOrderID,
       soh.TotalDue
FROM SalesLT.Customer AS c
     INNER JOIN SalesLT.SalesOrderHeader AS soh
         ON c.CustomerID = soh.CustomerID
WHERE soh.TotalDue > 500;
```

### Query restructuring

Restructuring queries using common table expressions (CTEs) can make your queries easier to read and maintain. This approach is especially helpful for complex logic or nested subqueries. GitHub Copilot can help you rewrite your existing queries to use CTEs while preserving the original intent and improving clarity.

#### Inner select to CTE example

```copilot-prompt
Rewrite this query using common table expressions (CTEs) to improve clarity:

SELECT *
FROM (SELECT ProductID,
             SUM(Quantity) AS TotalQuantity
      FROM Sales
      GROUP BY ProductID) AS SubQuery;
```

#### HAVING clause to CTE example

```copilot-prompt
Rewrite the following query using a CTE (common table expression) to improve readability and maintainability:

SELECT soh.CustomerID,
       COUNT(*) AS OrderCount
FROM SalesLT.SalesOrderHeader AS soh
WHERE soh.OrderDate > '2022-01-01'
GROUP BY soh.CustomerID
HAVING COUNT(*) > 5;
```

#### Aggregation clause to CTE example

```copilot-prompt
Use a CTE to separate the aggregation logic from the filter condition in this query:

SELECT ProductID,
       AVG(UnitPrice) AS AvgPrice
FROM SalesLT.SalesOrderDetail
GROUP BY ProductID
HAVING AVG(UnitPrice) > 50;
```

### Code-first performance scenarios

When you work with ORMs like Entity Framework, Prisma, or Sequelize, performance can degrade if you don't optimize your queries. GitHub Copilot helps you detect and resolve problems such as missing indexes, inefficient filtering, and N+1 problems in code-first workflows.

#### Prisma example

```copilot-prompt
In a Prisma project, how would you ensure that queries filtering by `OrderDate` in `SalesOrderHeader` are using indexes effectively?
```

#### Entity Framework Core example

```copilot-prompt
Using Entity Framework Core, how can you analyze and optimize a LINQ query that retrieves the top 10 customers by total order value?
```

#### Sequelize example

```copilot-prompt
In Sequelize, how do you restructure a query that fetches order history with product details to minimize N+1 query issues?
```

<a id="investigate-query-store-performance-preview"></a>

## Investigate queries with agent mode (preview)

Use the preview [Query Store agent tools](agent-mode.md#query-store-performance-tools-preview) directly in agent mode. You don't need the dashboard, but you need captured Query Store data and permission to read it. See [prerequisites](../mssql/mssql-query-performance-dashboard.md#prerequisites).

The `#mssql_query_store_analyze_query` tool retrieves SQL text, stored plans, runtime statistics, execution history, and waits. You don't pass SQL text or plan XML to it.

| Input | What to provide |
| --- | --- |
| Query | Required `queryId` from Query Store. If unknown, ask the agent to find top queries first. |
| Connection | Required `profileId` or `connectionId` backed by a saved profile. Name the profile or connection in your prompt. |
| Database | Optional `database`; defaults to the connection's or profile's database. |
| Time frame | Optional `hours` (default 24), or both `startUtc` and `endUtc` as ISO 8601 UTC timestamps. |
| Plans | Optional `planIds` to prioritize summaries; defaults to plans with the most total duration. |

Replace the example query ID, database, and profile with your own:

```copilot-prompt
#mssql_query_store_analyze_query Analyze query 4 in AdventureWorksLT using my LocalDev profile for the past 24 hours. Explain the main findings. Don't make changes.
```

In the [Query Performance Dashboard (preview)](../mssql/mssql-query-performance-dashboard.md), **Chat** is an optional shortcut in a query row's actions menu (**...**) or the query detail page. It opens agent mode with the query ID, profile, database, and selected time range filled in.

:::image type="content" source="../mssql/media/mssql-query-performance-dashboard/query-performance-dashboard-chat-menu.png" alt-text="Screenshot of the query overview actions menu with the optional Chat entry point for a Query Store investigation.":::

Diagnostic reads don't modify your database. Test recommended changes before applying them.

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot for MSSQL extension for Visual Studio Code](overview.md)
- [Query Performance Dashboard (preview)](../mssql/mssql-query-performance-dashboard.md)
- [Quickstart: Use GitHub Copilot agent mode](agent-mode.md)
- [Quickstart: Chat with the `@mssql` participant (ask mode)](chat-ask-mode.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
- [Limitations and known issues](limitations-and-known-issues.md)
