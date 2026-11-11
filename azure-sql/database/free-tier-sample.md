---
title: "Tutorial: Learn SQL with AdventureWorksLT in Azure SQL Database"
description: Learn SQL with AdventureWorksLT sample data and build a product explorer in Azure SQL Database. Start with the free tier or use a paid database.
ms.service: azure-sql-database
ms.subservice: development
ms.topic: tutorial
ms.date: 11/10/2026
ai-usage: ai-assisted
---

# Tutorial: Learn SQL with AdventureWorksLT in Azure SQL Database

Start learning SQL with a database that already contains related tables and sample data. When you create a database in the Azure portal, selecting **Sample** on the **Additional settings** tab populates the new database with **AdventureWorksLT**. You don't need to design tables or import a backup to start querying.

AdventureWorksLT is the lightweight version of the AdventureWorks sample database. Its product, customer, and sales-order data gives you a practical way to explore relationships and answer questions with Transact-SQL (T-SQL). It isn't the full AdventureWorks OLTP database or AdventureWorksDW; examples written for those databases might use different schemas and tables.

> [!NOTE]
> The [Azure SQL Database free tier](free-tier.md) is the recommended starting point for this small learning project. This tutorial isn't limited to the free tier: you can also use a paid Azure SQL database with AdventureWorksLT sample data. If you choose the free tier, [track your usage](free-tier.md#track-free-usage) to stay within its monthly allowances; application hosting and AI tools can have separate costs.

In this tutorial, you:

> [!div class="checklist"]
> - Create a database with AdventureWorksLT sample data.
> - Connect and inspect its tables and columns.
> - Read, filter, sort, join, and summarize sample data.
> - Give an AI coding assistant a prompt for a product explorer with create, read, update, and delete (CRUD) operations.

The SQL query examples in this tutorial are read-only; the application exercise changes sample data. You use the sample database you created, not a second database created by a query.

## Prerequisites

