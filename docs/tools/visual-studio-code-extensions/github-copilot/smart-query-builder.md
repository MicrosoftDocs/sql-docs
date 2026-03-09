---
title: "Quickstart: Smart Query Builder with GitHub Copilot"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how GitHub Copilot's Query Building Assistant helps construct efficient, accurate, and secure SQL queries or ORM-compatible code in Visual Studio Code.
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

# Quickstart: Use the smart query builder

In this quickstart, you learn how the query building assistant helps you craft efficient, accurate, and secure queries using either raw SQL or your preferred ORM. Designed for both code-first and data-first developers, it enables faster generation of production-ready logic aligned with your connected database schema.

## Get started

[!INCLUDE [get-started](../includes/get-started.md)]

## Query building

GitHub Copilot supports intelligent query construction directly within Visual Studio Code. From basic SELECTs to complex joins, filters, and aggregations, it generates SQL or ORM queries that follow best practices and reflect your current schema, so you can focus on your application logic.

Here are common use cases and examples of what you can ask via the chat participant:

### Time-based analysis

These prompts help analyze trends over time, such as recent sales activity, top performers by period, or comparisons to historical averages. GitHub Copilot can build queries that calculate values relative to your data's most recent dates, avoiding assumptions based on the current system date.

#### Return list of above-average sales orders for last six months

```copilot-prompt
Generate a nested query to fetch orders from `SalesLT.SalesOrderHeader` where the total is above the average order amount for the last six months, relative to the most recent order date in the database (not relative to the current date).
```

#### Return top three customers grouped by year

```copilot-prompt
Write a query to find the top three customers by total sales in the `SalesLT.SalesOrderHeader` table, grouped by year.
```

#### Return total revenue per customer in the last 30 days

```copilot-prompt
Find the total revenue for each customer in `SalesLT.Customer` who has placed orders in the last 30 days, relative to the most recent order date in `SalesLT.SalesOrderHeader` (not relative to the current date).
```

#### Return customers and orders in the last year

```copilot-prompt
Create a Sequelize query to fetch `Customers` (`SalesLT.Customers`) along with their orders (`SalesLT.SalesOrderDetail`) and total revenue, sorted by descending revenue during the last year in the database (not relative to the current date).
```

### Complex relationships

Use these prompts to generate queries that span multiple related tables. Whether you're joining customer data with order details or building revenue aggregations, GitHub Copilot helps you navigate complex relationships by using schema context to produce accurate joins and conditions.

#### Return a list of orders above the average total

```copilot-prompt
Using the actual schema of the `SalesLT.SalesOrderHeader` table, generate a nested SQL query that retrieves orders where the order total is above the average order total for the last six months. The six-month period should be calculated relative to the most recent order date in the table (not the current date).
```

#### Return customers ordered by revenue

```copilot-prompt
Using my current database, create a SQLAlchemy query to fetch customers along with their orders and total revenue, sorted by descending revenue.
```

#### Generate a query for total revenue per customer

```copilot-prompt
Using Prisma, generate a query that joins `SalesLT.Customer`, `SalesLT.SalesOrderHeader`, and `SalesLT.SalesOrderDetail` and calculates total revenue per customer.
```

#### Return top ten customers by sales

```copilot-prompt
In Entity Framework, write a LINQ query that returns the top 10 customers by sales in the past year using the `SalesLT` schema.
```

#### Return products not sold relative to recent sales

```copilot-prompt
Write a TypeORM query that finds products that haven't been sold in the last six months. The six-month period should be calculated relative to the most recent order date in the table (not the current date).
```

#### Retrieve customers based on total spend

```copilot-prompt
Write a Django ORM query that retrieves all customers who have made purchases in the last year, sorted by total spending. The "last year" period should be calculated relative to the most recent order date in the table (not the current date).
```

### Business insights

These prompts provide actionable insights from your data. From identifying churn-risk customers to finding unsold products, GitHub Copilot can help you build logic that supports strategic decisions and reporting, tailored to your connected database.

#### Identify new customers

```copilot-prompt
Using my current database, generate a list that shows which customers have placed their first order in the last six months, using the most recent order date in the database as the reference point.
```

#### Identify products with no recent sales

```copilot-prompt
Using my current database, generate a list that identifies products that haven't been sold in the last 12 months, using the most recent order date in the database as the reference.
```

#### Identify high-value customers with no recent purchases

```copilot-prompt
Identify customers who have placed more than five orders but none in the last 90 days, using the most recent order date in the database as reference.
```

#### Return the top five products based on return rate

```copilot-prompt
List the top five products with the highest return rate based on order returns or cancellations, calculated relative to the most recent order date.
```

#### Generate monthly revenue trend data

```copilot-prompt
Generate a trend of monthly revenue over the last 12 months based on `OrderDate` in `SalesLT.SalesOrderHeader`, using the most recent order date as the anchor.
```

#### Create a declining order frequency report

```copilot-prompt
Using SQLAlchemy and Pandas, create a report that identifies customers with declining order frequency over the last three quarters based on the most recent order date.
```

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot for MSSQL extension for Visual Studio Code](overview.md)
- [Quickstart: Use chat and inline GitHub Copilot suggestions](inline-copilot-suggestions.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
- [Limitations and known issues](limitations-and-known-issues.md)