- An Azure account and subscription with permission to create the required resources. If you use the free tier, check the [free tier prerequisites](free-tier.md#prerequisites).
- An approved way to connect to the database. See [Connect and query Azure SQL Database](connect-query-content-reference-guide.md).
- Optionally, an AI coding assistant for the application exercise. Only share schema information you're authorized to share. Don't paste passwords, access tokens, connection strings containing credentials, or customer data into a prompt.

## Create a database with sample data

For the recommended free tier path, follow [Create a free database in the Azure portal](free-tier.md?tabs=azure-portal#create-a-database). To use a paid database instead, follow [Create a single database](single-database-create-quickstart.md?tabs=azure-portal) and review its costs. If you already have an Azure SQL database with AdventureWorksLT sample data, skip to [Connect to your sample database](#connect-to-your-sample-database).

For either creation path, before you select **Review + create**, choose the sample data:

1. Open **Additional settings**.
1. Under **Data source**, set **Use existing data** to **Sample**.
1. Confirm that the page states that **AdventureWorksLT will be created as the sample database**.
1. Complete creation. If you selected the free tier, [verify the free tier deployment](free-tier.md#verify-the-deployment).

**Sample** selects the initial data, not the pricing model. For free tier eligibility, charges, and pause behavior, see the [free tier guide](free-tier.md).

The sample is an explicit creation choice, not something every new database contains. If you already have an empty database, these steps don't add the sample to it. Create a separate learning database with **Sample**, or use the [documented AdventureWorks installation options](/sql/samples/adventureworks-install-configure) appropriate for your platform. Don't overwrite a database you need.

## Connect to your sample database

1. Follow the [connection instructions for your chosen tool](connect-query-content-reference-guide.md). Select your database with AdventureWorksLT sample data; its name doesn't have to be `AdventureWorksLT`.
1. Open a query editor attached to that connection and run:

   ```sql
   SELECT DB_NAME() AS CurrentDatabase;
   ```

1. Confirm that the result matches your learning database before running the remaining examples.

To change databases, open or change the connection in your tool rather than running `USE`.

## Get to know the sample schema

A *table* stores rows with named columns. A *schema* groups database objects; `SalesLT.Product` means the `Product` table in the `SalesLT` schema. A *primary key* identifies a row, and a *foreign key* relates a row to a row in another table.

List the sample tables:

```sql
SELECT s.name AS SchemaName, t.name AS TableName
FROM sys.tables AS t
INNER JOIN sys.schemas AS s
    ON s.schema_id = t.schema_id
WHERE s.name = N'SalesLT'
ORDER BY t.name;
```

The AdventureWorksLT sample inspected for this tutorial contains these ten `SalesLT` tables:

| Area | Tables | What to explore |
|---|---|---|
| Products | `Product`, `ProductCategory`, `ProductModel` | Product prices and their category/model relationships |
| Descriptions | `ProductDescription`, `ProductModelProductDescription` | Descriptions associated with product models |
| Customers and addresses | `Customer`, `Address`, `CustomerAddress` | Customers and their associated addresses |
| Sales | `SalesOrderHeader`, `SalesOrderDetail` | Orders and the products on each order |

Inspect the columns in `Product` rather than guessing their names:

```sql
SELECT c.name AS ColumnName,
       ty.name AS DataType,
       c.max_length AS MaxLengthBytes,
       c.is_nullable AS IsNullable
FROM sys.columns AS c
INNER JOIN sys.types AS ty
    ON ty.user_type_id = c.user_type_id
WHERE c.object_id = OBJECT_ID(N'SalesLT.Product')
ORDER BY c.column_id;
```

`IsNullable = 1` means a column can contain `NULL`, representing missing or unknown information. `MaxLengthBytes` is a byte count, not always a character count; `-1` denotes a large-value type. You might see alias types such as `Name`.

If `SalesLT` tables aren't present, check the selected database and whether **Sample** was chosen during creation. Don't substitute tables from the full AdventureWorks schema or silently generate replacement tables.

## 1. Read your first rows

**Question:** What products are in the catalog?

```sql
SELECT TOP (5)
       ProductID,
       Name,
       ProductNumber,
       ListPrice
FROM SalesLT.Product
ORDER BY ProductID;
```

- `SELECT` names the columns to return.
- `FROM` identifies the table.
- `TOP (5)` limits the output to five rows.
- `ORDER BY` makes the result predictable. `ProductID` is a unique tie-breaker in later examples, too.

In the sample used to validate this tutorial, the first row is product `680`, **HL Road Frame - Black, 58**, with a list price of `1431.50`. Results can differ if your sample version or data has changed.

**Try it:** Return ten rows instead of five. Add `Color` to the selected columns.

## 2. Filter and sort products

**Question:** Which black products have a list price above 1,000?

```sql
SELECT TOP (5)
       ProductID,
       Name,
       Color,
       ListPrice
FROM SalesLT.Product
WHERE ListPrice > 1000
  AND Color = N'Black'
ORDER BY ListPrice DESC, ProductID;
```

`WHERE` keeps only matching rows. `AND` requires both conditions to be true. `DESC` puts the highest prices first. The `N` prefix denotes a Unicode string literal; text values use single quotation marks.

The validated sample returns **Mountain-100 Black, 38** first, with a list price of `3374.99`.

**Try it:** Change the color to `N'Red'` or change the minimum price.

## 3. Find missing values

**Question:** Which products don't have a color recorded?

```sql
SELECT TOP (5)
       ProductID,
       Name,
       Color
FROM SalesLT.Product
WHERE Color IS NULL
ORDER BY ProductID;
```

Use `IS NULL`, not `= NULL`. A missing value isn't the same as an empty string, zero, or a color named "None". The validated sample includes **LL Fork** among these results.

**Try it:** Use `IS NOT NULL` to find products with a recorded color.

## 4. Join related tables

**Question:** What are each product's category and model?

```sql
SELECT TOP (5)
       p.ProductID,
       p.Name AS ProductName,
       pc.Name AS CategoryName,
       pm.Name AS ModelName,
       p.ListPrice
FROM SalesLT.Product AS p
LEFT JOIN SalesLT.ProductCategory AS pc
    ON pc.ProductCategoryID = p.ProductCategoryID
LEFT JOIN SalesLT.ProductModel AS pm
    ON pm.ProductModelID = p.ProductModelID
ORDER BY p.ProductID;
```

`p`, `pc`, and `pm` are short aliases. Each `ON` clause follows a foreign-key relationship. `LEFT JOIN` keeps a product even when its nullable category or model relationship has no value. `AS ProductName` gives the result column a meaningful label.

For product `680`, the validated result shows category **Road Frames** and model **HL Road Frame**.

**Try it:** Add a `WHERE` clause that keeps only category `N'Helmets'`. A filter on `pc.Name` then excludes products without a matching category.

## 5. Summarize products by category

**Question:** Which categories contain the most products?

```sql
SELECT TOP (5)
       pc.ProductCategoryID,
       pc.Name AS CategoryName,
       COUNT(p.ProductID) AS ProductCount,
       CAST(AVG(p.ListPrice) AS decimal(19,2)) AS AverageListPrice
FROM SalesLT.ProductCategory AS pc
LEFT JOIN SalesLT.Product AS p
    ON p.ProductCategoryID = pc.ProductCategoryID
GROUP BY pc.ProductCategoryID, pc.Name
HAVING COUNT(p.ProductID) > 0
ORDER BY ProductCount DESC, pc.ProductCategoryID;
```

`GROUP BY` forms one group per category. `COUNT(p.ProductID)` counts matching products, not the placeholder row produced by a left join with no match. `AVG` calculates the average list price, and `CAST` displays it to two decimal places. `HAVING` filters groups after aggregation.

The validated sample's **Road Bikes** category contains `43` products with an average list price of `1597.45`. This groups by the product's directly assigned category; it doesn't roll up the category hierarchy.

**Try it:** Remove `HAVING` and `TOP (5)` to include empty categories. Their product count is zero and their average is `NULL`, not zero.

## 6. Answer a sales question

**Question:** Which products have the highest line sales in the sample orders?

```sql
SELECT TOP (5)
       p.ProductID,
       p.Name AS ProductName,
       SUM(d.OrderQty) AS UnitsOrdered,
       CAST(SUM(d.LineTotal) AS decimal(19,2)) AS LineSales
FROM SalesLT.SalesOrderDetail AS d
INNER JOIN SalesLT.Product AS p
    ON p.ProductID = d.ProductID
GROUP BY p.ProductID, p.Name
ORDER BY LineSales DESC, p.ProductID;
```

`INNER JOIN` returns matching order-line/product pairs. `SUM` adds quantities and line totals within each product group. Use the existing `LineTotal` column rather than multiplying list price by quantity: order lines have their own unit price and discount.

This value represents the sum of sample order-line amounts, not profit or a measure of paid invoices. It excludes order-level tax and freight. The validated sample's first result is **Touring-1000 Blue, 60**, with `26` units and line sales of `37191.49`. Historical sample dates aren't evidence of recent business activity.

**Try it:** Sort by `UnitsOrdered DESC, p.ProductID` instead.

## Build a product explorer with an AI coding assistant

Use an AI coding assistant to turn your sample database into a product explorer with CRUD operations for products, categories, and models. Start with a prompt that describes what you want to build, and let the agent discover the schema and work out the implementation details.

Configure your assistant's database connection securely, then replace the server and database placeholders in the prompt. Don't put credentials in the prompt. If the assistant can't inspect your database, it can help you provide the schema information it needs.

Review the agent's plan and generated code before running it. This prompt builds an application; it doesn't add AI inference to the application.

```text
Build a local product explorer using the AdventureWorksLT sample
data in my existing Azure SQL Database:

- Server: <your-server>.database.windows.net
- Database: <your-sample-database>

Support creating, viewing, editing, and deleting products, categories, and
models. Include product search and filters for category and price. Validate
inputs, respect table relationships, and confirm deletions. Explain when
related records prevent a deletion rather than deleting those records.

Inspect the schema with read-only access, suggest a framework, and confirm
a short plan with me before coding. Use real sample data, not mock data.
If you can't connect, explain what you need rather than inventing the schema.

Keep database access server-side, use parameterized queries and a
least-privilege app identity for CRUD on these tables, and keep credentials
out of chat and source code. Don't change the schema or provision cloud resources.

Include local setup instructions and tests for CRUD operations. Test against
my sample database with my approval, and tell me what you verified.
```

For authentication guidance, see [Microsoft Entra authentication for Azure SQL](authentication-aad-overview.md). If you later choose to deploy the application, review authentication, network access, and hosting costs separately.

## Troubleshoot common issues

| Symptom | What to check |
|---|---|
| `Invalid object name 'SalesLT.Product'` | Confirm `DB_NAME()` and whether the database was created with **Sample**. Full AdventureWorks and AdventureWorksLT aren't interchangeable. |
| Sign-in or connection failure | Check your configured authentication, database permissions, and approved network path. Don't disable certificate validation or open unrestricted firewall access. |
| Query succeeds but no rows match | Check the filter and actual sample values. Sample dates might be historical; don't assume a filter for the current year returns rows. |
| A category or model is blank | Inspect nullable relationships. Use `LEFT JOIN` when you need to preserve the product. |
| Sample tables don't appear in metadata | Check database selection and metadata visibility with an authorized administrator before assuming the tables are absent. |
| The free database is paused or unavailable | Review [free tier pause and monthly-limit behavior](free-tier.md#considerations-and-limitations). |

## Clean up or continue learning

The SQL query examples don't change the sample data or create tables. The application can add, update, and delete records, so review any data changes you want to keep before cleaning up.

- To keep learning, save your query file and reconnect to the same database later.
- When you're finished, disconnect your query tools. If you use the free tier, follow the [guidance for allowing auto-pause](free-tier.md#considerations-and-limitations).
- If you no longer need the learning database, delete **only that database** through the Azure portal after confirming its name. Don't delete a shared logical server or resource group.
- Stop any local application you created so it doesn't maintain background database connections.

## Related content

- [AdventureWorks sample databases](/sql/samples/adventureworks-install-configure)
- [Get started querying with Transact-SQL](/training/paths/get-started-querying-with-transact-sql/)
- [SELECT (Transact-SQL)](/sql/t-sql/queries/select-transact-sql)
- [Build and test apps with the Azure SQL Database free tier](free-tier.md)
